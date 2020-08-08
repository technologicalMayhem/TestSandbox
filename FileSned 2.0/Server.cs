using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FileSned
{
    public class Server
    {
        
        private static List<Upload> _uploads;
        private static bool _shouldShutdown;
        private static readonly object RenderLock = new object();
        
        public static void RunServer()
        {
            _shouldShutdown = false;
            _uploads = new List<Upload>();
            Console.Clear();
            var tcpListener = new TcpListener(IPAddress.Any, Program.Port);
            tcpListener.Start();
            Console.WriteLine($"Server is now listening on port {Program.Port}");
            Console.CancelKeyPress += (o, args) => _shouldShutdown = true;
            while (!_shouldShutdown)
            {
                if (tcpListener.Pending())
                {
                    _uploads.Add(new Upload(tcpListener.AcceptTcpClient()));
                }

                Thread.Sleep(1000);
                Render();
            }

            tcpListener.Stop();
            lock (RenderLock)
            {
                Console.CursorTop = 0;
                Console.WriteLine("Server is shutting down.".PadRight(Console.WindowWidth));
            }

            while (_uploads.All(t => !t.IsRunning))
            {
                Render();
                Thread.Sleep(1000);
            }
        }

        private static void Render()
        {
            var elementsToDisplay = Console.WindowHeight - 2;
            var uploads = _uploads.Count > elementsToDisplay
                ? _uploads.Skip(_uploads.Count - elementsToDisplay)
                : _uploads.ToArray();
            lock (RenderLock)
            {
                Console.CursorTop = 2;
                foreach (var upload in uploads)
                {
                    switch (upload.State)
                    {
                        case UploadState.Running:
                            break;
                        case UploadState.Success:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case UploadState.Failed:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    Console.Write(
                        $"[{upload.RunningTime:mm\\:ss}] {upload.IpAddress} -> {upload.FileName}"
                            .PadRight(Console.BufferWidth));
                    Console.ResetColor();
                }
            }
        }
    }
}