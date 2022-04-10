using InTheHand.Net.Bluetooth;

namespace DeskLinkServer.Logic.Helpers
{
    public static class BluetoothHelper
    {
        public static bool IsEnabled()
        {
            return BluetoothRadio.Default != null;
        }

        public static string GetRadioName()
        {
            return IsEnabled() ? BluetoothRadio.Default.Name : "Unknown";
        }
    }
}
