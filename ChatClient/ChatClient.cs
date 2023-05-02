using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class ChatClient
    {

        TcpClient Connection;
        const int PORT = 1337;
        public void SendMessage(string message)
        {
            if (Connection.Connected)
            {
                NetworkStream stream = Connection.GetStream(); // Get the network stream to send data to the server
                byte[] bytes = Encoding.UTF8.GetBytes(message); // Encode the string message as a byte array
                stream.Write(bytes, 0, bytes.Length); // Send the message to the server
            }
        }
        public void Connect(string ip, int port)
        {

            Connection = new TcpClient(); // Create a new instance of TcpClient
            Thread.Sleep(2000); // Wait for 2 seconds before attempting to connect
            Connection.Connect(ip, port); // Connect to the server with the specified IP and port number
            Thread thread = new Thread(Listen); // Create a new thread to listen for incoming messages

            thread.Start();// Start the thread

        }
        //public void Recieve() 
        //{

        //}
        public void Listen()
        {
            NetworkStream stream = Connection.GetStream(); // Get the network stream to receive data from the server
            byte[] buffer = new byte[4096]; // Create a buffer to store the incoming data
            while (Connection.Connected) // Keep listening while the connection is still active
            {
                int read = stream.Read(buffer, 0, buffer.Length); // Read the incoming data from the network stream
                string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read); // Convert the byte array to a string
                Console.WriteLine(messageFromServer); // Print the message to the console
                buffer = new byte[4096]; // Clear the buffer to receive new incoming data
            }
        }
        public void MainPage()
        {
            Console.ForegroundColor = ConsoleColor.Green; 
            Console.WriteLine("Start Chat. ");
            Console.ForegroundColor = ConsoleColor.Yellow; 
        }

        public void Run()
        {
            Connect("127.0.0.1", PORT); // Connect to the server with the specified IP and port number

            while (true) // Keep running until the program is terminated
            {
                string send = Console.ReadLine(); // Read a message from the console input
                SendMessage(send); // Send the message to the server
            }
        }

    }
}
