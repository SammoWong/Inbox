using AgileObjects.AgileMapper;

namespace Inbox.Mapping
{
    public static class MappingExtensions
    {
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource Clone<TSource>(this TSource source)
        {
            return Mapper.DeepClone(source);
        }

        /// <summary>
        /// 将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget Map<TSource , TTarget>(this TSource source)
        {
            return Mapper.Map(source).ToANew<TTarget>();
        }

        /// <summary>
        /// 将对象映射为指定类型
        /// </summary>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget Map<TTarget>(this object source)
        {
            return Mapper.Map(source).ToANew<TTarget>();
        }

        /// <summary>
        /// 使用源类型的对象更新目标类型的对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static TTarget Map<TSource, TTarget>(this TSource source, TTarget target)
        {
            return Mapper.Map(source).OnTo(target);
        }

        /// <summary>
        /// 将数据源映射为指定类型
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public static IQueryable<TTarget> Project<TSource, TTarget>(this IQueryable<TSource> queryable)
        {
            return queryable.Project().To<TTarget>();
        }
    }
}