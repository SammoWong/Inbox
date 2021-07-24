namespace Inbox.Sample.RabbitMQ.Simple
{
    public class Product
    {
        public Product(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
