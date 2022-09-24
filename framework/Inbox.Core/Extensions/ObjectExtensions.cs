using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace Inbox.Core.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T DeepClone<T>(this T @this)
        {
            using (var stream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stream, @this);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)xmlSerializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// 获取对象DescriptionAttribute特性的Description
        /// </summary>
        /// <param name="this"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static string GetDescription(this object @this, bool inherit = false)
        {
            var attr = @this.GetType().GetCustomAttribute<DescriptionAttribute>(inherit);
            if (attr == null)
                return string.Empty;

            return attr.Description;
        }

        /// <summary>
        /// 获取对象DisplayAttribute特性的Name
        /// </summary>
        /// <param name="this"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static string GetDisplayName(this object @this, bool inherit = false)
        {
            var attr = @this.GetType().GetCustomAttribute<DisplayAttribute>(inherit);
            if (attr == null)
                return string.Empty;

            return attr.Name;
        }
    }
}
