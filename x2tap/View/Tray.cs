using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace x2tap.View
{
    class Tray
    {

        private NotifyIcon notifyIcon = null;

        /// <summary>
        /// 初始化托盘
        /// </summary>
        public void InitTray()
        {
            MenuItem Item_About = new MenuItem("Telegram群组");
            Item_About.Click += (object sender, EventArgs e) =>
            {
                Utils.Shell.ExecuteCommandNoWait("START", "https://t.me/x2tapChat");
            };
            MenuItem Item_GitHub = new MenuItem("GitHub");
            Item_GitHub.Click += (object sender, EventArgs e) =>
            {
                Utils.Shell.ExecuteCommandNoWait("START", "https://github.com/hacking001/x2tap");
            };
            MenuItem Item_Help = new MenuItem("帮助", new MenuItem[] { Item_About, Item_GitHub });



            MenuItem Item_Exit = new MenuItem("退出");
            Item_Exit.Click += (object sender, EventArgs e) =>
            {
                if (Global.Views.MainForm.Started)
                {
                    MessageBox.Show("请先点击关闭按钮", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Utils.Config.SaveToFile();
                    Utils.Shell.ExecuteCommandNoWait("taskkill", "/f", "/t", "/im", "x2tap.exe");
                    //Environment.Exit(0);
                }
            };


            MenuItem[] parentMenuitem = new MenuItem[] { Item_Help, new MenuItem("-"), Item_Exit };

            notifyIcon = new NotifyIcon()
            {
                BalloonTipText = "X2Tap已启动！",
                Text = Assembly.GetExecutingAssembly().GetName().Name,
                Visible = true,
                ContextMenu = new ContextMenu(parentMenuitem),
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath)
            };
            notifyIcon.ShowBalloonTip(1000);
            notifyIcon.DoubleClick += (object sender, EventArgs e) =>
            {
            };
        }
    }
}