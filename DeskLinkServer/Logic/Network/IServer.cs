using System;
using System.Collections.Generic;
using System.Net;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic.Network
{
    public interface IServer
    {
        event Action ClientConnected;
        event Action ClientDisconnected;
        event Action<Message> DataReceived;
        event Action<string> ErrorOccured;
        event Action<string, string, string, IPEndPoint> RegisterRequest;

        void Send(byte[] data, IPEndPoint endPoint);

        void SetKnownDevices(List<Device> devices);

        void Start();

        void Stop();

        void Restart();
    }
}
