using System;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic.Network
{
    public interface IServer
    {
        event Action ClientConnected;
        event Action ClientDisconnected;
        event Action<DataRecievedEventArgs> DataReceived;
        event Action<string> ErrorOccured;

        void Start();

        void Stop();

        void Restart();
    }
}
