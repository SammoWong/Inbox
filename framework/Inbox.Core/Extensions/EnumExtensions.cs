using System;
using System.Threading.Tasks;

namespace Inbox.Core.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum @this) => @this.GetDescription(false);


        public static string ToDisplayName(this Enum @this) => @this.GetDisplayName(false);

        /// <summary>
        /// 将具有整数值的指定对象转换为枚举成员
        /// @this 不是 System.SByte、System.Int16、System.Int32、System.Int64、System.Byte、System.UInt16、System.UInt32 和 System.UInt64 类型。
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this object @this) where TEnum : Enum
        {
            var type = typeof(TEnum);

            if (@this == null && type.IsNullableType())
                return (TEnum)@this;

            if (type.IsNullableType())
                type = type.GetGenericArguments()[0];

            return (TEnum)Enum.ToObject(type, @this);
        }
    }
}
