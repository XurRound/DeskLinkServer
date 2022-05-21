using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace DeskLinkServer.Logic.Helpers
{
    public class DeviceInfo
    {
        private static string WmiQuery(string what, string from)
        {
            foreach (ManagementObject entry in (new ManagementObjectSearcher($"Select {what} From {from}")).Get())
                return entry[what].ToString().Trim();
            return "stub";
        }

        public static string GetDeviceIdentifier()
        {
            string hwInfo = "";
            hwInfo += WmiQuery("Manufacturer", "Win32_processor");
            hwInfo += WmiQuery("Name",         "Win32_processor");
            hwInfo += WmiQuery("Manufacturer", "Win32_BaseBoard");
            hwInfo += WmiQuery("Product",      "Win32_BaseBoard");
            hwInfo += WmiQuery("SerialNumber", "Win32_BaseBoard");
            hwInfo += WmiQuery("Manufacturer", "Win32_BIOS");
            hwInfo += WmiQuery("Version",      "Win32_BIOS");
            hwInfo += WmiQuery("SerialNumber", "Win32_BIOS");
            using (SHA1 shaHasher = SHA1.Create())
                return BytesToHexString(shaHasher.ComputeHash(Encoding.UTF8.GetBytes(hwInfo)));
        }

        private static string BytesToHexString(byte[] data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in data)
                stringBuilder.AppendFormat("{0:x2}", b);
            return stringBuilder.ToString();
        }
    }
}
