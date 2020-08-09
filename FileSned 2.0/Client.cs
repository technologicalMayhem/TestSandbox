using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleAppUtil;

namespace FileSned
{
    public static class Client
    {
        public static void RunClient()
        {
            var filePath = UserInput.GetInput("Give filename", File.Exists);
            var address = IPAddress.None;
            UserInput.GetInput("Give address", s => IPAddress.TryParse(s, out address));
            
            var tcpClient = new TcpClient(address.ToString(), Program.Port);
            SpinWait.SpinUntil(() => tcpClient.Connected);
            
            //Todo: Add error handling
            Console.Clear();
            Console.WriteLine($"Uploading: {new FileInfo(filePath).Name}");
            UploadFile(tcpClient.GetStream(), filePath);

            tcpClient.Close();
            Console.WriteLine("Transfer done!");
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