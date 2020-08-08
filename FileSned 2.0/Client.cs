using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ConsoleAppUtil;

namespace FileSned
{
    public static class Client
    {
        public static void UploadFile()
        {
            var readLine = UserInput.GetInput("Give filename", File.Exists);
            var fileIn = File.OpenRead(readLine);
            var fileSize = new FileInfo(readLine).Length;
            var address = IPAddress.None;
            UserInput.GetInput("Give address", s => IPAddress.TryParse(s, out address));
            var tcpClient = new TcpClient(address.ToString(), Program.Port);
            SpinWait.SpinUntil(() => tcpClient.Connected);
            var task = fileIn.CopyToAsync(tcpClient.GetStream());
            while (!task.IsCompletedSuccessfully)
            {
                Console.WriteLine((fileIn.Position / (double) fileSize).ToString("P1"));
                Thread.Sleep(100);
            }

            tcpClient.Close();
            Console.WriteLine("Transfer done!");
        }
    }
}