using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;

namespace Inbox.RabbitMQ.Simple
{
    public class Queue<T> : IQueue<T>
    {
        private readonly Connection _connection;
        private readonly string _queue = typeof(T).Name;

        public Queue(Connection connection)
        {
            _connection = connection;
        }

        public void Publish(T obj)
        {
            Connect(channel => channel.BasicPublish(string.Empty, _queue, null, Encoding.Default.GetBytes(JsonSerializer.Serialize(obj))));
        }

        public void Subscibe(Action<T> action)
        {
            Connect(channel =>
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (_, args) => action(JsonSerializer.Deserialize<T>(Encoding.Default.GetString(args.Body.ToArray())));
                channel.BasicConsume(_queue, true, consumer);
            });
        }

        private void Connect(Action<IModel> action)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = _connection.HostName,
                Port = _connection.Port,
                UserName = _connection.UserName,
                Password = _connection.Password
            };

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(_queue, true, false);

            action(channel);
        }
    }
}
