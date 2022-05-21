using System;
using System.Windows;
using DeskLinkServer.Logic.Configuration;
using DeskLinkServer.Logic.Helpers;
using DeskLinkServer.Logic.Network;
using DeskLinkServer.Logic.Network.Discovery;
using DeskLinkServer.Logic.Protocol;

namespace DeskLinkServer.Logic
{
    public class MainLogic
    {
        public Config Configuration { get; }
        public IServer Server { get; }

        private ProtocolHandler protocolHandler;

        private readonly ServiceDispatcher serviceDispatcher;

        public MainLogic()
        {
            Configuration = ConfigManager.LoadConfig();

            serviceDispatcher = new ServiceDispatcher(Configuration.ServiceName, Configuration.ServicePort);

            Server = new UDPServer(15500);

            //Configuration.KnownDevices.Add(new Device("A00A2239AD42421584E564599FA927FB", "My liebe device"));

            protocolHandler = new ProtocolHandler((message) =>
            {
                byte[] data = message.Data;
                switch (message.MessageType)
                {
                    case MessageType.CursorMove:
                        short dx = (short)((data[0] << 8) + data[1]);
                        short dy = (short)((data[2] << 8) + data[3]);
                        WinAPIHelper.MoveCursor(dx, dy);
                        break;
                    case MessageType.TypeSymbol:
                        WinAPIHelper.SendInput(data[0].ToString());
                        break;
                    case MessageType.TypeSpecialSymbol:
                        string cmd;
                        switch (data[1])
                        {
                            case 0x10:
                                cmd = "{ENTER}";
                                break;
                            case 0x15:
                                cmd = "{BACKSPACE}";
                                break;
                            default:
                                cmd = "";
                                return;
                        }
                        WinAPIHelper.SendInput(cmd);
                        break;
                    case MessageType.LeftClick:
                        WinAPIHelper.MouseEvent(WinAPIHelper.MouseClickEventType.LeftClick);
                        break;
                    case MessageType.RightClick:
                        WinAPIHelper.MouseEvent(WinAPIHelper.MouseClickEventType.RightClick);
                        break;
                    case MessageType.LeftDown:
                        WinAPIHelper.MouseEvent(WinAPIHelper.MouseClickEventType.LeftDown);
                        break;
                    case MessageType.LeftUp:
                        WinAPIHelper.MouseEvent(WinAPIHelper.MouseClickEventType.LeftUp);
                        break;
                }
            }, Configuration.KnownDevices);

            Server.DataReceived += new Action<Message>((message) =>
            {
                protocolHandler.Handle(message, Server);
            });
        }

        public void Start()
        {
            serviceDispatcher.Start();
            Server.Start();
        }

        public void Stop()
        {
            serviceDispatcher.Stop();
            Server.Stop();
            ConfigManager.SaveConfig(Configuration);
        }
    }
}
