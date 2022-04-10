using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace DeskLinkServer.Logic.Network.Discovery
{
    public class ServiceDispatcher
    {
        private readonly UdpClient udpClient;

        private readonly CancellationTokenSource ctSource;

        private readonly byte[] greeting = Encoding.UTF8.GetBytes("DeskLink|1.0");

        private readonly string serviceName;

        private readonly Thread workingThread;

        public ServiceDispatcher(string serviceName, int servicePort)
        {
            this.serviceName = serviceName;

            ctSource = new CancellationTokenSource();

            udpClient = new UdpClient(servicePort, AddressFamily.InterNetwork)
            {
                EnableBroadcast = true,
            };

            workingThread = new Thread(() =>
            {
                while (!ctSource.IsCancellationRequested)
                {
                    try
                    {
                        IPEndPoint endp = new IPEndPoint(IPAddress.Any, servicePort);
                        byte[] buffer = udpClient.Receive(ref endp);
                        HandleMessage(endp, buffer);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            });
        }

        private void HandleMessage(IPEndPoint endPoint, byte[] data)
        {
            Console.WriteLine("Got data: " + Encoding.UTF8.GetString(data));

            if (data.SequenceEqual(greeting))
            {
                string localIP;
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP))
                {
                    socket.Connect(endPoint.Address, endPoint.Port);
                    localIP = (socket.LocalEndPoint as IPEndPoint).Address.ToString();
                    socket.Close();
                }
                if (localIP != null)
                {
                    string reportStr = $"DLS|{serviceName}|{localIP}|{Dns.GetHostName()}";
                    byte[] report = Encoding.UTF8.GetBytes(reportStr);
                    udpClient.Send(report, report.Length, endPoint);

                    Console.WriteLine($"Report sent: '{reportStr}'");
                }
            }
        }

        public void Start()
        {
            workingThread.Start();
        }

        public void Stop()
        {
            ctSource.Cancel();
            workingThread.Interrupt();
            udpClient.Close();
        }
    }
}
