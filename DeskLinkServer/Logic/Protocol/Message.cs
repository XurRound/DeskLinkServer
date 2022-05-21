using System;
using System.Net;

namespace DeskLinkServer.Logic.Protocol
{
    public class Message
    {
        public MessageType MessageType { get; }

        public byte[] Data { get; }

        public IPEndPoint From { get; }

        public Message(byte[] data, IPEndPoint from)
        {
            if (!Enum.IsDefined(typeof(MessageType), (int)data[0]))
                throw new Exception("Unknown message received");
            MessageType = (MessageType)data[0];
            Data = new byte[data.Length - 1];
            From = from;
            Array.Copy(data, 1, Data, 0, data.Length - 1);
        }
    }
}
