using System.Runtime.InteropServices;

namespace AsKeyIndicator
{
    public static class NativeMethods
    {
        public const byte VkNumlock = 0x90;
        private const uint KeyeventfExtendedkey = 1;
        private const int KeyeventfKeyup = 0x2;

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        public static void SimulateKeyPress(byte keyCode)
        {
            keybd_event(VkNumlock, 0x45, KeyeventfExtendedkey, 0);
            keybd_event(VkNumlock, 0x45, KeyeventfExtendedkey | KeyeventfKeyup, 0);
        }
    }
}
