using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FileSned
{
    public class Upload
    {
        private readonly TcpClient _client;
        private readonly DateTime _startTime;
        private readonly Thread _thread;

        private TimeSpan _endTime;

        public Upload(TcpClient client)
        {
            FileName = Guid.NewGuid().ToString();
            State = UploadState.Running;
            _startTime = DateTime.Now;
            _endTime = TimeSpan.Zero;
            _client = client;
            _thread = new Thread(() => ReceiveFile(_client));
            _thread.Start();
        }

        public bool IsRunning => _thread.IsAlive;
        public IPAddress IpAddress => ((IPEndPoint) _client.Client.RemoteEndPoint).Address;
        public UploadState State { get; private set; }
        public TimeSpan RunningTime => IsRunning ? DateTime.Now.Subtract(_startTime) : _endTime;
        public string FileName { get; }

        private void ReceiveFile(TcpClient client)
        {
            var write = File.OpenWrite(FileName);
            var copyToAsync = client.GetStream().CopyToAsync(write);
            while (!copyToAsync.IsCompleted) Thread.Sleep(100);

            write.DisposeAsync();
            if (copyToAsync.IsFaulted)
            {
                State = UploadState.Failed;
                File.Delete(FileName);
            }
            else
            {
                State = UploadState.Success;
            }

            _endTime = DateTime.Now.Subtract(_startTime);
        }
    }

    public enum UploadState
    {
        Running,
        Success,
        Failed
    }
}