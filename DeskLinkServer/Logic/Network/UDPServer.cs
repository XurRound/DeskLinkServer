using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic.Network
{
    public class UDPServer : IServer
    {
        public event Action ClientConnected;
        public event Action ClientDisconnected;
        public event Action<Message> DataReceived;
        public event Action<string> ErrorOccured;

        private readonly UdpClient udpClient;

        private CancellationTokenSource ctSource;

        public UDPServer(int port)
        {
            udpClient = new UdpClient(port, AddressFamily.InterNetwork);
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Start()
        {
            ctSource = new CancellationTokenSource();
            new Thread(() =>
            {
                IPEndPoint remote = null;
                while(!ctSource.IsCancellationRequested)
                {
                    try
                    {
                        byte[] data = udpClient?.Receive(ref remote);
                        DataReceived?.Invoke(new Message(data, remote));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }).Start();
        }

        public void Send(byte[] data, IPEndPoint endPoint)
        {
            udpClient?.Send(data, data.Length, endPoint);
        }

        public void Stop()
        {
            ctSource.Cancel();
            udpClient?.Close();
        }
    }
}
