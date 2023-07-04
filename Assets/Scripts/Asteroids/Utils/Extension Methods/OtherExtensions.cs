using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Utils.Extension_Methods
{
    public static class OtherExtensions
    {
        public static TObject Nullable<TObject>(this TObject obj) where TObject : UnityEngine.Object
        {
            if (obj is null) return null;
            return !obj ? null : obj;
        }

        public static bool IsUnityNull<T>(this T obj) where T : class
        {
            if (obj is Object uObj)
            {
                return uObj.Nullable() is null;
            }

            return obj is null;
        }

        public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key, out TValue value)
        {
            key = pair.Key;
            value = pair.Value;
        }
    }
}