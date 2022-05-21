using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic.Network
{
    public class ProtocolHandler
    {
        private List<IPAddress> authorizedIPs;

        private List<Device> knownDevices;

        private event Action<Message> onAuthorizedMessage;

        public ProtocolHandler(Action<Message> onAuthorizedMessage, List<Device> knownDevices)
        {
            authorizedIPs = new List<IPAddress>();
            this.onAuthorizedMessage = onAuthorizedMessage;
            this.knownDevices = knownDevices;
        }

        public void Handle(Message message, IServer server)
        {
            if (message.MessageType == MessageType.Auth)
            {
                byte[] response;
                string id = BitConverter.ToString(message.Data).Replace("-", "");
                if (knownDevices.Where((d) => d.Identifier == id).SingleOrDefault() != null)
                {
                    response = new byte[] { 0x77, 0xFF };
                    authorizedIPs.Add(message.From.Address);
                }
                else
                    response = new byte[] { 0x77, 0x01 };
                server.Send(response, message.From);
            }
            else
            {
                if (authorizedIPs.Contains(message.From.Address))
                    onAuthorizedMessage?.Invoke(message);
            }
        }
    }
}
