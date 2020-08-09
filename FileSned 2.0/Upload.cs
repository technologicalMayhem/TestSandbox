using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FileSned
{
    public class Upload
    {
        public bool IsRunning => _thread.IsAlive;
        public IPAddress IpAddress => ((IPEndPoint) _client.Client.RemoteEndPoint).Address;
        public UploadState State { get; private set; }
        public TimeSpan RunningTime => IsRunning ? DateTime.Now.Subtract(_startTime) : _endTime;
        public string FileName { get; private set; }
        public long FileSize { get; private set; }

        public double Progress =>
            _fileStream != null && _fileStream.Position != 0
                ? _fileStream.Position / (double) FileSize
                : 0;

        private readonly TcpClient _client;
        private readonly Stream _netStream;
        private readonly DateTime _startTime;
        private readonly Thread _thread;

        private TimeSpan _endTime;
        private Stream _fileStream;

        public Upload(TcpClient client)
        {
            State = UploadState.Running;
            _startTime = DateTime.Now;
            _endTime = TimeSpan.Zero;
            _client = client;
            _netStream = client.GetStream();
            _thread = new Thread(ReceiveFile);
            _thread.Start();
        }

        private void ReceiveFile()
        {
            var nameLength = BitConverter.ToInt32(_netStream.ReadExactly(4));
            FileName = Encoding.UTF8.GetString(_netStream.ReadExactly(nameLength));
            FileSize = BitConverter.ToInt64(_netStream.ReadExactly(8));

            _fileStream = File.OpenWrite(Path.Combine(Server.StoragePath, FileName));
            var copyToAsync = _netStream.CopyToAsync(_fileStream);
            while (!copyToAsync.IsCompleted) Thread.Sleep(100);

            _fileStream.DisposeAsync();
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