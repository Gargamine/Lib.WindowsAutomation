using RCGT.WindowsAutomation.Win32Inputs;
using System;

namespace RCGT.WindowsAutomation
{
    public class Input
    {
        public void Send(string value)
        {
            Keyboard.Send(value);
        }

        public void Send(Keys key)
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
    }
}
