using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic.Network
{
    public class TCPServer : IServer
    {
        public event Action ClientConnected;
        public event Action ClientDisconnected;
        public event Action<DataRecievedEventArgs> DataReceived;
        public event Action<string> ErrorOccured;

        private readonly TcpListener listener;

        private readonly Thread workingThread;

        private CancellationTokenSource ctSource;

        public TCPServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            ctSource = new CancellationTokenSource();
            workingThread = new Thread(() =>
            {
                try
                {
                    while (!ctSource.IsCancellationRequested)
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        ClientConnected?.Invoke();
                        new Thread(() =>
                        {
                            new TCPDevice(client, (dataReader) =>
                            {
                                DataReceived?.Invoke(new DataRecievedEventArgs(dataReader));
                            }, () =>
                            {
                                ClientDisconnected?.Invoke();
                            }).Communicate();
                        }).Start();
                    }
                }
                catch (Exception e)
                {
                    ErrorOccured?.Invoke(e.Message);
                }
                finally
                {
                    listener.Stop();
                }
            });
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Start()
        {
            listener.Start();
            ctSource = new CancellationTokenSource();
            workingThread.Start();
        }
        public void Stop()
        {
            ctSource.Cancel();
            listener.Stop();
            workingThread.Interrupt();
        }
    }
}
