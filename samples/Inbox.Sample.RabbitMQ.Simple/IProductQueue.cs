using Inbox.RabbitMQ.Simple;

namespace Inbox.Sample.RabbitMQ.Simple
{
    public interface IProductQueue : IQueue<Product>
    {
    }
}
