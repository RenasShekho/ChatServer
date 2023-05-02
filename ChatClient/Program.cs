using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new instance of the ChatClient class.
            ChatClient client = new ChatClient();
            // Call the MainPage method of the ChatClient object,
            client.MainPage();
            client.Run();
          
        }
    }
}
