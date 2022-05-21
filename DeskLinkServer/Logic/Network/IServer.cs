using System;
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

        void Send(byte[] data, IPEndPoint endPoint);

        void Start();

        void Stop();

        void Restart();
    }
}
