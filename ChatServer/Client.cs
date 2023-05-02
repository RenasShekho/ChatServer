using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    
        public class Client
        {
       // This line declares a new public class called Client.
            public TcpClient TcpClient { get; set; }
            public string Name { get; set; }

        //These two lines define two properties of the Client class, TcpClient and Name.
        public Client(TcpClient tcpClient, string name)
            {
                TcpClient = tcpClient;
                Name = name;
            }
        }
    
}
