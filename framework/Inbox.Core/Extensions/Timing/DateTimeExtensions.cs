using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Core.Extensions.Timing
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 判断当前时间是否周末
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DateTime @this)
        {
            return @this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// 判断当前时间是否工作日
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsWeekday(this DateTime @this)
        {
            return !(@this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday);
        }

        /// <summary>
        /// 判断当前时间是否在范围内
        /// </summary>
        /// <param name="this"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool InRange(this DateTime @this, DateTime minValue, DateTime maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }
    }
}
