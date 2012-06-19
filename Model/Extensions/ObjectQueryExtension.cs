using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

// Awesome code from Rudi Breedenraedt (http://www.codetuning.net/blog/post/Entity-Framework-reattaching-entity-graphs-%283%29.aspx)
// License is probably http://www.codeproject.com/info/cpol10.aspx
namespace MeisterGeister.Model.Extensions
{
	/// <summary>
	/// Extension methods on ObjectQuery.
	/// </summary>
	public static class ObjectQueryExtension
	{
		/// <summary>
		/// Specifies the related objects to include in the query results using
		/// a lambda expression listing the path members.
		/// </summary>
		/// <returns>A new System.Data.Objects.ObjectQuery&lt;T&gt; with the defined query path.</returns>
		public static ObjectQuery<T> Include<T>(this ObjectQuery<T> query, Expression<Func<T, object>> path)
		{
			// Retrieve member path:
			List<ExtendedPropertyInfo> members = new List<ExtendedPropertyInfo>();
			EntityFrameworkHelper.CollectRelationalMembers(path, members);

			// Build string path:
			StringBuilder sb = new StringBuilder();
			string separator = "";
			foreach (ExtendedPropertyInfo member in members)
			{
				if (member.ReferenceOnly) break;
				sb.Append(separator);
				sb.Append(member.PropertyInfo.Name);
				separator = ".";
			}

			// Apply Include:
			if (sb.Length > 0)
				return query.Include(sb.ToString());
			else
				return query;
		}
	}
}
