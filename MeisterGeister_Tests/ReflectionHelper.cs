using System;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace MeisterGeister_Tests
{
    /// <summary>
    /// von http://mkamoski1.wordpress.com/2009/12/01/reflection-for-information-on-constants/
    /// </summary>
    static class ReflectionHelper
    {
        #region HelperMethods

        /// <summary>
        /// This returns FieldInfo for constants in a given type.
        /// </summary>
        /// <param name="targetType">This is the type to use.</param>
        /// <returns>This is an array of FieldInfo objects.</returns>
        /// <remarks>
        /// Note that this code was refactored from code found on 20091201 at the following link...
        /// http://weblogs.asp.net/whaggard/archive/2003/02/20/2708.aspx
        /// </remarks>
        public static FieldInfo[] GetConstantFieldInfoArray(System.Type targetType)
        {
            //The "BindingFlags.Public" gets all public fields.
            //The "BindingFlags.Static" gets all static fields.
            //The "BindingFlags.FlattenHierarchy" gets fields from all base types.
            FieldInfo[] myFields = targetType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            ArrayList myConstants = new ArrayList();

            //Go through the list and only pick out the constants.
            foreach (FieldInfo myFieldInfoTemp in myFields)
            {
                // IsLiteral determines if its value is written at compile time and not changeable.
                // IsInitOnly determine if the field can be set in the body of the constructor.
                // For C#, a field with the readonly keyword would have IsLiteral=true and IsInitOnly=true.
                // For C#, a const field would have IsLiteral=true and IsInitOnly=false.
                if ((myFieldInfoTemp.IsLiteral) && (!myFieldInfoTemp.IsInitOnly))
                {
                    myConstants.Add(myFieldInfoTemp);
                }
            }

            // Return an array of FieldInfos
            return (FieldInfo[])myConstants.ToArray(typeof(FieldInfo));
        }

        /// <summary>
        /// This will get the values of all the constants in the given type.
        /// </summary>
        /// <param name="targetType">This is the type to use.</param>
        /// <param name="convertToLowercase">This is a flag indicating if the values should be converted to lowercase or not.</param>
        /// <param name="trimWhitespace">This is a flag indicating if the values should be whitespace-trimmed or not.</param>
        /// <returns>This a collection of the values.</returns>
        /// <remarks>
        /// Note this will only work if the underlying type for the constants is "string".
        /// </remarks>
        public static StringCollection GetConstantValueStringCollection(System.Type targetType, bool convertToLowercase, bool trimWhitespace)
        {
            StringCollection myCollection = null;

            FieldInfo[] myFieldInfoArray = MeisterGeister_Tests.ReflectionHelper.GetConstantFieldInfoArray(targetType);
            string myValueStringTemp = "";
            object myValueObjectTemp = null;
            int myLoopIndex = 0;
            myCollection = new StringCollection();

            foreach (FieldInfo myFieldInfoTemp in myFieldInfoArray)
            {
                myValueObjectTemp = null;
                myValueStringTemp = "";

                if (myFieldInfoTemp == null)
                {
                    throw new System.ApplicationException("The current object, myFieldInfoTemp, is null.");
                }

                myValueObjectTemp = myFieldInfoTemp.GetValue(null);
                Debug.WriteLine("myLoopIndex='" + myLoopIndex.ToString() + "'");

                if (myValueObjectTemp == null)
                {
                    myValueStringTemp = "";
                    Debug.WriteLine("myValueObjectTemp == null");
                    Debug.WriteLine("myValueStringTemp='" + myValueStringTemp + "'");
                }
                else
                {
                    myValueStringTemp = myValueObjectTemp.ToString();

                    if (convertToLowercase)
                    {
                        myValueStringTemp = myValueStringTemp.ToLower();
                    }

                    if (trimWhitespace)
                    {
                        myValueStringTemp = myValueStringTemp.Trim();
                    }

                    Debug.WriteLine("myValueObjectTemp != null");
                    Debug.WriteLine("myValueStringTemp='" + myValueStringTemp + "'");
                }

                myCollection.Add(myValueStringTemp);
                myLoopIndex = myLoopIndex + 1;
            }

            return myCollection;
        }

        #endregion //HelperMethods
    }
}
