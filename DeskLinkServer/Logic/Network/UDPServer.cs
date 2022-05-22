using System;
using System.Collections.Generic;
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
        public event Action<string, string, IPEndPoint> RegisterRequest;

        private readonly UdpClient udpClient;

        private CancellationTokenSource ctSource;

        private ProtocolHandler protocolHandler;

        public UDPServer(int port, List<Device> knownDevices)
        {
            udpClient = new UdpClient(port, AddressFamily.InterNetwork);

            protocolHandler = new ProtocolHandler(this, knownDevices);
            protocolHandler.OnAuthorizedMessage += ((msg) =>
            {
                DataReceived?.Invoke(msg);
            });
            protocolHandler.OnAuthSuccess += ((devName) =>
            {
                ClientConnected?.Invoke();
            });
            protocolHandler.OnRegisterRequest += ((devId, devName, endPoint) =>
            {
                RegisterRequest?.Invoke(devId, devName, endPoint);
            });
            protocolHandler.OnDeviceQuit += ((devName) =>
            {
                ClientDisconnected?.Invoke();
            });
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
                        protocolHandler.Handle(new Message(data, remote));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(e.StackTrace);
                        ErrorOccured?.Invoke(e.Message);
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
