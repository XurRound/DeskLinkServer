using System;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic.Network
{
    public interface IServer
    {
        public event Action<string> ClientConnected;
        public event Action<string> ClientDisconnected;
        public event Action<DataRecievedEventArgs> DataReceived;
        public event Action<string> ErrorOccured;

        void Start();

        void Stop();

        void Restart();
    }
}
