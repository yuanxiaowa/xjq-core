using MyTool.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var hwnd =(IntPtr) 0x001f1222;
            var hp1 = (IntPtr)0x41112e;
            var x = 132;
            var y = 656;
            var lParam = x + (y << 4 * 4);
            //Win32Api.SendMessage(hp1, Mscode.WM_PARENTNOTIFY, 0x201, lParam);
            //Win32Api.SendMessage((IntPtr)0x33086e, Mscode.WM_PARENTNOTIFY, 0x201, lParam);
            //Win32Api.SendMessage((IntPtr)0x741196, Mscode.WM_PARENTNOTIFY, 0x201, lParam);
            //Win32Api.SendMessage((IntPtr)0x11368e, Mscode.WM_PARENTNOTIFY, 0x201, lParam);

            Win32Api.SendMessage(hwnd, Mscode.WM_LBUTTONDOWN, 1, lParam);
            Win32Api.SendMessage(hwnd, Mscode.WM_LBUTTONUP, 0, lParam);
            Win32Api.SendTextMessage(hwnd, Mscode.WM_SETTEXT, 0, "nn");
        }
    }
}
