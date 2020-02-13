using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTool.entities
{
    class MouseMoveTool
    {
        InputHookHelper helper = new InputHookHelper();
        Control ctrl;
        bool is_doing = false;
        public event EventHandler<MouseEventArgs> MouseMove;
        public MouseMoveTool(Control handler, Control ctrl = null)
        {
            this.ctrl = ctrl;
            handler.MouseDown += Button_MouseDown;
            helper.MouseMove += Helper_MouseMove;
            helper.MouseUp += Helper_MouseUp;
            helper.InstallHooks();
        }

        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            is_doing = true;
        }

        private void Helper_MouseUp(object sender, MouseEventArgs e)
        {
            is_doing = false;
        }

        private void Helper_MouseMove(object sender, MouseEventArgs e)
        {
            if (is_doing)
            {
                MouseMove?.Invoke(ctrl, e);
            }
        }
        ~MouseMoveTool()
        {
            helper.UninstallHooks();
        }
    }
}
