using Inbox.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Core.Helpers
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取指定枚举类型的所有值
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IList<Enum> GetEnums(Type enumType)
        {
            var enums = new List<Enum>();

            if (enumType.IsNullableType())
            {
                enumType = enumType.GetTypeOfNullable();
                enums.Add(null);
            }

            var enumList = Enum.GetValues(enumType).Cast<Enum>();
            enums.AddRange(enumList);
            return enums;
        }

        /// <summary>
        /// 获取指定枚举类型的所有值
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <returns></returns>
        public static IList<TEnum> GetEnums<TEnum>()
        {
            var enums = new List<TEnum>();

            var enumType = typeof(TEnum);
            if (enumType.IsNullableType())
            {
                enumType = enumType.GetTypeOfNullable();
                enums.Add(default);
            }

            var enumList = Enum.GetValues(enumType).Cast<TEnum>();
            enums.AddRange(enumList);
            return enums;
        }
    }
}
