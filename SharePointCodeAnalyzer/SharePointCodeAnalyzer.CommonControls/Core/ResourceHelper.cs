using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SharePointCodeAnalyzer.CommonControls.Core
{
    internal class ResourceHelper
    {
        private static List<ResourceDictionary> cache = null;
        private static Dictionary<string, object> cachedItems = new Dictionary<string, object>();
        private static List<string> notIncludedKeys = new List<string>();

        internal static object FindResource(string key)
        {
            if (cachedItems.ContainsKey(key))
            {
                return cachedItems[key];
            }
            if (notIncludedKeys.Contains(key))
            {
                return null;
            }
            object obj2 = null;
            if (cache == null)
            {
                cache = Application.Current.Resources.MergedDictionaries.ToList<ResourceDictionary>();
            }
            foreach (ResourceDictionary dictionary in cache)
            {
                foreach (object obj3 in dictionary.Keys)
                {
                    if (obj3.ToString() == key)
                    {
                        obj2 = dictionary[obj3];
                        break;
                    }
                }
            }
            if (obj2 != null)
            {
                if (!cachedItems.ContainsKey(key))
                {
                    cachedItems.Add(key, obj2);
                }
                return obj2;
            }
            notIncludedKeys.Add(key);
            return obj2;
        }

        public static string GetKeyForObject(object objectWithKey)
        {
            if (objectWithKey != null)
            {
                foreach (ResourceDictionary dictionary in Application.Current.Resources.MergedDictionaries)
                {
                    foreach (object obj2 in dictionary.Keys)
                    {
                        if (dictionary[obj2] == objectWithKey)
                        {
                            return obj2.ToString();
                        }
                    }
                }
            }
            return null;
        }

        internal static object GetResourceOfType(Type typeOfDataContext, Type typeOfResource, string prefix, string suffix)
        {
            bool foundInCache = false;
            string key = prefix + typeOfDataContext.Name + suffix;
            if (cachedItems.ContainsKey(key))
            {
                foundInCache = true;
                return cachedItems[key];
            }
            object obj2 = InternalGetResourceOfType(typeOfDataContext, typeOfResource, prefix, suffix, ref foundInCache);
            if (!cachedItems.ContainsKey(key))
            {
                cachedItems.Add(key, obj2);
            }
            return obj2;
        }

        internal static void InitializeCache(List<Type> list)
        {
            using (List<Type>.Enumerator enumerator = list.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Type current = enumerator.Current;
                }
            }
            int count = cachedItems.Count;
        }

        private static object InternalGetResourceOfType(Type typeOfDataContext, Type typeOfResource, string prefix, string suffix, ref bool foundInCache)
        {
            string key = prefix + typeOfDataContext.Name + suffix;
            if (cachedItems.ContainsKey(key))
            {
                foundInCache = true;
                return cachedItems[key];
            }
            object obj2 = FindResource(key);
            if (obj2 != null)
            {
                return obj2;
            }
            //if (typeOfDataContext.GetTypeInfo().BaseType != null)
            //{
            //    return InternalGetResourceOfType(typeOfDataContext.GetTypeInfo().BaseType, typeOfResource, prefix, suffix, ref foundInCache);
            //}
            return null;
        }
    }
}
