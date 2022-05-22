using System;
using System.Linq;
using System.Text;
using System.Windows;
using DeskLinkServer.Logic.Configuration;
using DeskLinkServer.Logic.Helpers;
using DeskLinkServer.Logic.Network;
using DeskLinkServer.Logic.Network.Discovery;
using DeskLinkServer.Logic.Protocol;
using DeskLinkServer.Services;
using DeskLinkServer.Stores;

namespace DeskLinkServer.Logic
{
    public class MainLogic
    {
        public Config Configuration { get; }
        public IServer Server { get; }

        private readonly ServiceDispatcher serviceDispatcher;

        public MainLogic(NavigationStore navigation)
        {
            Configuration = ConfigManager.LoadConfig();

            serviceDispatcher = new ServiceDispatcher(Configuration.ServiceName, Configuration.ServicePort);

            Server = new UDPServer(15500, Configuration.KnownDevices);

            Server.DataReceived += new Action<Message>((message) =>
            {
                byte[] data = message.Data;
                switch (message.MessageType)
                {
                    case MessageType.CursorMove:
                        short dx = (short)((data[0] << 8) + data[1]);
                        short dy = (short)((data[2] << 8) + data[3]);
                        WinAPIHelper.MoveCursor(dx, dy);
                        break;
                    case MessageType.TypeText:
                        WinAPIHelper.SendInput(Encoding.UTF8.GetString(data), true);
                        break;
                    case MessageType.TypeSpecialSymbol:
                        string cmd = "";
                        switch (data[0])
                        {
                            case 0x08:
                                cmd = "{BACKSPACE}";
                                break;
                            case 0x0D:
                                cmd = "{ENTER}";
                                break;
                        }
                        if (cmd != "")
                            WinAPIHelper.SendInput(cmd, false);
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
            });
            Server.RegisterRequest += (devId, devName, endPoint) =>
            {
                if (Configuration.KnownDevices.Where(d => d.Identifier == devId).Count() != 0)
                {
                    Server.Send(new byte[] { 0x77, 0x01 }, endPoint);
                    return;
                }
                MessageBoxResult result = MessageBox.Show(
                    $"Добавить [{devName}] в список доверенных устройств?",
                    "Подключение нового устройства",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );
                if (result == MessageBoxResult.Yes)
                {
                    Configuration.KnownDevices.Add(new Device(devId, devName));
                    Server.Send(new byte[] { 0x77, 0x01 }, endPoint);
                    NavigationService.NavigateToDeviceList(navigation, this);
                }
                else
                    Server.Send(new byte[] { 0x77, 0xFF }, endPoint);
            };
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
