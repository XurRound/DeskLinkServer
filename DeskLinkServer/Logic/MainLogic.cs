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

        private readonly ServiceDispatcher serviceDispatcher;

        public MainLogic()
        {
            Configuration = ConfigManager.LoadConfig();

            serviceDispatcher = new ServiceDispatcher(Configuration.ServiceName, Configuration.ServicePort);

            Server = new TCPServer(15500);

            RegisterCommands();
        }

        private void RegisterCommands()
        {
            Server.DataReceived += new Action<DataRecievedEventArgs>((e) =>
            {
                DataReader reader = e.DataReader;
                if (e.IsKnownMessage)
                {
                    byte[] data;
                    switch (e.MessageType)
                    {
                        case MessageType.Auth:
                            MessageBox.Show("LOL");
                            break;
                        case MessageType.CursorMove:
                            data = reader.ReadBytes(4);
                            short dx = (short)((data[0] << 8) + data[1]);
                            short dy = (short)((data[2] << 8) + data[3]);
                            WinAPIHelper.MoveCursor(dx, dy);
                            Console.WriteLine("W: " + DateTime.Now.Millisecond);
                            break;
                        case MessageType.TypeSymbol:
                            data = reader.ReadBytes(1);
                            WinAPIHelper.SendInput(data[0].ToString());
                            break;
                        case MessageType.TypeSpecialSymbol:
                            data = reader.ReadBytes(1);
                            string cmd;
                            switch (data[0])
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
                }
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
