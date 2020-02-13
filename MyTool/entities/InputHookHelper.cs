using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTool.entities
{
    /// <summary>
    /// https://www.cnblogs.com/wangjixianyun/archive/2012/12/25/2832405.html
    /// </summary>
    public class InputHookHelper : IDisposable
    {
        private HookProc msProc;
        private HookProc kbProc;

        public IntPtr MouseHook { get; private set; } = IntPtr.Zero;
        public IntPtr KeyboardHook { get; private set; } = IntPtr.Zero;

        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseMove;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<NewKeyboardMessageEventArgs> NewKeyboardMessage;

        public InputHookHelper()
        {
            msProc = LowLevelMouseProc;
            kbProc = LowLevelKeyboardProc;
        }

        public void InstallHooks()
        {
#if DEBUG
            DebugLog("Installing Hooks");
#endif
            if (MouseHook == IntPtr.Zero)
                MouseHook = NativeMethods.SetWindowsHookEx(HookType.LowLevelMouse, msProc, IntPtr.Zero, 0);
            if (KeyboardHook == IntPtr.Zero)
                KeyboardHook = NativeMethods.SetWindowsHookEx(HookType.LowLevelKeyboard, kbProc, IntPtr.Zero, 0);
        }

        public void UninstallHooks()
        {
#if DEBUG
            DebugLog("Uninstalling Hooks");
#endif
            NativeMethods.UnhookWindowsHookEx(MouseHook);
            NativeMethods.UnhookWindowsHookEx(KeyboardHook);
            MouseHook = KeyboardHook = IntPtr.Zero;
        }

        private IntPtr LowLevelMouseProc(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var st = (MouseLowLevelHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLowLevelHookStruct));
                var msg_type = (MouseMessage)wParam;
                EventHandler<MouseEventArgs> evt_handler = null;
                MouseButtons button = MouseButtons.Left;
                if (msg_type == MouseMessage.LButtonDown || msg_type == MouseMessage.RButtonDown)
                {
                    evt_handler = MouseDown;
                    button = msg_type == MouseMessage.LButtonDown ? MouseButtons.Left : MouseButtons.Right;
                }
                else if (msg_type == MouseMessage.MouseMove)
                {
                    evt_handler = MouseMove;
                }
                else if (msg_type == MouseMessage.LButtonUp || msg_type == MouseMessage.RButtonUp)
                {
                    evt_handler = MouseUp;
                    button = msg_type == MouseMessage.LButtonUp ? MouseButtons.Left : MouseButtons.Right;
                }
                var e = new MouseEventArgs(button, 1, st.pt.x, st.pt.y, 1);
                //        MouseMove = 0x200,
                //LButtonDown = 0x201,
                //LButtonUp = 0x202,
                //RButtonDown = 0x204,
                //RButtonUp = 0x205,
                //MouseWheel = 0x20a,
                //MouseHWheel = 0x20e
                evt_handler?.Invoke(this, e);
            }
            return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        private IntPtr LowLevelKeyboardProc(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var st = (KeyboardLowLevelHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardLowLevelHookStruct));
                NewKeyboardMessage?.Invoke(this, new NewKeyboardMessageEventArgs(st.vkCode, (KeyboardMessage)wParam));
            }
            return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
#if DEBUG
            DebugLog($"Dispose() called, disposing: {disposing}, disposedValue: {disposedValue}");
#endif
            if (!disposedValue)
            {
                UninstallHooks();
                disposedValue = true;
            }
        }

        ~InputHookHelper() => Dispose(false);

        public void Dispose()
        {
#if DEBUG
            DebugLog("Dispose() called");
#endif
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

#if DEBUG
        private void DebugLog(string message)
            => Debug.WriteLine($"[InputHookHelper:{GetHashCode():X}] {message}");
#endif
    }

    public class NewKeyboardMessageEventArgs : EventArgs
    {
        public int VirtKeyCode { get; private set; }
        public KeyboardMessage MessageType { get; private set; }

        public NewKeyboardMessageEventArgs(int vkCode, KeyboardMessage msg)
        {
            VirtKeyCode = vkCode;
            MessageType = msg;
        }
    }

    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    internal delegate IntPtr HookProc(int nCode, UIntPtr wParam, IntPtr lParam);

    internal enum HookType
    {
        LowLevelKeyboard = 13,
        LowLevelMouse = 14
    }

    public enum MouseMessage
    {
        MouseMove = 0x200,
        LButtonDown = 0x201,
        LButtonUp = 0x202,
        RButtonDown = 0x204,
        RButtonUp = 0x205,
        MouseWheel = 0x20a,
        MouseHWheel = 0x20e
    }

    public enum KeyboardMessage
    {
        KeyDown = 0x100,
        KeyUp = 0x101,
        SysKeyDown = 0x104,
        SysKeyUp = 0x105
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MouseLowLevelHookStruct
    {
        public Point pt;
        public int mouseData;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct KeyboardLowLevelHookStruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    internal class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetWindowsHookEx(HookType idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll")]
        internal static extern int UnhookWindowsHookEx(IntPtr hHook);

        [DllImport("user32.dll")]
        internal static extern IntPtr CallNextHookEx(IntPtr _, int nCode, UIntPtr wParam, IntPtr lParam);
    }

    /*
    public class UsageProgram
    {
        static InputHookHelper helper = new InputHookHelper();
        static void Test(string[] args)
        {
            helper.NewMouseMessage += (sender, e) =>
                Debug.WriteLine($"{e.MessageType}, x: {e.Position.x}, y: {e.Position.y}");
            helper.NewKeyboardMessage += (sender, e) =>
                Debug.WriteLine($"{e.MessageType}, VirtKeyCode: {e.VirtKeyCode}");
            helper.InstallHooks();
            // Do something ...
            helper.UninstallHooks();
        }
    }
    */
}
