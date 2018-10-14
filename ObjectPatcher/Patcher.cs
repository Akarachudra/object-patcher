using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectPatcher
{
    public static class Patcher
    {
        private static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, MemberInfo>> TypeMember;

        static Patcher()
        {
            TypeMember = new ConcurrentDictionary<Type, ConcurrentDictionary<string, MemberInfo>>();
        }

        public static void Apply<T>(T obj, Dictionary<string, object> patchDictionary)
        {
            var type = obj.GetType();
            foreach (var entry in patchDictionary)
            {
                var memberInfo = GetMemberInfo(type, entry.Key);
                if (memberInfo is PropertyInfo propertyInfo)
                {
                    propertyInfo.SetValue(obj, entry.Value);
                }
                else if (memberInfo is FieldInfo fieldInfo)
                {
                    fieldInfo.SetValue(obj, entry.Value);
                }
            }
        }

        private static MemberInfo GetMemberInfo(Type t, string key)
        {
            if (!TypeMember.TryGetValue(t, out var members))
            {
                FillMembersForType(t);
                members = TypeMember[t];
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

            TypeMember.TryAdd(t, members);
        }
    }
}