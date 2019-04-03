using System;
using System.IO;
using System.Windows.Forms;
using x2tap.Utils;
using x2tap.View;

namespace x2tap
{
	public static class x2tap
	{
        /// <summary>
        ///     应用程序的主入口点
        /// </summary>
        [STAThread]
        public static void Main()
        {
#if !DEBUG
			var filenames = new string[]
			{
				"driver\\OemVista.inf",
				"driver\\tap0901.cat",
				"driver\\tap0901.sys",
				"tapinstall.exe",
				"tun2socks.exe",
				"geoip.dat",
				"geosite.dat",
				"v2ctl.exe",
				"v2ray.exe",
				"wv2ray.exe"
			};

			// 依赖文件检查
			foreach (var filename in filenames)
			{
				if (!File.Exists(filename))
				{
					MessageBox.Show(string.Format("缺失重要文件：{0}", filename), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

					Application.Exit();
					return;
				}
			}
#endif

			// 检查 TUN/TAP 适配器
			if (TUNTAP.GetComponentId() == "" && !TUNTAP.Create())
            {
                MessageBox.Show("尝试安装 TUN/TAP 适配器时失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Application.Exit();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Global.Views.MainForm = new MainForm());
        }
    }
}