using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Cyber_Kitchen.Models
{
   
        public static class Extension
        {

            /// <summary>
            /// 
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="value"></param>
            /// <returns></returns>
            public static T ParseEnum<T>(this string value) where T : struct
            {
                T enumobj = default(T);
                if (Enum.TryParse(value, true, out enumobj))
                {
                    return enumobj;
                }
                else
                {
                    return default(T);
                }
            }

            #region Type Extensions        

            /// <summary>
            /// Update properties with properties of the object Supplied (typically anonymous)
            /// </summary>
            /// <typeparam name="T">Type of Source Object</typeparam>
            /// <param name="destination">Object whose property you want to update</param>
            /// <param name="source">destination object (typically anonymous) you want to take values from</param>
            /// <returns>Update reference to same Object</returns>
            public static T Assign<T>(this T destination, object source, params string[] ignoredProperties)
            {
                if (ignoredProperties == null) ignoredProperties = new string[0];
                if (destination != null && source != null)
                {
                    var query = from sourceProperty in source.GetType().GetProperties()
                                join destProperty in destination.GetType().GetProperties()
                                on sourceProperty.Name.ToLower() equals destProperty.Name.ToLower()             //Case Insensitive Match
                                where !ignoredProperties.Contains(sourceProperty.Name)
                                where destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)   //Properties can be Assigned
                                where destProperty.GetSetMethod() != null                                       //Destination Property is not Readonly
                                select new { sourceProperty, destProperty };


                    foreach (var pair in query)
                    {
                        //Go ahead and Assign the value on the destination
                        pair.destProperty
                            .SetValue(destination,
                                value: pair.sourceProperty.GetValue(obj: source, index: new object[] { }),
                                index: new object[] { });
                    }

                }
                return destination;
            }

            /// <summary>
            /// Update properties with properties of the object Supplied (typically anonymous)
            /// </summary>
            /// <typeparam name="T">Type of Source Object</typeparam>
            /// <param name="destination">Object whose property you want to update</param>
            /// <param name="source">destination object (typically anonymous) you want to take values from</param>
            /// <returns>Update reference to same Object</returns>
            public static D Assign<S, D>(this D destination, S source, params string[] ignoredProperties)
            {
                if (ignoredProperties == null) ignoredProperties = new string[0];
                if (destination != null && source != null)
                {
                    var query = from sourceProperty in typeof(S).GetProperties()
                                join destProperty in typeof(D).GetProperties()
                                on sourceProperty.Name.ToLower() equals destProperty.Name.ToLower()             //Case Insensitive Match
                                where !ignoredProperties.Contains(sourceProperty.Name)
                                where destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)   //Properties can be Assigned
                                where destProperty.GetSetMethod() != null                                       //Destination Property is not Readonly
                                where destProperty.GetIndexParameters().Length == 0                             //property is not an indexed property
                                select new { sourceProperty, destProperty };


                    foreach (var pair in query)
                    {
                        //Go ahead and Assign the value on the destination
                        pair.destProperty
                            .SetValue(destination, value: pair.sourceProperty.GetValue(obj: source, index: new object[] { }));
                    }
                }
                return destination;
            }
            #endregion;

            #region String extensions

            /// <summary>
            /// Trim the string at both ends using the supplied "trimString" string
            /// </summary>
            /// <param name="string"></param>
            /// <param name="trimString"></param>
            /// <returns></returns>
            public static string Trim(this string @string, string trimString) => @string.TrimStart(trimString).TrimEnd(trimString);

            /// <summary>
            /// Trim the string at its start using the supplied "trimString" string
            /// </summary>
            /// <param name="original"></param>
            /// <param name="searchString"></param>
            /// <returns></returns>
            public static string TrimStart(this string original, string searchString)
                => original.StartsWith(searchString) ?
                   original.Substring(searchString.Length) :
                   original;

            /// <summary>
            /// Trim the string at its end using the supplied "trimString" string
            /// </summary>
            /// <param name="original"></param>
            /// <param name="searchString"></param>
            /// <returns></returns>
            public static string TrimEnd(this string original, string searchString)
                => original.EndsWith(searchString) ?
                   original.Substring(0, original.Length - searchString.Length) :
                   original;

            /// <summary>
            /// Joins the strings in the given string sequence using the supplied separator
            /// </summary>
            /// <param name="stringSequence"></param>
            /// <param name="separator"></param>
            /// <returns></returns>
            public static string JoinUsing(this IEnumerable<string> stringSequence, string separator) => string.Join(separator, stringSequence);

            /// <summary>
            /// counts the number of occurences of a substring
            /// </summary>
            /// <param name="source"></param>
            /// <param name="subString"></param>
            /// <returns></returns>
            public static int SubstringCount(this string source, string subString)
            {
                if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(subString)) return 0;
                if (subString.Length > source.Length) return 0;

                int lindex = 0;
                string sub = null;
                int count = 0;
                do
                {
                    sub = source.Substring(lindex, subString.Length);

                    if (sub.Equals(subString))
                    {
                        count++;
                        lindex += subString.Length;
                    }
                    else lindex++;
                }
                while ((lindex + subString.Length) <= source.Length);

                return count;
            }

            /// <summary>
            /// Splits a string using the supplied separator
            /// </summary>
            /// <param name="source"></param>
            /// <param name="separator"></param>
            /// <returns></returns>
            public static string SplitWords(this string source, string separator = " ")
                => source.Aggregate(new StringBuilder(), (acc, ch) => acc.Append(char.IsUpper(ch) ? separator : "").Append(ch)).ToString().Trim();

            #endregion
        }
    }
