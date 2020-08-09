using System;
using System.Text;
using ConsoleAppUtil;

namespace FileSned
{
    internal static class Program
    {
        public const int Port = 8000;

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            if (args.Length > 0)
            {
                if (args[0] == "server") Server.RunServer();
            }
            else
            {
                var shutdown = false;
                while (!shutdown)
                {
                    Console.Clear();
                    new Menu(new[]
                    {
                        new MenuItem("Client", Client.RunClient),
                        new MenuItem("Server", Server.RunServer),
                        new MenuItem("Exit", () => shutdown = true)
                    }).ShowMenu();
                }
            }
        }
    }
}