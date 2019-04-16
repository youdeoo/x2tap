using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

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
			Directory.SetCurrentDirectory(Application.StartupPath);

#if !DEBUG
			var filenames = new string[]
			{
				"driver\\OemVista.inf",
				"driver\\tap0901.cat",
				"driver\\tap0901.sys",
				"dnscrypt-forwarding-rules.txt",
				"dnscrypt-proxy.exe",
				"dnscrypt-proxy.toml",
				"geoip.dat",
				"geosite.dat",
				"libsodium.dll",
				"libsodium.lib",
				"libssp-0.dll",
				"libwinpthread-1.dll",
				"Newtonsoft.Json.dll",
				"pcre3.dll",
				"RunHiddenConsole.exe",
				"ssr-local.exe",
				"tapinstall.exe",
				"tun2socks.exe",
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

			// 检查 TUN/TAP 适配器
			if (Utils.TUNTAP.GetComponentID() == "" && !Utils.TUNTAP.Create())
            {
                MessageBox.Show("尝试安装 TUN/TAP 适配器时失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                Application.Exit();
                return;
            }
#endif

			using (var mutex = new Mutex(false, "Global\\x2tap"))
			{
				if (mutex.WaitOne(0, false))
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(Global.Views.MainForm = new View.MainForm());
				}
				else
				{
					MessageBox.Show("只允许同时运行一个", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
        }
    }
}