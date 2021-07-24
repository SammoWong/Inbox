using Inbox.RabbitMQ.Simple;

namespace Inbox.Sample.RabbitMQ.Simple
{
    public class ProductQueue : Queue<Product>, IProductQueue
    {
        public ProductQueue(Connection connection) : base(connection)
        {
        }
    }
}
