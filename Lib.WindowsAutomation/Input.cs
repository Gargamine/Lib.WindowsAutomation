using Lib.WindowsAutomation.Win32Inputs;
using System;
using System.Threading;

namespace Lib.WindowsAutomation
{
    public static class Input
    {
        public static void Send(string value)
        {
            Keyboard.Send(value);
        }

        public static void Send(Keys key)
        {
            switch (key)
            {
                case Keys.TAB:
                    Keyboard.Send(ScanCodeShort.TAB);
                    break;
                case Keys.ENTER:
                    Keyboard.Send(ScanCodeShort.RETURN);
                    break;
                default:
                    throw new NotSupportedException($"Key {key.ToString()} not supported.");
            }
        }

        public static void Click(int x, int y)
        {
            Mouse.SetCursorPosition(x, y);

            Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
            Thread.Sleep(100);
            
            Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);
            Thread.Sleep(100);
        }
    }
}
