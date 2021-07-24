using System;

namespace Inbox.RabbitMQ.Simple
{
    public class Connection
    {
        public Connection(string hostName, int port, string userName, string password)
        {
            HostName = hostName;
            Port = port;
            UserName = userName;
            Password = password;
        }

        public string HostName { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
