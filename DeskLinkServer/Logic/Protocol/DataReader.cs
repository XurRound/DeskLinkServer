using System.Net.Sockets;

namespace DeskLinkServer.Logic.Protocol
{
    public class DataReader
    {
        private readonly NetworkStream stream;

        public DataReader(NetworkStream stream)
        {
            this.stream = stream;
        }

        public NetworkStream GetStream()
        {
            return stream;
        }

        public byte[] ReadBytes(int count)
        {
            byte[] buffer = new byte[count];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}
