using System; 
using System.Windows.Forms; 
using System.Drawing;
using System.Drawing.Imaging; 
using System.Threading;
using System.Runtime.InteropServices; 
using System.Diagnostics; 
 
namespace CheckScreen {
    class Program {

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        } 
        [DllImport("user32.dll")]
        static extern bool SetWindowPos (
             int hWnd,
             int hWndInsertAfter, 
             int X,
             int Y,
             int cx,
             int cy,
             uint uFlags 
        );   


        static void Main(string[] args)
        { 
            //Solve_Cycle(); 
            Experiments();
        }

        static bool IsThereLogic() {
            Process[] logic_proc = Process.GetProcessesByName("Logic");
            Process[] code_proc = Process.GetProcessesByName("Code");

            if (logic_proc.Length == 0) {
                if (code_proc.Length != 0) {
                    code_proc[0].Kill();
                }
                Application.Exit();
            }
            return logic_proc.Length == 0;
        }

        static void FindWindow() {  
            Process[] logic_Pr = Process.GetProcessesByName("Logic");
            if (logic_Pr.Length != 0)  
                logic_Pr[0].Kill();
            Process.Start(@".\Logic Game\Logic.exe"); 
            Thread.Sleep(3000); 
            logic_Pr = Process.GetProcessesByName("Logic");
            SetForegroundWindow(logic_Pr[0].MainWindowHandle);

            int logic_Width = 585;
            int logic_Height = 453;
            int logic_X = (Screen.PrimaryScreen.Bounds.Width - logic_Width) / 2;
            int logic_Y = (Screen.PrimaryScreen.Bounds.Height - logic_Height) / 2;


            SetCursorPos(logic_X+17, logic_Y+12);  
        }

        static void Experiments(){ 
            Process[] result = new Process[10];
            int count = 0;
            Process[] processes = Process.GetProcesses();
            foreach (var process in processes) {
                if (process.MainWindowTitle
                           .IndexOf("Logic", StringComparison.InvariantCulture) > -1)
                {
                    result[count] = process;
                    count++;
                }
            }
            foreach (var process in processes) {
                SetWindowPos((int)process.MainWindowHandle, -2, 960, 540, 600, 600, 0x0010);
            } 
            
            //RECT rect = new RECT();
            //GetWindowRect(result[0].MainWindowHandle, out rect);

            //Console.WriteLine(rect.Left); 
        }

        static void Screen_CurrentLevel() { 
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            int logic_Width = 585;
            int logic_Height = 453;
            int logic_X = (Screen.PrimaryScreen.Bounds.Width - logic_Width)/2;
            int logic_Y = (Screen.PrimaryScreen.Bounds.Height - logic_Height)/2;

            Size logic_size = new Size(logic_Width, logic_Height);

            Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height );
            Graphics gfxScreenshot = Graphics.FromImage(bmpScreenshot);  
            gfxScreenshot.CopyFromScreen(logic_X,
                                        logic_Y,
                                        0,
                                        0,
                                        logic_size,
                                        CopyPixelOperation.SourceCopy); 
            bmpScreenshot.Save(@".\screens\CurrentLevel.png", ImageFormat.Png);
             
        }

        static int Comparison() { 
            string path = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
             
            Bitmap logic_window = new Bitmap(@".\screens\CurrentLevel.png");
            //Thread.Sleep(250);
            Color templateColor = new Color();
            Color levelColor = new Color();

            int[,] significantPixels = new int[,] { { 537, 307 }, { 269, 261 }, { 545, 380 }, { 495, 280 }, { 494, 193 }, { 495, 397 }, { 500, 378 } };
             
            for (int i = 2; i < 9; i++) { 
                Bitmap template = new Bitmap(@".\screens\"+i+".png");
                templateColor = template.GetPixel(significantPixels[i-2,0], significantPixels[i-2, 1]);
                levelColor = logic_window.GetPixel(significantPixels[i-2, 0], significantPixels[i-2, 1]); 
                if (templateColor == levelColor) { 
                    return i;
                } 
            } 
            return 0;
        }


        static void Solve_Cycle() {

            FindWindow(); 
            IsThereLogic();

            int passed = -1; 
            int logic_case = 1;
            Process[] code_proc = Process.GetProcessesByName("Code");

            while (code_proc.Length == 0 && passed != 7) {
                if(!IsThereLogic()) 
                    Process.Start(@".\solutions\Inf" + logic_case + @"\Code.exe"); 
                else   
                    Application.Exit(); 
                code_proc = Process.GetProcessesByName("Code");
                Thread.Sleep(1); 
                while (code_proc.Length != 0) {
                    if (IsThereLogic()) { code_proc[0].Kill();  Application.Exit(); }
                    code_proc = Process.GetProcessesByName("Code"); 
                    if (code_proc.Length == 0) { 
                        passed++;
                        Thread.Sleep(135);
                        Screen_CurrentLevel(); 
                        logic_case = Comparison();
                        break;
                    }
                    Thread.Sleep(1);
                } 
            } 
            //Console.ReadLine();
            Application.Exit(); 
        }
    }
}
