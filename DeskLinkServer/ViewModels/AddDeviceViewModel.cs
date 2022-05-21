using DeskLinkServer.Framework.Base;
using DeskLinkServer.Logic;
using DeskLinkServer.Logic.Helpers;
using DeskLinkServer.Services;
using DeskLinkServer.Stores;
using QRCoder;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DeskLinkServer.ViewModels
{
    public class AddDeviceViewModel : BaseViewModel
    {
        public ImageBrush QRCodeImage { get; private set; }

        public ICommand ReturnToDevicesListCommand { get; }

        public AddDeviceViewModel(NavigationStore navigationStore, MainLogic mainLogic)
        {
            ReturnToDevicesListCommand = new RelayCommand((o) =>
            {
                NavigationService.NavigateToDeviceList(navigationStore, mainLogic);
            });
            IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
            string ips = "";
            foreach (IPAddress address in addresses)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    ips += (ips == "" ? "" : ",") + address.ToString();
            }
            Console.WriteLine(ips);
            GenerateQR($"{mainLogic.Configuration.ServiceName}|{DeviceInfo.GetDeviceIdentifier()}|{ips}|{Dns.GetHostName()}");
        }

        private void GenerateQR(string data)
        {
            byte[] code = BitmapByteQRCodeHelper.GetQRCode(data, QRCodeGenerator.ECCLevel.Q, 1);
            using (MemoryStream stream = new MemoryStream(code))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                int stride = image.PixelWidth * 4;
                byte[] pixels = new byte[stride * image.PixelHeight];
                image.CopyPixels(pixels, stride, 0);
                for (int i = 0; i < stride * image.PixelHeight; i += 4)
                {
                    if (pixels[i] == 255)
                    {
                        pixels[i] = 30;
                        pixels[i + 1] = 30;
                        pixels[i + 2] = 30;
                        pixels[i + 3] = 0;
                    }
                    else if (pixels[i] == 0)
                    {
                        pixels[i] = 255;
                        pixels[i + 1] = 255;
                        pixels[i + 2] = 255;
                        pixels[i + 3] = 255;
                    }
                }
                WriteableBitmap bitmap = new WriteableBitmap(image);
                bitmap.WritePixels(new Int32Rect(0, 0, image.PixelWidth, image.PixelHeight), pixels, stride, 0);
                QRCodeImage = new ImageBrush(bitmap);
            }
        }
    }
}
