using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Core.Extensions
{
    public static class RandomExtensions
    {
        /// <summary>
        /// 返回随机布尔值
        /// </summary>
        /// <param name="random"></param>
        /// <returns>随机布尔值</returns>
        public static bool NextBoolean(this Random random)
        {
            return random.NextDouble() >= 0.5;
        }

        /// <summary>
        /// 返回指定枚举类型的随机枚举值
        /// </summary>
        /// <param name="random"></param>
        /// <returns>指定枚举类型的随机枚举值</returns>
        public static T NextEnum<T>(this Random random) where T : struct
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }
            Array array = Enum.GetValues(type);
            int index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }

        /// <summary>
        /// 返回数组中的随机元素
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="random"></param>
        /// <param name="source">源数组</param>
        /// <returns>元素数组中的某个随机项</returns>
        public static T NextItem<T>(this Random random, T[] source)
        {
            if (source == null || source.Length == 0)
            {
                return default(T);
            }
            return source[random.Next(source.Length)];
        }

        /// <summary>
        /// 从指定源集合中获取排除指定元素的指定个数的随机项集合
        /// </summary>
        /// <typeparam name="T">项类型</typeparam>
        /// <param name="random"></param>
        /// <param name="source">源集合</param>
        /// <param name="count">要获取的子集合的项数量</param>
        /// <param name="excepts">要排除的项集合</param>
        /// <returns></returns>
        public static List<T> NextItems<T>(this Random random, T[] source, int count, params T[] excepts)
        {
            if (source.Length <= count)
            {
                return source.ToList();
            }
            List<T> result = new List<T>();
            while (result.Count < count)
            {
                T item = random.NextItem(source);
                if (result.Contains(item) || excepts.Contains(item))
                {
                    continue;
                }
                result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// 获取指定的长度的随机数字字符串
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机数字符串</returns>
        public static string NextNumberString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            char[] pattern = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var result = string.Empty;
            int n = pattern.Length;
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }

        /// <summary>
        /// 获取指定的长度的随机字母字符串
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机字母组成字符串</returns>
        public static string NextLetterString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            char[] pattern = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var result = string.Empty;
            int n = pattern.Length;
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }

        /// <summary>
        /// 获取指定的长度的随机字母和数字字符串
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length">要获取随机数长度</param>
        /// <returns>指定长度的随机字母和数字组成字符串</returns>
        public static string NextLetterAndNumberString(this Random random, int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length");
            }
            char[] pattern = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            var result = string.Empty;
            int n = pattern.Length;
            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(0, n);
                result += pattern[rnd];
            }
            return result;
        }
    }
}
