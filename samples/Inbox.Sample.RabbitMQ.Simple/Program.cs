using Inbox.RabbitMQ.Simple;
using System;

namespace Inbox.Sample.RabbitMQ.Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            var product = new Product("Product");

            //发布
            Publish(product);

            //订阅
            Subscribe(product);

            Console.ReadKey();
        }

        static void Publish(Product product)
        {
            IProductQueue productQueue = new ProductQueue(new Connection("localhost", 5672, "guest", "guest"));
            productQueue.Publish(product);
        }

        static void Subscribe(Product product)
        {
            IProductQueue productQueue = new ProductQueue(new Connection("localhost", 5672, "guest", "guest"));
            productQueue.Subscibe(product => Handle(product));
        }

        static void Handle(Product product)
        {
            Console.WriteLine("Product Name : " + product.Name);
        }
    }
}
