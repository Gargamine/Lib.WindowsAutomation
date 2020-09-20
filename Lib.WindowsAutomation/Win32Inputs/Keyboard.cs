using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Lib.WindowsAutomation.Win32Inputs
{

#pragma warning disable 649
    internal static class Keyboard
    {
        [Flags]
        public enum KEYEVENTF : uint
        {
            EXTENDEDKEY = 1,
            KEYUP = 2,
            SCANCODE = 8,
            UNICODE = 4
        }

        [Flags]
        public enum MouseEventDataXButtons : uint
        {
            Nothing = 0,
            XBUTTON1 = 1,
            XBUTTON2 = 2
        }

        [Flags]
        public enum MOUSEEVENTF : uint
        {
            ABSOLUTE = 32768, // 0x00008000
            HWHEEL = 4096, // 0x00001000
            MOVE = 1,
            MOVE_NOCOALESCE = 8192, // 0x00002000
            LEFTDOWN = 2,
            LEFTUP = 4,
            RIGHTDOWN = 8,
            RIGHTUP = 16, // 0x00000010
            MIDDLEDOWN = 32, // 0x00000020
            MIDDLEUP = 64, // 0x00000040
            VIRTUALDESK = 16384, // 0x00004000
            WHEEL = 2048, // 0x00000800
            XDOWN = 128, // 0x00000080
            XUP = 256 // 0x00000100
        }

        public static void Send(ScanCodeShort a)
        {
            var Inputs = new INPUT[1];
            var Input = new INPUT();
            Input.type = 1;
            Input.U.ki.wScan = a;
            Input.U.ki.dwFlags = KEYEVENTF.SCANCODE;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
            Thread.Sleep(100);
        }

        public static void Send(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                foreach (char character in input)
                {
                    switch (character)
                    {
                        #region Letters
                        case 'a':
                            Send(ScanCodeShort.KEY_A);
                            break;
                        case 'A':
                            SendWithShift(ScanCodeShort.KEY_A);
                            break;
                        case 'b':
                            Send(ScanCodeShort.KEY_B);
                            break;
                        case 'B':
                            SendWithShift(ScanCodeShort.KEY_B);
                            break;
                        case 'c':
                            Send(ScanCodeShort.KEY_C);
                            break;
                        case 'C':
                            SendWithShift(ScanCodeShort.KEY_C);
                            break;
                        case 'd':
                            Send(ScanCodeShort.KEY_D);
                            break;
                        case 'D':
                            SendWithShift(ScanCodeShort.KEY_D);
                            break;
                        case 'e':
                            Send(ScanCodeShort.KEY_E);
                            break;
                        case 'E':
                            SendWithShift(ScanCodeShort.KEY_E);
                            break;
                        case 'f':
                            Send(ScanCodeShort.KEY_F);
                            break;
                        case 'F':
                            SendWithShift(ScanCodeShort.KEY_F);
                            break;
                        case 'g':
                            Send(ScanCodeShort.KEY_G);
                            break;
                        case 'G':
                            SendWithShift(ScanCodeShort.KEY_G);
                            break;
                        case 'h':
                            Send(ScanCodeShort.KEY_H);
                            break;
                        case 'H':
                            SendWithShift(ScanCodeShort.KEY_H);
                            break;
                        case 'i':
                            Send(ScanCodeShort.KEY_I);
                            break;
                        case 'I':
                            SendWithShift(ScanCodeShort.KEY_I);
                            break;
                        case 'j':
                            Send(ScanCodeShort.KEY_J);
                            break;
                        case 'J':
                            SendWithShift(ScanCodeShort.KEY_J);
                            break;
                        case 'k':
                            Send(ScanCodeShort.KEY_K);
                            break;
                        case 'K':
                            SendWithShift(ScanCodeShort.KEY_K);
                            break;
                        case 'l':
                            Send(ScanCodeShort.KEY_L);
                            break;
                        case 'L':
                            SendWithShift(ScanCodeShort.KEY_L);
                            break;
                        case 'm':
                            Send(ScanCodeShort.KEY_M);
                            break;
                        case 'M':
                            SendWithShift(ScanCodeShort.KEY_M);
                            break;
                        case 'n':
                            Send(ScanCodeShort.KEY_N);
                            break;
                        case 'N':
                            SendWithShift(ScanCodeShort.KEY_N);
                            break;
                        case 'o':
                            Send(ScanCodeShort.KEY_O);
                            break;
                        case 'O':
                            SendWithShift(ScanCodeShort.KEY_O);
                            break;
                        case 'p':
                            Send(ScanCodeShort.KEY_P);
                            break;
                        case 'P':
                            SendWithShift(ScanCodeShort.KEY_P);
                            break;
                        case 'q':
                            Send(ScanCodeShort.KEY_Q);
                            break;
                        case 'Q':
                            SendWithShift(ScanCodeShort.KEY_Q);
                            break;
                        case 'r':
                            Send(ScanCodeShort.KEY_R);
                            break;
                        case 'R':
                            SendWithShift(ScanCodeShort.KEY_R);
                            break;
                        case 's':
                            Send(ScanCodeShort.KEY_S);
                            break;
                        case 'S':
                            SendWithShift(ScanCodeShort.KEY_S);
                            break;
                        case 't':
                            Send(ScanCodeShort.KEY_T);
                            break;
                        case 'T':
                            SendWithShift(ScanCodeShort.KEY_T);
                            break;
                        case 'u':
                            Send(ScanCodeShort.KEY_U);
                            break;
                        case 'U':
                            SendWithShift(ScanCodeShort.KEY_U);
                            break;
                        case 'v':
                            Send(ScanCodeShort.KEY_V);
                            break;
                        case 'V':
                            SendWithShift(ScanCodeShort.KEY_V);
                            break;
                        case 'w':
                            Send(ScanCodeShort.KEY_W);
                            break;
                        case 'W':
                            SendWithShift(ScanCodeShort.KEY_W);
                            break;
                        case 'x':
                            Send(ScanCodeShort.KEY_X);
                            break;
                        case 'X':
                            SendWithShift(ScanCodeShort.KEY_X);
                            break;
                        case 'y':
                            Send(ScanCodeShort.KEY_Y);
                            break;
                        case 'Y':
                            SendWithShift(ScanCodeShort.KEY_Y);
                            break;
                        case 'z':
                            Send(ScanCodeShort.KEY_Z);
                            break;
                        case 'Z':
                            SendWithShift(ScanCodeShort.KEY_Z);
                            break;
                        #endregion Letters
                        #region Special characters
                        case '-':
                            Send(ScanCodeShort.OEM_MINUS);
                            break;
                        case ' ':
                            Send(ScanCodeShort.SPACE);
                            break;
                        case '!':
                            SendWithShift(ScanCodeShort.KEY_1);
                            break;
                        case '?':
                            SendWithShift(ScanCodeShort.OEM_2);
                            break;
                        case '@':
                            SendWithShift(ScanCodeShort.KEY_2);
                            break;
                        case '%':
                            SendWithShift(ScanCodeShort.KEY_5);
                            break;
                        case '{':
                            SendWithShift(ScanCodeShort.OEM_4);
                            break;
                        case '}':
                            SendWithShift(ScanCodeShort.OEM_6);
                            break;
                        case '[':
                            Send(ScanCodeShort.OEM_4);
                            break;
                        case ']':
                            Send(ScanCodeShort.OEM_6);
                            break;
                        case '(':
                            SendWithShift(ScanCodeShort.KEY_9);
                            break;
                        case ')':
                            SendWithShift(ScanCodeShort.KEY_0);
                            break;
                        case '*':
                            SendWithShift(ScanCodeShort.KEY_8);
                            break;
                        case '#':
                            SendWithShift(ScanCodeShort.KEY_3);
                            break;
                        case '$':
                            SendWithShift(ScanCodeShort.KEY_4);
                            break;
                        case '+':
                            SendWithShift(ScanCodeShort.OEM_PLUS);
                            break;
                        case '=':
                            Send(ScanCodeShort.OEM_PLUS);
                            break;
                        case '.':
                            SendWithShift(ScanCodeShort.DECIMAL);
                            break;
                        case ',':
                            SendWithShift(ScanCodeShort.OEM_COMMA);
                            break;
                        #endregion Special characters
                        #region Numbers
                        case '1':
                            Send(ScanCodeShort.KEY_1);
                            break;
                        case '2':
                            Send(ScanCodeShort.KEY_2);
                            break;
                        case '3':
                            Send(ScanCodeShort.KEY_3);
                            break;
                        case '4':
                            Send(ScanCodeShort.KEY_4);
                            break;
                        case '5':
                            Send(ScanCodeShort.KEY_5);
                            break;
                        case '6':
                            Send(ScanCodeShort.KEY_6);
                            break;
                        case '7':
                            Send(ScanCodeShort.KEY_7);
                            break;
                        case '8':
                            Send(ScanCodeShort.KEY_8);
                            break;
                        case '9':
                            Send(ScanCodeShort.KEY_9);
                            break;
                        case '0':
                            Send(ScanCodeShort.KEY_0);
                            break;
                        #endregion Numbers
                        default:
                            throw new NotSupportedException($"Key '{character}' not supported by 'Lib.WindowsAutomation.Win32Inputs.Keyboard' class.");
                    }

                    Thread.Sleep(100);
                }
            }
        }

        //public void Send(ScanCodeShort a)
        //{
        //    var num = (int)SendInput(1U, new INPUT[1]
        //    {
        //        new INPUT
        //        {
        //            type = 1U,
        //            U =
        //            {
        //                ki =
        //                {
        //                    wScan = a,
        //                    dwFlags = KEYEVENTF.SCANCODE
        //                }
        //            }
        //        }
        //    }, INPUT.Size);
        //}

        private static void SendWithShift(ScanCodeShort a)
        {
            HoldLShift();
            Send(a);
            ReleaseLShift();
        }

        private static void SendWithAltCar(ScanCodeShort a)
        {
            HoldAltCar();
            Send(a);
            ReleaseAltCar();
        }

        private static void HoldAltCar()
        {
            var Inputs = new INPUT[1];
            var Input = new INPUT();
            Input.type = 1;
            Input.U.ki.wScan = ScanCodeShort.RMENU;
            Input.U.ki.dwFlags = KEYEVENTF.SCANCODE;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
        }

        private static void ReleaseAltCar()
        {
            var Inputs = new INPUT[1];
            var Input = new INPUT();
            Input.type = 1;
            Input.U.ki.wScan = ScanCodeShort.RMENU;
            Input.U.ki.dwFlags = KEYEVENTF.KEYUP | KEYEVENTF.SCANCODE;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
        }

        private static void HoldLShift()
        {
            var Inputs = new INPUT[1];
            var Input = new INPUT();
            Input.type = 1;
            Input.U.ki.wScan = ScanCodeShort.LSHIFT;
            Input.U.ki.dwFlags = KEYEVENTF.SCANCODE;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
        }
        private static void ReleaseLShift()
        {
            var Inputs = new INPUT[1];
            var Input = new INPUT();
            Input.type = 1;
            Input.U.ki.wScan = ScanCodeShort.LSHIFT;
            Input.U.ki.dwFlags = KEYEVENTF.KEYUP | KEYEVENTF.SCANCODE;
            Inputs[0] = Input;
            SendInput(1, Inputs, INPUT.Size);
        }


        [DllImport("user32.dll")]
        internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray)] [In]
            INPUT[] pInputs, int cbSize);

        public struct INPUT
        {
            public uint type;
            public InputUnion U;

            public static int Size => Marshal.SizeOf(typeof(INPUT));
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] internal MOUSEINPUT mi;
            [FieldOffset(0)] internal KEYBDINPUT ki;
            [FieldOffset(0)] internal HARDWAREINPUT hi;
        }

        public struct MOUSEINPUT
        {
            internal int dx;
            internal int dy;
            internal MouseEventDataXButtons mouseData;
            internal MOUSEEVENTF dwFlags;
            internal uint time;
            internal UIntPtr dwExtraInfo;
        }

        public struct KEYBDINPUT
        {
            internal VirtualKeyShort wVk;
            internal ScanCodeShort wScan;
            internal KEYEVENTF dwFlags;
            internal int time;
            internal UIntPtr dwExtraInfo;
        }

        public struct HARDWAREINPUT
        {
            internal int uMsg;
            internal short wParamL;
            internal short wParamH;
        }
    }
}
