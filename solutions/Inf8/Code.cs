using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace SimulationSpace
{
    partial class HiddenWindow
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "Name"; 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ClientSize = new System.Drawing.Size(0, 0); 
            this.Top = 0;
            this.Left = 0;
            this.StartPosition = FormStartPosition.Manual; 
            this.ClientSize = new System.Drawing.Size(0, 0);
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.ShowIcon = false; 
            this.AllowTransparency = true;
            this.BackColor = Color.AliceBlue;
            this.TransparencyKey = this.BackColor; 
            this.Text = " ";
            this.Load += new System.EventHandler(this.HiddenWindow_Load_1);
            this.ResumeLayout(false);
        }
        private void HiddenWindow_Load_1(object sender, EventArgs e) { }
    }
    public partial class HiddenWindow : Form
    {
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            this.Visible = false; // == Hide
        }
        public HiddenWindow()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
    }

    static class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);
        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();
        [DllImport("user32.dll")]
        static extern int MapVirtualKey(int uCode, uint uMapType);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern void mouse_event(int dsFlags, int dx, int dy, int cButtons, int dsExtraInfo);
        [DllImport("user32.dll")]
        static extern void keybd_event(int bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            funcs();
            Application.Run(new HiddenWindow());
        }

        [Flags]
        public enum Keys
        {
            Backspace = 8,
            Tab = 9,
            Clear = 12,
            Enter = 13,

            Shift = 16,
            LShift = 160,
            RShift = 161,
            Control = 17,
            LControl = 162,
            RControl = 163,
            Alt = 18,
            LAlt = 164,
            RAlt = 165,

            PauseBreak = 19,
            CapsLock = 20,
            Escape = 27,
            Space = 32,
            PageUp = 33,
            PageDown = 34,
            End = 35,
            Home = 36,
            ArrowLeft = 37,
            ArrowUp = 38,
            ArrowRight = 39,
            ArrowDown = 40,
            Select = 41,
            Print = 42,
            Execute = 43,
            PrtScr = 44,
            Insert = 45,
            Delete = 46,

            D0 = 48,
            D1 = 49,
            D2 = 50,
            D3 = 51,
            D4 = 52,
            D5 = 53,
            D6 = 54,
            D7 = 55,
            D8 = 56,
            D9 = 57,

            A = 65,
            B = 66,
            C = 67,
            D = 68,
            E = 69,
            F = 70,
            G = 71,
            H = 72,
            I = 73,
            J = 74,
            K = 75,
            L = 76,
            M = 77,
            N = 78,
            O = 79,
            P = 80,
            Q = 81,
            R = 82,
            S = 83,
            T = 84,
            U = 85,
            V = 86,
            W = 87,
            X = 88,
            Y = 89,
            Z = 90,
            LWin = 91,
            RWin = 92,
            Menu = 93,

            Sleep = 95,
            NumEnter = 13,
            Num0 = 96,
            Num1 = 97,
            Num2 = 98,
            Num3 = 99,
            Num4 = 100,
            Num5 = 101,
            Num6 = 102,
            Num7 = 103,
            Num8 = 104,
            Num9 = 105,
            NumMultiply = 106,
            NumAdd = 107,
            NumSubtract = 108,
            NumDecimal = 109,
            NumDivide = 110,
            F1 = 112,
            F2 = 113,
            F3 = 114,
            F4 = 115,
            F5 = 116,
            F6 = 117,
            F7 = 118,
            F8 = 119,
            F9 = 120,
            F10 = 121,
            F11 = 122,
            F12 = 123,
            Mute = 173,
            VolumeDown = 174,
            VolumeUp = 175,
            Next = 176,
            Previous = 177,
            Stop = 178,
            Pause = 179,

            Semicolon = 186,
            Equal = 187,
            Comma = 188,
            Minus = 189,
            Period = 190,
            Slash = 191,
            Backquote = 192,
            BracketLeft = 219,
            Backslash = 220,
            BracketRight = 221,
            Quote = 222,

            NumLock = 144,
            ScrollLock = 145
        }

        public enum MouseData
        {
            LButton = 0x100,
            RButton = 0x101,
            MButton = 0x102,
            X1Button = 0x00000001,
            X2Button = 0x00000002,
            Wheel_Up = 0x108,
            Wheel_Down = -0x108,
        }
        public enum MouseStates
        {
            LeftDown = 0x0002, LeftUp = 0x0004,
            RightDown = 0x0008, RightUp = 0x0010,
            MiddleDown = 0x0020, MiddleUp = 0x0040,
            XDown = 0x0080, XUp = 0x0100,
            MouseMove = 0x0001,
            MouseWheel = 0x0800,
        }

        // ������ ����� 
        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public readonly uint uMsg;
            public readonly ushort wParamL;
            public readonly ushort wParamH;
        }
        // ������ �������
        public struct Input
        {
            public int type;
            public InputUnion Union;
        }
        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public MouseInput mouse;
            [FieldOffset(0)] public KeyboardInput keyboard;
            [FieldOffset(0)] public readonly HardwareInput hi;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort KeyCode;
            public ushort ScanCode;
            public uint dwFlags;
            public readonly uint time;
            public IntPtr dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public readonly uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        public static void KeyboardEvent(int key, uint flag)
        {
            Input[] Input = {
                new Input {
                    type = 1, // Keyboard
                    Union = new InputUnion {
                        keyboard = new KeyboardInput {
                            KeyCode = (ushort) key,
                            ScanCode = (ushort) MapVirtualKey(key, 0),
                            dwFlags = flag,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                }
            };
            SendInput(1, Input, Marshal.SizeOf(typeof(Input)));
        }
        public static void MouseEvent(MouseData data, MouseStates state, int x, int y)
        {
            Input[] Input = {
                new Input {
                    type = 0, // Mouse
                    Union = new InputUnion {
                        mouse = new MouseInput {
                            dx = Convert.ToInt32(x),
                            dy = Convert.ToInt32(y),
                            mouseData = (uint) data,
                            dwFlags = (uint) state,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                }
            };
            SendInput(1, Input, Marshal.SizeOf(typeof(Input)));
        }
        
        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }


        static void funcs()
        {
            void KeyDown(Keys key)
            {
                if ((key == Keys.LWin) ||
                     (key == Keys.RWin) ||
                     (key == Keys.PrtScr) ||
                     (key == Keys.PauseBreak) ||
                     (key == Keys.Insert) ||
                     (key == Keys.Home) ||
                     (key == Keys.End) ||
                     (key == Keys.PageUp) ||
                     (key == Keys.PageDown) ||
                     (key == Keys.Delete) ||
                     (key == Keys.ArrowLeft) ||
                     (key == Keys.ArrowUp) ||
                     (key == Keys.ArrowRight) ||
                     (key == Keys.ArrowDown) ||
                     (key == Keys.NumDivide) ||
                     (key == Keys.NumEnter) ||
                     (key == Keys.Sleep) ||
                     (key == Keys.Next) ||
                     (key == Keys.Previous) ||
                     (key == Keys.Stop) ||
                     (key == Keys.Pause) ||
                     (key == Keys.Mute) ||
                     (key == Keys.VolumeDown) ||
                     (key == Keys.VolumeUp))
                {
                    keybd_event((int)key, 0, 0, UIntPtr.Zero); 
                }
                else
                {
                    KeyboardEvent((int)key, 0); 
                }
                Thread.Sleep(15);
            }
            void KeyUp(Keys key)
            {
                if ((key == Keys.LWin) ||
                    (key == Keys.RWin) ||
                    (key == Keys.PrtScr) ||
                    (key == Keys.PauseBreak) ||
                    (key == Keys.Insert) ||
                    (key == Keys.Home) ||
                    (key == Keys.End) ||
                    (key == Keys.PageUp) ||
                    (key == Keys.PageDown) ||
                    (key == Keys.Delete) ||
                    (key == Keys.ArrowLeft) ||
                    (key == Keys.ArrowUp) ||
                    (key == Keys.ArrowRight) ||
                    (key == Keys.ArrowDown) ||
                    (key == Keys.NumDivide) ||
                    (key == Keys.NumEnter) ||
                    (key == Keys.Sleep) ||
                    (key == Keys.Next) ||
                    (key == Keys.Previous) ||
                    (key == Keys.Stop) ||
                    (key == Keys.Pause) ||
                    (key == Keys.Mute) ||
                    (key == Keys.VolumeDown) ||
                    (key == Keys.VolumeUp))
                {
                    keybd_event((int)key, 0, 0x02, UIntPtr.Zero);
                }
                else
                {
                    KeyboardEvent((int)key, 0x02);
                }
            }
            void KeyPress(Keys key)
            {
                KeyDown(key);
                KeyUp(key);
            }

            void LMB__Down()
            {
                MouseEvent(MouseData.LButton, MouseStates.LeftDown, 0, 0); 
            }
            void LMB__Up()
            {
                MouseEvent(MouseData.LButton, MouseStates.LeftUp, 0, 0);
            }
            void LMB__Click()
            {
                LMB__Down();
                LMB__Up();
            } 

            void RMB__Down()
            {
                MouseEvent(MouseData.RButton, MouseStates.RightDown, 0, 0); 
            }
            void RMB__Up()
            {
                MouseEvent(MouseData.RButton, MouseStates.RightUp, 0, 0);
            }
            void RMB__Click()
            {
                RMB__Down();
                RMB__Up();
            }

            void MMB__Down()
            {
                MouseEvent(MouseData.MButton, MouseStates.MiddleDown, 0, 0); 
            }
            void MMB__Up()
            {
                MouseEvent(MouseData.MButton, MouseStates.MiddleUp, 0, 0);
            }
            void MMB__Click()
            {
                MMB__Down();
                MMB__Up();
            }

            void X1__Down()
            {
                MouseEvent(MouseData.X1Button, MouseStates.XDown, 0, 0); 
            }
            void X1__Up()
            {
                MouseEvent(MouseData.X1Button, MouseStates.XUp, 0, 0);
            }
            void X1__Click()
            {
                X1__Down();
                X1__Up();
            }

            void X2__Down()
            {
                MouseEvent(MouseData.X2Button, MouseStates.XDown, 0, 0); 
            }
            void X2__Up()
            {
                MouseEvent(MouseData.X2Button, MouseStates.XUp, 0, 0);
            }
            void X2__Click()
            {
                X2__Down();
                X2__Up();
            }

            void MouseWheel__Down()
            {
                MouseEvent(MouseData.Wheel_Down, MouseStates.MouseWheel, 0, 0); 
            }
            void MouseWheel__Up()
            {
                MouseEvent(MouseData.Wheel_Up, MouseStates.MouseWheel, 0, 0); 
            }

            void CursorRelative(int x, int y)
            {
                SetCursorPos(GetCursorPosition().X + x, GetCursorPosition().Y + y); 
            }
            void CursorAbsolute(int x, int y)
            {
                SetCursorPos(x, y);
            }
             
            void Delay(int time)
            {
                Thread.Sleep(time);
            } 

            void CopyToClipboard (string text)
            {
                Clipboard.SetText(text); 
            }
            void OpenFolder(string path)
            {
                Process.Start(path); 
            }
            void OpenFile(string path)
            {
                Process.Start(path);
            }

            CursorRelative(30,0);   
// 100000000000000000000000000
CursorRelative(0,220);   
LMB__Click();   
CursorRelative(0,-220);   
CursorRelative(0,245);   
LMB__Click();   
CursorRelative(0,-245);   
CursorRelative(0,125);   
LMB__Click();   
CursorRelative(0,-125);   
Delay(700);   
CursorRelative(0,220);   
LMB__Click();   
Delay(700);   
LMB__Click();   
CursorRelative(0,-220);   
Delay(700);   
CursorRelative(0,180);   
LMB__Click();   
CursorRelative(0,-180);   
Delay(700);   
CursorRelative(0,410);   
LMB__Click();   
CursorRelative(0,-410);   
CursorRelative(0,315);   
LMB__Click();   
CursorRelative(0,-315);   
Delay(700);   
CursorRelative(0,410);   
LMB__Click();   
CursorRelative(0,-410);   
Delay(700);   
CursorRelative(-30,0);  

            Environment.Exit(0);
        }
    }
}
