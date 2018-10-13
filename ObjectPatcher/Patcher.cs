using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectPatcher
{
    public static class Patcher
    {
        private static ConcurrentDictionary<Type, ConcurrentDictionary<string, MemberInfo>> typeMember;

        static Patcher()
        {
            typeMember = new ConcurrentDictionary<Type, ConcurrentDictionary<string, MemberInfo>>();
        }

        public static void Apply<T>(T obj, Dictionary<string, object> patchDictionary)
        {
            var type = obj.GetType();
            foreach (var entry in patchDictionary)
            {
                var memberInfo = GetMemberInfo(type, entry.Key);
                var propertyInfo = memberInfo as PropertyInfo;
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(obj, entry.Value);
                }
            }
        }

        private static MemberInfo GetMemberInfo(Type t, string key)
        {
            if (!typeMember.TryGetValue(t, out var members))
            {
                FillMembersForType(t);
                members = typeMember[t];
            }

            var normalizedKey = key.ToLower();
            if (members.TryGetValue(normalizedKey, out var memberInfo))
            {
                return memberInfo;
            }

            return null;
        }

        private static void FillMembersForType(Type t)
        {
            var members = new ConcurrentDictionary<string, MemberInfo>();
            var properties = t.GetRuntimeProperties();
            var fields = t.GetRuntimeFields();
            foreach (var propertyInfo in properties)
            {
                members.TryAdd(propertyInfo.Name.ToLower(), propertyInfo);
            }

            foreach (var fieldInfo in fields)
            {
                members.TryAdd(fieldInfo.Name.ToLower(), fieldInfo);
            }

            typeMember.TryAdd(t, members);
        }
    }
}