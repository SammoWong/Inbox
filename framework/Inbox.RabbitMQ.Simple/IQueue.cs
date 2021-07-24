using System;

namespace Inbox.RabbitMQ.Simple
{
    public interface IQueue<T>
    {
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="obj"></param>
        void Publish(T obj);

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="action"></param>
        void Subscibe(Action<T> action);
    }
}
