using QRCoder;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DeskLinkServer.Views
{
    public partial class AddDeviceView : UserControl
    {
        public AddDeviceView()
        {
            InitializeComponent();
            GenerateQR();
        }

        private void GenerateQR()
        {
            string data = "testdatatestdatatestdatatestdatatestdatatestdatatestdatatestdatatestdata";
            byte[] code = BitmapByteQRCodeHelper.GetQRCode(data, QRCodeGenerator.ECCLevel.Q, 2);
            using (MemoryStream stream = new MemoryStream(code))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.NearestNeighbor);
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
                img.Background = new ImageBrush(bitmap);
            }
        }
    }
}
