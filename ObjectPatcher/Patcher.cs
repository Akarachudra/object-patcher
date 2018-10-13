using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectPatcher
{
    public static class Patcher
    {
        public static void Apply<T>(T obj, Dictionary<string, object> patchDictionary)
        {
            foreach (var entry in patchDictionary)
            {
                var propertyInfo = obj.GetType().GetRuntimeProperty(entry.Key);
                if (propertyInfo == null)
                {
                    throw new MissingMemberException();
                }
            }
        }
    }
}