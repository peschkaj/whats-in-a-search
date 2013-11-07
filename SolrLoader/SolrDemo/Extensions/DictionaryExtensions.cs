using System;
using System.Collections.Generic;

namespace SolrDemo.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            return dict.ContainsKey(key) ? dict[key] : default(TValue);
        }
    }
}

