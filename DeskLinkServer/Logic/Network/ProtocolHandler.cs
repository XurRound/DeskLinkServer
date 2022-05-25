using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using DeskLinkServer.Logic.Protocol;
using System.Text;

namespace DeskLinkServer.Logic.Network
{
    public class ProtocolHandler
    {
        public event Action<Message> OnAuthorizedMessage;
        public event Action<string, string, string, IPEndPoint> OnRegisterRequest;
        public event Action<string> OnAuthSuccess;
        public event Action<string> OnDeviceQuit;

        private readonly IServer server;

        private List<Device> knownDevices;
        private readonly List<IPAddress> authorizedIPs;

        public ProtocolHandler(IServer server)
        {
            authorizedIPs = new List<IPAddress>();
            knownDevices = new List<Device>();
            this.server = server;
        }

        public void SetKnownDevices(List<Device> devices)
        {
            authorizedIPs?.Clear();
            OnDeviceQuit?.Invoke("ALL");
            knownDevices = devices;
        }

        public void Handle(Message message)
        {
            switch (message.MessageType)
            {
                case MessageType.Register:
                    string devName = Encoding.UTF8.GetString(message.Data, 3, message.Data[0]);
                    string devId = Encoding.UTF8.GetString(message.Data, message.Data[0] + 3, message.Data[1]).ToUpper();
                    string myIp = Encoding.UTF8.GetString(message.Data, message.Data[0] + message.Data[1] + 3, message.Data[2]);
                    Console.WriteLine($"On register: {devName}:{devId}|{myIp}");
                    OnRegisterRequest?.Invoke(devId, devName, myIp, message.From);
                    break;
                case MessageType.Auth:
                    byte[] response;
                    string id = BitConverter.ToString(message.Data).Replace("-", "");
                    Console.WriteLine($"On auth: {id}");
                    if (knownDevices.Where((d) => d.Identifier == id).SingleOrDefault() != null)
                    {
                        response = new byte[] { 0x77, 0xFF };
                        authorizedIPs.Add(message.From.Address);
                        OnAuthSuccess?.Invoke(id);
                    }
                    else
                        response = new byte[] { 0x77, 0x01 };
                    server.Send(response, message.From);
                    break;
                case MessageType.Quit:
                    string id1 = BitConverter.ToString(message.Data).Replace("-", "");
                    OnDeviceQuit?.Invoke(id1);
                    break;
                default:
                    if (authorizedIPs.Contains(message.From.Address))
                        OnAuthorizedMessage?.Invoke(message);
                    break;
            }
        }
    }
}
