using System;

namespace ChatClient2
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatClient2 client = new ChatClient2();
            client.Connect("127.0.0.1", 1337);
            client.MainPage();
            client.Run();
        }
    }
}
