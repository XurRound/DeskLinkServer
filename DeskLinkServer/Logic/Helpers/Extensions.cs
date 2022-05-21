using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace DeskLinkServer.Logic.Helpers
{
    public static class Extensions
    {
        public static TcpState GetState(this TcpClient tcpClient)
        {
            var foo = IPGlobalProperties.GetIPGlobalProperties()
              .GetActiveTcpConnections()
              .SingleOrDefault(x => x.RemoteEndPoint.Equals(tcpClient.Client.RemoteEndPoint));
            return foo != null ? foo.State : TcpState.Unknown;
        }
    }
}
