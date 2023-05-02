using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatServer
{
    internal class ChatServer
    {    //  constant integer called PORT with a value of 1337
        const int PORT = 1337;

        List<Client> clients = new List<Client>();
        // define a public method called Start that does not return anything
        public void Start()
        {    // create a new TcpListener object listening on IP address "0.0.0.0" and PORT
            TcpListener listner = new TcpListener(IPAddress.Parse("0.0.0.0"), PORT);

            // start the TcpListener object
            listner.Start();
            Console.WriteLine($"Server: Lytter på port{PORT}");
            while (true)
            {   // accept an incoming TCP client connection
                TcpClient tcpClient = listner.AcceptTcpClient();

                // assign a name to the client based on the number of clients currently connected
                string name = $"Client {clients.Count + 1}";

                // create a new Client object with the TcpClient and name
                Client client = new Client(tcpClient, name);

                // add the client to the list of clients
                clients.Add(client);

                // create a new thread to handle the client's messages
                Thread thread = new Thread(() =>
                {

                    while (true)
                    {
                        // receive a message from the client
                        string message = Recieve(client.TcpClient);
                        // broadcast the message to all clients except the sender
                        Broadcast(message, client);
                    }
                });
                thread.Start();
            }
        }
        // define a public method called Recieve that takes a TcpClient as input and returns a string
        public string Recieve(TcpClient tcpClient)
        {   
            // get the network stream from the TcpClient
            NetworkStream stream = tcpClient.GetStream();
            // create a byte array of size 4096 to store the message
            byte[] buffer = new byte[4096];
            // read the message into the buffer and get the number of bytes read
            int read = stream.Read(buffer, 0, buffer.Length);
            // convert the bytes to a string
            string recieve = Encoding.UTF8.GetString(buffer, 0, read);
            Console.WriteLine($"{GetClientName(tcpClient)} wrote: " + recieve);
            // return the received message
            return recieve;
        }
        // define a public method called Broadcast that takes a message and a sender Client as input and does not return anything
        public void Broadcast(string message, Client sender)
        {     
            // convert the message to bytes
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            // loop through all clients
            foreach (Client client in clients)
            {       
                // check if the client is connected and not the sender
                if (client.TcpClient.Connected && !client.TcpClient.Equals(sender.TcpClient))
                {   // get the network stream for the client
                    NetworkStream Stream = client.TcpClient.GetStream();
                    // write the message to the stream
                    Stream.Write(bytes, 0, bytes.Length);
                }
            }

        }
        // define a public method called GetClientName that takes a TcpClient as input and returns a string
        public string GetClientName(TcpClient tcpClient)

        {    // Find the Client object in the list of clients whose TcpClient object matches the provided TcpClient object.
            Client client = clients.Find(c => c.TcpClient.Equals(tcpClient));
            // If a matching Client object was found, return its Name property.
            // Otherwise, return a string indicating that the client is unknown and include the hash code of the provided TcpClient object.
            return client != null ? client.Name : $"Unknown Client ({tcpClient.GetHashCode()})";
        }

       

    }


}
