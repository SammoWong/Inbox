using System;

namespace Inbox.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDescription(this Enum @this) => @this.GetDescription(false);
    }
}
