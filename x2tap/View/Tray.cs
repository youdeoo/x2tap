using System;
using System.Reflection;
using System.Windows.Forms;

namespace x2tap.View
{
    public class Tray
    {
        public NotifyIcon Icon;

        /// <summary>
        ///		初始化托盘
        /// </summary>
        public void Init()
        {
            var openTelegramGroup = new MenuItem("Telegram 群组");
			openTelegramGroup.Click += (object sender, EventArgs e) =>
            {
                Utils.Shell.ExecuteCommandNoWait("START", "https://t.me/x2tapChat");
            };

			var openTelegramChannel = new MenuItem("Telegram 频道");
			openTelegramChannel.Click += (object sender, EventArgs e) =>
			{
				Utils.Shell.ExecuteCommandNoWait("START", "https://t.me/x2tap");
			};

            var openGithub = new MenuItem("Github");
			openGithub.Click += (object sender, EventArgs e) =>
            {
                Utils.Shell.ExecuteCommandNoWait("START", "https://github.com/hacking001/x2tap");
            };

            MenuItem help = new MenuItem("帮助", new MenuItem[] { openTelegramGroup, openTelegramChannel, openGithub });

            MenuItem exit = new MenuItem("退出");
			exit.Click += (object sender, EventArgs e) =>
            {
                if (Global.Views.MainForm.Started)
                {
                    MessageBox.Show("请先停止再退出程序", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Utils.Config.SaveToFile();
					Icon.Dispose();
                    Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "x2tap.exe");
                }
            };

            MenuItem[] parent = new MenuItem[] { help, new MenuItem("-"), exit };

            Icon = new NotifyIcon()
            {
                Text = Assembly.GetExecutingAssembly().GetName().Name,
                Visible = true,
                ContextMenu = new ContextMenu(parent),
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath)
            };
			Icon.DoubleClick += (object sender, EventArgs e) =>
			{
				if (Global.Views.MainForm.WindowState == FormWindowState.Minimized)
				{
					Global.Views.MainForm.Show();
					Global.Views.MainForm.WindowState = FormWindowState.Normal;
				}
			};
        }
    }
}