using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Core.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 创建指定泛型类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(this Type type)
        {
            var obj = Activator.CreateInstance(type);
            if (obj is null)
                return default;
            return (T)obj;
        }

        /// <summary>
        /// 创建指定泛型类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(this Type type, params object[] args)
        {
            var obj = Activator.CreateInstance(type, args);
            if (obj is null)
                return default;
            return (T)obj;
        }

        /// <summary>
        /// 判断类型是否为可空类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 判断类型是否为List类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsListType(this Type type)
        {
            return type.GetInterfaces().Contains(typeof(IList));
        }

        /// <summary>
        /// 判断类型是否为字典类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDictionaryType(this Type type)
        {
            return (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IDictionary<,>))) ||
                    (from t in type.GetInterfaces()
                     where t.IsGenericType
                     select t.GetGenericTypeDefinition()).Any<Type>(t => (t == typeof(IDictionary<,>)));
        }

        /// <summary>
        /// 判断类型是否为集合类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCollectionType(this Type type)
        {
            return ((type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(ICollection<>))) ||
                    (from t in type.GetInterfaces()
                     where t.IsGenericType
                     select t.GetGenericTypeDefinition()).Any<Type>(t => (t == typeof(ICollection<>))));
        }

        /// <summary>
        /// 判断类型是否为可迭代类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnumerableType(this Type type)
        {
            return type.GetInterfaces().Contains(typeof(IEnumerable));
        }

        /// <summary>
        /// 获取<see cref="Nullable{TValue}"/>范型的构造类型
        /// </summary>
        public static Type GetTypeOfNullable(this Type type)
        {
            return type.GetGenericArguments()[0];
        }

        /// <summary>
        /// 获取不可空类型的构造类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetNonNullableType(this Type type)
        {
            if (!type.IsNullableType())
            {
                return type;
            }

            return type.GetGenericArguments()[0];
        }
    }
}
