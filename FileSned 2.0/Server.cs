using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ByteSizeLib;
using Microsoft.VisualBasic;

namespace FileSned
{
    public static class Server
    {
        public static string StoragePath { get; private set; } = "Storage";

        private static List<Upload> _uploads;
        private static bool _shouldShutdown;
        private static readonly object RenderLock = new object();

        public static void RunServer()
        {
            RunServer("Storage");
        }

        public static void RunServer(string storagePath)
        {
            StoragePath = storagePath;
            _shouldShutdown = false;
            _uploads = new List<Upload>();
            Directory.CreateDirectory(StoragePath);
            Console.Clear();
            var tcpListener = new TcpListener(IPAddress.Any, Program.Port);
            tcpListener.Start();

            Console.WriteLine($"Server is now listening on port {Program.Port}");
            Console.CancelKeyPress += (o, args) => _shouldShutdown = true;
            while (!_shouldShutdown)
            {
                if (tcpListener.Pending()) _uploads.Add(new Upload(tcpListener.AcceptTcpClient()));

                Thread.Sleep(1000);
                Render();
            }

            tcpListener.Stop();
            lock (RenderLock)
            {
                Console.CursorTop = 0;
                Console.WriteLine("Server is shutting down.".PadRight(Console.WindowWidth));
            }

            while (_uploads.Any(t => t.IsRunning))
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

                    var output = new List<string>
                    {
                        $"[{upload.RunningTime:mm\\:ss}]",
                        upload.IpAddress.ToString(),
                        upload.FileName,
                        ByteSize.FromBytes(upload.FileSize).ToString()
                    };
                    if (upload.State == UploadState.Running) output.Add(upload.Progress.ToString("P1"));
                    Console.Write(Strings.Join(output.ToArray()).PadRight(Console.BufferWidth));
                    Console.ResetColor();
                }
            }
        }
    }
}