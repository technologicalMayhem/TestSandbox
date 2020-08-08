using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ByteSizeLib;
using ConsoleAppUtil;

namespace FileSned
{
    internal class Program
    {
        public const int Port = 8000;

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            if (args.Length > 0)
            {
                if (args[0] == "server")
                {
                    Server.RunServer();
                }
            }
            else
            {
                var shutdown = false;
                while (!shutdown)
                {
                    new Menu(new[]
                    {
                        new MenuItem("Client", Client.UploadFile),
                        new MenuItem("Server", Server.RunServer),
                        new MenuItem("Exit", () => shutdown = true),
                    }).ShowMenu();
                }
            }
        }
    }
}