using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Core.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 判断集合是否为null或空集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> @this) => @this == null || !@this.Any();

        /// <summary>
        /// 如果条件成立则添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="value"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddIf<T>([NotNull] this ICollection<T> @this, Func<T, bool> predicate, T value)
        {
            if (@this.IsReadOnly)
                throw new InvalidOperationException($"{nameof(@this)} is readonly");

            if (predicate(value))
                @this.Add(value);
        }

        /// <summary>
        /// 如果不为空则添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddIfNotNull<T>(this ICollection<T> @this, T value) where T : class
        {
            if (@this == null)
                throw new ArgumentNullException(nameof(@this));

            if (value != null)
                @this.Add(value);
        }

        /// <summary>
        /// 如果不存在则添加
        /// </summary>
        public static void AddIfNotExist<T>(this ICollection<T> @this, T value)
        {
            if (@this == null)
                throw new ArgumentNullException(nameof(@this));

            if (!@this.Contains(value))
                @this.Add(value);
        }
    }
}
