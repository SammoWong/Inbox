using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Core.Helpers
{
    public static class CsvHelper
    {
        /// <summary>
        /// 读取csv文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> ReadAsync<T>(string path, string separator) where T : new()
        {
            if (!File.Exists(path) || string.IsNullOrEmpty(separator)) return Enumerable.Empty<T>();

            separator = separator.Trim();

            var lines = await File.ReadAllLinesAsync(path, Encoding.Default);

            if (lines.Length < 2) return Enumerable.Empty<T>();

            var columns = lines.First().Split(separator);

            if (columns.Length == 0) return Enumerable.Empty<T>();

            var items = new List<T>();

            foreach (var line in lines.Skip(1))
            {
                var values = line.Split(separator);

                var item = new T();

                for (var i = 0; i < columns.Length; i++)
                {
                    var property = item.GetType().GetProperty(columns[i]);

                    if (property is null) continue;

                    var type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    var value = Convert.ChangeType(values[i], type, CultureInfo.InvariantCulture);

                    property.SetValue(item, value, default);
                }

                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// 写入csv文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="separator"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static Task WriteAsync<T>(string path, string separator, IEnumerable<T> items)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(separator) || items is null) return Task.CompletedTask;

            separator = separator.Trim();

            var columns = typeof(T).GetProperties().Select(property => property.Name).ToList();

            var lines = new List<string> { string.Join(separator, columns) };

            lines.AddRange(items.Select(item => columns.Select(column => typeof(T).GetProperty(column)?.GetValue(item, null))).Select(values => string.Join(separator, values)));

            return File.WriteAllLinesAsync(path, lines);
        }
    }
}
