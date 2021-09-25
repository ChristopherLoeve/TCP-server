using System;

namespace TCP_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!\n");

            Server server = new Server();
            server.Start();
        }
    }
}
