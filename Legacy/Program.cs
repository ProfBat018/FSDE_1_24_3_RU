using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    class Program
    {
        [DllImport("user32.dll")]
        public static extern int MessageBox(int hWnd, string text, string caption, uint type);

        static void Main(string[] args)
        {
            MessageBox(0, "Hello World!", "Hello Dialog", 0);
        }
    }
}