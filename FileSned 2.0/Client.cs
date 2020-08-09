using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ConsoleAppUtil;

namespace FileSned
{
    public static class Client
    {
        public static void RunClient()
        {
            var (filePath, address) = GetUserInput();
            TransmitFiles(address, filePath);
        }

        public static void TransmitFiles(string address, string filePath)
        {
            var tcpClient = new TcpClient();
            tcpClient.Connect(address, Program.Port);

            //Todo: Add error handling
            Console.Clear();
            Console.WriteLine($"Uploading: {new FileInfo(filePath).Name}");
            UploadFile(tcpClient.GetStream(), filePath);

            tcpClient.Close();
            Console.WriteLine("Transfer done!");
        }

        private static (string filePath, string address) GetUserInput()
        {
            var filePath = UserInput.GetInput("Give filename", File.Exists);
            var address = UserInput.GetInput("Give address", s => IPAddress.TryParse(s, out _));
            return (filePath, address);
        }

        private static void UploadFile(Stream uploadTarget, string filePath)
        {
            var info = new FileInfo(filePath);
            var fileStream = File.OpenRead(filePath);

            var encodedName = Encoding.UTF8.GetBytes(info.Name);
            var encodedNameLength = BitConverter.GetBytes(encodedName.Length);
            var encodedFileLength = BitConverter.GetBytes(info.Length);
            var header = encodedNameLength.Concat(encodedName).Concat(encodedFileLength).ToArray();

            uploadTarget.Write(header);
            var task = fileStream.CopyToAsync(uploadTarget);

            while (!task.IsCompletedSuccessfully)
            {
                Console.WriteLine((fileStream.Position / (double) info.Length).ToString("P1"));
                Console.CursorTop--;
                Thread.Sleep(100);
            }
        }
    }
}