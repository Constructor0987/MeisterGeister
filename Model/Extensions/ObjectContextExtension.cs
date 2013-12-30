using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace MeisterGeister.Model.Extensions
{
    // Based on awesome code from Rudi Breedenraedt (http://www.codetuning.net/blog/post/Entity-Framework-reattaching-entity-graphs-%283%29.aspx)
    // License is probably http://www.codeproject.com/info/cpol10.aspx
    // Modified to work with POCOs

	/// <summary>
	/// Extension methods on ObjectContext.
	/// </summary>
    public static class ObjectContextExtension
    {
        /// <summary>
        /// Attaches an entire objectgraph to the context.
        /// </summary>
        public static T AttachObjectGraph<T>(this ObjectContext context, T entity, params Expression<Func<T, object>>[] paths)
        {
            return AttachObjectGraphs(context, new T[] { entity }, paths)[0];
        }

        /// <summary>
        /// Attaches multiple entire objectgraphs to the context.
        /// </summary>
        public static T[] AttachObjectGraphs<T>(this ObjectContext context, IEnumerable<T> entities, params Expression<Func<T, object>>[] paths)
        {
            T[] unattachedEntities = entities.ToArray();
            T[] attachedEntities = new T[unattachedEntities.Length];
            Type entityType = typeof(T);

            if (unattachedEntities.Length > 0)
            {
                // Workaround to ensure the assembly containing the entity type is loaded:
                // (see: https://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=3405138&SiteID=1)
                try { context.MetadataWorkspace.LoadFromAssembly(entityType.Assembly); }
                catch { }

                #region Automatic preload root entities

                // Create a WHERE clause for preload the root entities:
                StringBuilder where = new StringBuilder("(1=0)");
                List<ObjectParameter> pars = new List<ObjectParameter>();
                int pid = 0;
                foreach (T entity in unattachedEntities)
                {
                    // If the entity has an entitykey:
                    EntityKey entityKey;
                    if (entity is IEntityWithKey)
                        entityKey = ((IEntityWithKey)entity).EntityKey;
                    else
                        entityKey = context.GetEntityKeyFromPrimaryKey(entity);
                    if (entityKey != null)
                    {
                        where.Append(" OR ((1=1)");
                        foreach (EntityKeyMember keymember in entityKey.EntityKeyValues)
                        {
                            string pname = String.Format("p{0}", pid++);
                            where.Append(" AND (it.[");
                            where.Append(keymember.Key);
                            where.Append("] = @");
                            where.Append(pname);
                            where.Append(")");
                            pars.Add(new ObjectParameter(pname, keymember.Value));
                        }
                        where.Append(")");
                    }
                }

                // If WHERE clause not empty, construct and execute query:
                if (pars.Count > 0)
                {
                    // Construct query:
                    ObjectQuery<T> query = new ObjectQuery<T>(GetEntitySetName(context, typeof(T)), context);
                    if (paths != null)
                        foreach (var path in paths)
                            query = query.Include(path);
                    query = query.Where(where.ToString(), pars.ToArray());

                    // Execute query and load entities:
                    //Console.WriteLine(query.ToTraceString());
                    query.Execute(MergeOption.AppendOnly).ToArray();
                }

                #endregion Automatic preload root entities

                // Attach the root entities:
                for (int i = 0; i < unattachedEntities.Length; i++)
                    attachedEntities[i] = (T)context.AddOrAttachInstance(unattachedEntities[i], true);

                // Collect property paths into a tree:
                TreeNode<ExtendedPropertyInfo> root = new TreeNode<ExtendedPropertyInfo>(null);
                if (paths != null)
                    foreach (var path in paths)
                    {
                        List<ExtendedPropertyInfo> members = new List<ExtendedPropertyInfo>();
                        EntityFrameworkHelper.CollectRelationalMembers(path, members);
                        root.AddPath(members);
                    }

                // Navigate over all properties:
                for (int i = 0; i < unattachedEntities.Length; i++)
                    NavigatePropertySet(context, root, unattachedEntities[i], attachedEntities[i]);
            }

            // Return the attached root entities:
            return attachedEntities;
        }

        /// <summary>
        /// Adds or attaches the entity to the context. If the entity has an EntityKey,
        /// the entity is attached, otherwise a clone of it is added.
        /// </summary>
        /// <returns>The attached entity.</returns>
        public static object AddOrAttachInstance(this ObjectContext context, object entity, bool applyChanges, bool alwaysAdd = false)
        {
            //try { context.MetadataWorkspace.LoadFromAssembly(entity.GetType().Assembly); }
            //catch { }
            object attachedEntity = null;
            if(alwaysAdd) //always just add the entity, if this is set
            {
                attachedEntity = context.GetShallowClone(entity);
                context.AddObject(context.GetEntitySetName(entity.GetType()), attachedEntity);
                return attachedEntity;
            }
            EntityKey entityKey = context.GetEntityKeyFromPrimaryKey(entity);
            if (entityKey != null && entityKey.EntityKeyValues != null)
                context.TryGetObjectByKey(entityKey, out attachedEntity);
            if (attachedEntity == null)
            {
                //avoid adding the same object multiple times.
                if(context.GetChangedObjects().Where(ose => context.GetEntityKeyFromPrimaryKey(ose.Entity) == entityKey).Count() == 0)
                {
                    attachedEntity = context.GetShallowClone(entity);
                    context.AddObject(context.GetEntitySetName(entity.GetType()), attachedEntity);
                    //((IEntityWithKey)entity).EntityKey = ((IEntityWithKey)attachedEntity).EntityKey;
                }
                return attachedEntity;
            }
            else
            {
                if (applyChanges)
                {
                    if (context.ObjectStateManager.GetObjectStateEntry(attachedEntity).State == EntityState.Deleted)
                        context.ObjectStateManager.ChangeObjectState(attachedEntity, EntityState.Modified);
                    context.ApplyCurrentValues(entityKey.EntitySetName, entity);
                }
                return attachedEntity;
            }
        }

        /// <summary>
        /// Returns the EntitySetName for the given entity type.
        /// </summary>
        public static string GetEntitySetName(this ObjectContext context, Type entityType)
        {
            Type type = ObjectContext.GetObjectType(entityType);

            while (type != null)
            {
                EntitySetBase set = context.GetEntitySet(type);
                if (set != null)
                    return set.Name;
                // If no matching attribute or entitySetName found, try basetype:
                type = type.BaseType;
            }

            // Fail if no valid entitySetName was found:
            throw new InvalidOperationException(String.Format("Unable to determine EntitySetName of type '{0}'.", entityType));
        }

        public static EntityKey GetEntityKeyFromPrimaryKey(this ObjectContext context, object o) //where T : class
        {
            IDictionary<string, object> key = new Dictionary<string, object>();
            string entitysetname = context.GetEntitySetName(o.GetType());
            foreach (System.Data.Metadata.Edm.EdmMember keyMember in context.GetEntitySet(o.GetType()).ElementType.KeyMembers)
            {
                //Impromptu ist schneller als Reflection
                object keyValue = ImpromptuInterface.Impromptu.InvokeGet(o, keyMember.Name);
                key.Add(keyMember.Name, keyValue);
            }
            try
            {
                return new EntityKey(context.DefaultContainerName + "." + entitysetname, key);
            }
            catch (ArgumentException ex)
            {
                string keyString = String.Empty;
                foreach (string k in key.Keys)
                    keyString += String.Format("{0}:{1}\n", k, key[k]);
                ArgumentException e = new ArgumentException("Error while creating the key.\nKey values:\n" + keyString, ex);
                foreach (string k in key.Keys)
                    e.Data.Add(k, key[k]);
                throw e;
            }
        }

        public static EntitySet GetEntitySet(this ObjectContext context, Type entityType)
        {
            entityType = ObjectContext.GetObjectType(entityType);
            EntityContainer container =
                    context.MetadataWorkspace
                        .GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);
            string entitySetName = container.BaseEntitySets.First(b => b.ElementType.Name == entityType.Name).Name;

            EntitySet entitySet = null;
            container.TryGetEntitySetByName(entitySetName, true, out entitySet);
            return entitySet;
        }

        /// <summary>
        /// Returns a clone of only the primitive database persistent properties.
        /// </summary>
        public static object GetShallowClone(this ObjectContext context, object entity)
        {
            Type entityType = ObjectContext.GetObjectType(entity.GetType());
            object clone = ImpromptuInterface.Impromptu.InvokeConstructor(entityType); // Activator.CreateInstance(entity.GetType());
            foreach (System.Data.Metadata.Edm.EdmMember member in context.GetEntitySet(entityType).ElementType.Members.Where(m => m.TypeUsage.EdmType is PrimitiveType))
            {
                ImpromptuInterface.Impromptu.InvokeSet(clone, member.Name, ImpromptuInterface.Impromptu.InvokeGet(entity, member.Name));
            }
            return clone;
        }


        /// <summary>
        /// Synchronises a targetlist with a sourcelist by adding or removing items from the targetlist.
        /// The targetlist is untyped and controlled through reflection.
        /// </summary>
        private static void SyncList(object targetlist, List<object> sourcelist, out List<object> removedItems)
        {
            List<object> localsourcelist = new List<object>(sourcelist);
            List<object> toremove = new List<object>();

            // Compare both lists:
            foreach (object item in (IEnumerable)targetlist)
            {
                bool found = false;
                for (int i = 0; i < localsourcelist.Count; i++)
                {
                    if (Object.ReferenceEquals(localsourcelist[i], item))
                    {
                        localsourcelist[i] = null;
                        found = true;
                    }
                }
                if (!found)
                    toremove.Add(item);
            }

            // Add members not in targetlist:
            foreach (object item in localsourcelist)
            {
                if (Object.ReferenceEquals(item, null) == false)
                    ImpromptuInterface.Impromptu.InvokeMemberAction(targetlist, "Add", item);
                    //targetlist.PublicInvokeMethod("Add", item);
            }

            // Remove members not in sourcelist:
            foreach (object item in toremove)
                ImpromptuInterface.Impromptu.InvokeMemberAction(targetlist, "Remove", item);
                //targetlist.PublicInvokeMethod("Remove", item);

            // Expose removed items:
            removedItems = toremove;
        }

        #region Entity path marker methods

        /// <summary>
        /// Marker method to indicate this section of the path expression
        /// should not be loaded but only referenced.
        /// </summary>
        public static object ReferenceOnly(this System.ComponentModel.INotifyPropertyChanged entity)
        {
            throw new InvalidOperationException("The ReferenceOnly() method is a marker method in entity property paths and should not be effectively invoked.");
        }

        /// <summary>
        /// Marker method to indicate the instances the method is called on
        /// within path expressions should not be updated.
        /// </summary>
        public static object WithoutUpdate(this System.ComponentModel.INotifyPropertyChanged entity)
        {
            throw new InvalidOperationException("The WithoutUpdate() method is a marker method in entity property paths and should not be effectively invoked.");
        }

        /// <summary>
        /// Marker method to indicate the instances the method is called on
        /// within path expressions should always be inserted.
        /// </summary>
        public static object AlwaysInsert(this System.ComponentModel.INotifyPropertyChanged entity)
        {
            throw new InvalidOperationException("The AlwaysInsert() method is a marker method in entity property paths and should not be effectively invoked.");
        }

        #endregion

        /// <summary>
        /// Navigates a property path on detached instance to translate into attached instance.
        /// </summary>
        private static void NavigatePropertySet(ObjectContext context, TreeNode<ExtendedPropertyInfo> propertynode, object owner, object attachedowner)
        {
            // Try to navigate each of the properties:
            foreach (TreeNode<ExtendedPropertyInfo> childnode in propertynode.Children)
            {
                ExtendedPropertyInfo property = childnode.Item;

                // Retrieve navigation property value:
                object related = property.PropertyInfo.GetValue(owner, null); //z.B. owner.Held_Fernwaffe oder Talent.Talentgruppe

                if ((property.ReferenceOnly) && (typeof(IEnumerable).IsAssignableFrom(property.PropertyInfo.PropertyType)))
                {
                    // ReferenceOnly marker not valid on collections:
                    throw new InvalidOperationException("The ReferenceOnly marker method is not supported on the many side of relations.");
                }
                else if (property.ReferenceOnly)
                {
                    //no action needed?
                    //// Apply reference update on ReferenceOnly:
                    //EntityReference reference = (EntityReference)attachedowner.PublicGetProperty(property.PropertyInfo.Name + "Reference");
                    //reference.EntityKey = ((EntityReference)owner.PublicGetProperty(property.PropertyInfo.Name + "Reference")).EntityKey;
                }
                else if (related is IEnumerable)
                {
                    // Load types manually in the context before calling the procedure

                    object attachedlist = property.PropertyInfo.GetValue(attachedowner, null);
                    // Recursively navigate through new members:
                    List<object> newlist = new List<object>();
                    foreach (var relatedinstance in (IEnumerable)related)
                    {
                        object attachedinstance = context.AddOrAttachInstance(relatedinstance, !property.NoUpdate, property.AlwaysInsert);
                        if (attachedinstance != null)
                        {
                            newlist.Add(attachedinstance);
                            NavigatePropertySet(context, childnode, relatedinstance, attachedinstance);
                        }
                    }

                    //// Synchronise lists:
                    List<object> removedItems;
                    SyncList(attachedlist, newlist, out removedItems);

                    //// Delete removed items if association is owned:
                    //if (AssociationEndBehaviorAttribute.GetAttribute(property.PropertyInfo).Owned)
                    //{
                    //    foreach (var removedItem in removedItems)
                    //        context.DeleteObject(removedItem);
                    //}

                }
                else if (!typeof(IEnumerable).IsAssignableFrom(property.PropertyInfo.PropertyType)) //-> EntityReference
                {
                    // Load reference of currently attached in context:
                    //RelatedEnd relatedEnd = (RelatedEnd)attachedowner.PublicGetProperty(property.PropertyInfo.Name + "Reference");
                    //if (((EntityObject)attachedowner).EntityState != EntityState.Added && !relatedEnd.IsLoaded)
                    //    relatedEnd.Load();

                    // Recursively navigate through new value (unless it's null):
                    object attachedinstance;
                    if (related == null)
                        attachedinstance = null;
                    else
                    {
                        attachedinstance = context.AddOrAttachInstance(related, !property.NoUpdate, property.AlwaysInsert);
                        NavigatePropertySet(context, childnode, related, attachedinstance);
                    }

                    // Synchronise value:
                    //property.PropertyInfo.SetValue(attachedowner, attachedinstance, null);
                }
            }
        }

        public static IEnumerable<ObjectStateEntry> GetChangedObjects(this ObjectContext context)
        {
            return context.ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added | System.Data.EntityState.Deleted | System.Data.EntityState.Modified);
        }

        public static void DiscardChanges(this ObjectContext context)
        {

            foreach (ObjectStateEntry entry in context.GetChangedObjects())
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.ChangeState(EntityState.Unchanged);
                        goto case System.Data.EntityState.Modified;
                    case EntityState.Modified:
                        System.Data.Common.DbDataRecord original = entry.OriginalValues;
                        foreach (string prop in entry.GetModifiedProperties())
                        {
                            int ordinal = original.GetOrdinal(prop);
                            entry.CurrentValues.SetValue(ordinal, original[ordinal]);
                            //RaisePropertyChanged(entry.Entity, prop);
                        }
                        break;
                    case EntityState.Added:
                        entry.ChangeState(EntityState.Detached);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
