using System;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic.Network
{
    public class BluetoothServer : IServer
    {
        public event Action<string> ClientConnected;
        public event Action<string> ClientDisconnected;
        public event Action<DataRecievedEventArgs> DataReceived;
        public event Action<string> ErrorOccured;

        public void Restart()
        {
            
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}
