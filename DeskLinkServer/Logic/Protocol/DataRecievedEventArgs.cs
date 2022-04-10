using System;

namespace DeskLinkServer.Logic.Protocol
{
    public class DataRecievedEventArgs
    {
        public MessageType? MessageType { get; }
        public DataReader DataReader { get; }
        public bool IsKnownMessage { get; } = true;

        public DataRecievedEventArgs(int messageId, DataReader dataReader)
        {
            if (!Enum.IsDefined(typeof(MessageType), messageId))
            {
                MessageType = (MessageType)messageId;
                DataReader = dataReader;
            }
            else
                IsKnownMessage = false;
        }
    }
}
