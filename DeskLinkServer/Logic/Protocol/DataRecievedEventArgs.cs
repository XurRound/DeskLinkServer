using System;

namespace DeskLinkServer.Logic.Protocol
{
    public class DataRecievedEventArgs
    {
        public MessageType? MessageType { get; }
        public DataReader DataReader { get; }
        public bool IsKnownMessage { get; } = true;

        public DataRecievedEventArgs(DataReader dataReader)
        {
            byte messageId = dataReader.ReadBytes(1)[0];
            if (Enum.IsDefined(typeof(MessageType), (int)messageId))
            {
                MessageType = (MessageType)messageId;
                DataReader = dataReader;
            }
            else
                IsKnownMessage = false;
        }
    }
}
