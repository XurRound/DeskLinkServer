using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic.Network
{
    public class UDPServer : IServer
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
