using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient2
{
    internal class ChatClient2
    {
        TcpClient Connection;
        const int PORT = 1337;
        public void SendMessage(string message)
        {
            if (Connection.Connected)
            {
                NetworkStream stream = Connection.GetStream();
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                stream.Write(bytes, 0, bytes.Length);
                //stream.Close();
            }
        }
        public void Connect(string ip, int port)
        {

            Connection = new TcpClient();
            Thread.Sleep(2000);
            Connection.Connect(ip, port);
            Thread thread = new Thread(Listen);

            thread.Start();

        }
        //public void Recieve() 
        //{

        //}
        public void Listen()
        {
            NetworkStream stream = Connection.GetStream();
            byte[] buffer = new byte[4096];
            while (Connection.Connected)
            {
                int read = stream.Read(buffer, 0, buffer.Length);
                string messageFromServer = Encoding.UTF8.GetString(buffer, 0, read);
                Console.WriteLine(messageFromServer);
                buffer = new byte[4096]; // clear buffer
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
            Connect("127.0.0.1", PORT);

            while (true)
            {
                string send = Console.ReadLine();

                SendMessage(send);

            }
        }

    }
}
