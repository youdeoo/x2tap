using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace x2tap.View
{
    public partial class MainForm : Form
    {
        /// <summary>
        ///		上行流量
        /// </summary>
        public long UplinkBandwidth = 0;

		/// <summary>
		///		下行流量
		/// </summary>
		public long DownlinkBandwidth = 0;

        /// <summary>
        ///     启动状态
        /// </summary>
        public bool Started;

        /// <summary>
        ///     状态信息
        /// </summary>
        public string Status = "请下达命令！";

        public MainForm()
        {
            InitializeComponent();

			CheckForIllegalCrossThreadCalls = false;

        }

        /// <summary>
        ///     初始化代理
        /// </summary>
        public void InitProxies()
        {
            // 先清空掉内容
            ProxyComboBox.Items.Clear();
            // 添加 v2ray 代理
            foreach (var v2ray in Global.V2RayProxies)
            {
                ProxyComboBox.Items.Add(string.Format("[v2ray] {0}", v2ray.Remark));
            }

            // 添加 Shadowsocks 代理
            foreach (var shadowsocks in Global.ShadowsocksProxies)
            {
                ProxyComboBox.Items.Add(string.Format("[SS] {0}", shadowsocks.Remark));
            }

			// 添加 ShadowsocksR 代理
			foreach (var shadowsocksr in Global.ShadowsocksRProxies)
			{
				ProxyComboBox.Items.Add(string.Format("[SSR] {0}", shadowsocksr.Remark));
			}

            if (ProxyComboBox.Items.Count > 0)
            {
                ProxyComboBox.SelectedIndex = 0;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 初始化日志目录
            if (!Directory.Exists("logging"))
            {
                Directory.CreateDirectory("logging");
            }

			// 初始化配置
			Utils.Config.InitFromFile();

            // 初始化代理
            InitProxies();
            if (ProxyComboBox.Items.Count > 0)
            {
                ProxyComboBox.SelectedIndex = 0;
            }

            // 初始化模式
            ModeComboBox.SelectedIndex = 0;
			foreach (var mode in Global.Modes)
			{
				ModeComboBox.Items.Add(string.Format("[外置规则] {0}", mode.Name));
			}

            // 初始化适配器
            Task.Run(() =>
            {
                using (var client = new UdpClient("114.114.114.114", 53))
                {
                    var address = ((IPEndPoint) client.Client.LocalEndPoint).Address;
                    Global.Config.AdapterAddress = address.ToString();

                    var addressGeted = false;

                    var adapters = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (var adapter in adapters)
                    {
                        var properties = adapter.GetIPProperties();

                        foreach (var information in properties.UnicastAddresses)
                        {
                            if (information.Address.AddressFamily == AddressFamily.InterNetwork && Equals(information.Address, address))
                            {
                                addressGeted = true;
                            }
                        }

                        foreach (var information in properties.GatewayAddresses)
                        {
                            if (addressGeted)
                            {
                                Global.Config.AdapterGateway = information.Address.ToString();
                                break;
                            }
                        }

                        if (addressGeted)
                        {
                            break;
                        }
                    }
                }
            });

            // 后台工作
            Task.Run(() =>
            {
				var count = 0;

				while (true)
                {
                    try
                    {
						// 更新标题栏时间
						Text = string.Format("x2tap - {0}", DateTime.Now.ToString());

						// 更新状态信息
						StatusLabel.Text = string.Format("状态：{0}", Status);
						
						// 更新流量信息
						if (Started)
                        {
							if (count % 10 == 0)
							{
								Task.Run(() =>
								{
									var adapters = NetworkInterface.GetAllNetworkInterfaces();
									foreach (var adapter in adapters)
									{
										if (adapter.Name == Utils.TUNTAP.GetName(Utils.TUNTAP.GetComponentID()))
										{
											var stats = adapter.GetIPv4Statistics();

											UsedBandwidthLabel.Text = $"已使用：{Utils.ComputeBandwidth(stats.BytesReceived + stats.BytesSent)}";
											UplinkSpeedLabel.Text = $"↑：{Utils.ComputeBandwidth(stats.BytesSent - UplinkBandwidth)}/s";
											DownlinkSpeedLabel.Text = $"↑：{Utils.ComputeBandwidth(stats.BytesReceived - DownlinkBandwidth)}/s";

											UplinkBandwidth = stats.BytesSent;
											DownlinkBandwidth = stats.BytesReceived;
										}
									}
								});
							}
                        }
                        else
                        {
							UplinkBandwidth = 0;
							DownlinkBandwidth = 0;
							UsedBandwidthLabel.Text = "已使用：0 KB";
                            UplinkSpeedLabel.Text = "↑：0 KB/s";
                            DownlinkSpeedLabel.Text = "↓：0 KB/s";
                        }

						if (count > 100000)
						{
							count = 0;
						}
						else
						{
							count++;
						}

                        // 休眠 100 毫秒
                        Thread.Sleep(100);
                    }
                    catch (Exception)
                    {
                        // 跳过
                    }
                }
            });

            new Tray().InitTray();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Started)
            {
                e.Cancel = true;

                MessageBox.Show("请先点击关闭按钮", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
				Utils.Config.SaveToFile();
				Utils.Shell.ExecuteCommandNoWait("taskkill", "/f", "/t", "/im", "x2tap.exe");
                //Environment.Exit(0);
            }
        }

        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var cbx = sender as ComboBox;
            if (cbx != null)
            {
                e.DrawBackground();

                if (e.Index >= 0)
                {
                    var sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    var brush = new SolidBrush(cbx.ForeColor);

                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        brush = SystemBrushes.HighlightText as SolidBrush;
                    }

                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

		private void AddSocks5ServerToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void AddV2RayServerButton_Click(object sender, EventArgs e)
        {
			(Global.Views.Server.V2Ray = new Server.V2Ray()).Show();
			Hide();
		}

		private void AddShadowsocksServerButton_Click(object sender, EventArgs e)
        {
            (Global.Views.Server.Shadowsocks = new Server.Shadowsocks()).Show();
            Hide();
        }

		private void AddShadowsocksRServerButton_Click(object sender, EventArgs e)
		{
			(Global.Views.Server.ShadowsocksR = new Server.ShadowsocksR()).Show();
			Hide();
		}

		private void TelegramGroupButton_Click(object sender, EventArgs e)
		{
			Utils.Shell.ExecuteCommandNoWait("START", "https://t.me/x2tapChat");
		}

		private void TelegramChannelButton_Click(object sender, EventArgs e)
		{
			Utils.Shell.ExecuteCommandNoWait("START", "https://t.me/x2tap");
		}

		private void GithubButton_Click(object sender, EventArgs e)
		{
			Utils.Shell.ExecuteCommandNoWait("START", "https://github.com/hacking001/x2tap");
		}

		private void DeleteButton_Click(object sender, EventArgs e)
        {
            var index = ProxyComboBox.SelectedIndex;
            if (index != -1)
            {
                ProxyComboBox.Items.RemoveAt(index);

                if (index < Global.V2RayProxies.Count)
                {
                    Global.V2RayProxies.RemoveAt(index);
                }
                else if (index < Global.V2RayProxies.Count + Global.ShadowsocksProxies.Count)
                {
                    Global.ShadowsocksProxies.RemoveAt(index - Global.V2RayProxies.Count);
                }
				else
				{
					Global.ShadowsocksRProxies.RemoveAt(index - Global.V2RayProxies.Count - Global.ShadowsocksProxies.Count);
				}

                if (ProxyComboBox.Items.Count < index)
                {
                    ProxyComboBox.SelectedIndex = index;
                }
                else if (ProxyComboBox.Items.Count == 1)
                {
                    ProxyComboBox.SelectedIndex = 0;
                }
                else
                {
                    ProxyComboBox.SelectedIndex = index - 1;
                }
            }
            else
            {
                MessageBox.Show("请选择一个代理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ProxyComboBox.SelectedIndex != -1)
            {
                if (ProxyComboBox.SelectedIndex < Global.V2RayProxies.Count)
                {
                    (Global.Views.Server.V2Ray = new Server.V2Ray(true, ProxyComboBox.SelectedIndex)).Show();
                }
                else if (ProxyComboBox.SelectedIndex < Global.V2RayProxies.Count + Global.ShadowsocksProxies.Count)
                {
                    (Global.Views.Server.Shadowsocks = new Server.Shadowsocks(true, ProxyComboBox.SelectedIndex - Global.V2RayProxies.Count)).Show();
                }
                else
                {
                    (Global.Views.Server.ShadowsocksR = new Server.ShadowsocksR(true, ProxyComboBox.SelectedIndex - Global.V2RayProxies.Count - Global.ShadowsocksProxies.Count)).Show();
                }

                Hide();
            }
            else
            {
                MessageBox.Show("请选择一个代理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SubscribeButton_Click(object sender, EventArgs e)
        {
            (Global.Views.SubscribeForm = new SubscribeForm()).Show();
            Hide();
        }

        private void AdvancedButton_Click(object sender, EventArgs e)
        {
            (Global.Views.AdvancedForm = new AdvancedForm()).Show();
            Hide();
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            if (!Started)
            {
                if (ProxyComboBox.SelectedIndex != -1)
                {
                    if (Utils.TUNTAP.GetComponentID() != "")
                    {
                        Status = "执行中";
						Reset(false);
                        ControlButton.Text = "执行中";

						Task.Run(() =>
						{
							try
							{
								if (ProxyComboBox.Text.StartsWith("[v2ray]") || ProxyComboBox.Text.StartsWith("[SS]"))
								{
									//////////////////////////////////////////////////
									// 处理配置文件
									//////////////////////////////////////////////////
									Thread.Sleep(1000);
									Status = "正在生成配置文件中";
									if (ProxyComboBox.Text.StartsWith("[v2ray]"))
									{
										if (ModeComboBox.SelectedIndex == 0)      // [内置规则] 绕过局域网和中国
										{
											File.WriteAllText("x2tap.txt", Utils.Config.GetV2Ray(Global.V2RayProxies[ProxyComboBox.SelectedIndex]));
										}
										else if (ModeComboBox.SelectedIndex == 1) // [内置规则] 绕过局域网
										{
											File.WriteAllText("x2tap.txt", Utils.Config.GetV2Ray(Global.V2RayProxies[ProxyComboBox.SelectedIndex], false));
										}
										else                                      // [外置规则]
										{
											if (Global.Modes[ModeComboBox.SelectedIndex - 2].BypassChina)
											{
												File.WriteAllText("x2tap.txt", Utils.Config.GetV2Ray(Global.V2RayProxies[ProxyComboBox.SelectedIndex]));
											}
											else
											{
												File.WriteAllText("x2tap.txt", Utils.Config.GetV2Ray(Global.V2RayProxies[ProxyComboBox.SelectedIndex], false));
											}
										}
									}
									else
									{
										if (ModeComboBox.SelectedIndex == 0)      // [内置规则] 绕过局域网和中国
										{
											File.WriteAllText("x2tap.txt", Utils.Config.GetShadowsocks(Global.ShadowsocksProxies[ProxyComboBox.SelectedIndex - Global.V2RayProxies.Count]));
										}
										else if (ModeComboBox.SelectedIndex == 1) // [内置规则] 绕过局域网
										{
											File.WriteAllText("x2tap.txt", Utils.Config.GetShadowsocks(Global.ShadowsocksProxies[ProxyComboBox.SelectedIndex - Global.V2RayProxies.Count], false));
										}
										else                                      // [外置规则]
										{
											if (Global.Modes[ModeComboBox.SelectedIndex - 2].BypassChina)
											{
												File.WriteAllText("x2tap.txt", Utils.Config.GetShadowsocks(Global.ShadowsocksProxies[ProxyComboBox.SelectedIndex - Global.V2RayProxies.Count]));
											}
											else
											{
												File.WriteAllText("x2tap.txt", Utils.Config.GetShadowsocks(Global.ShadowsocksProxies[ProxyComboBox.SelectedIndex - Global.V2RayProxies.Count], false));
											}
										}
									}

									//////////////////////////////////////////////////
									// 启动 v2ray 程序
									//////////////////////////////////////////////////
									Thread.Sleep(1000);
									Status = "正在启动 v2ray 中";
									Utils.Shell.ExecuteCommandNoWait("START", "RunHiddenConsole.exe", "wv2ray.exe", "-config", "x2tap.txt");

									//////////////////////////////////////////////////
									// 检查 v2ray 状态
									//////////////////////////////////////////////////
									Thread.Sleep(2000);
									if (!Utils.CheckTCPPortOpen(2810))
									{
										Status = "检测到 v2ray 启动失败";
										Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "wv2ray.exe");
										throw new InvalidOperationException();
									}
								}
								else if (ProxyComboBox.Text.StartsWith("[SSR]"))
								{
									//////////////////////////////////////////////////
									// 处理配置文件
									//////////////////////////////////////////////////
									Thread.Sleep(1000);
									Status = "正在生成配置文件中";
									File.WriteAllText("x2tap.txt", Utils.Config.GetShadowsocksR(Global.ShadowsocksRProxies[ProxyComboBox.SelectedIndex - Global.V2RayProxies.Count - Global.ShadowsocksProxies.Count]));

									//////////////////////////////////////////////////
									// 启动 SSR 程序
									//////////////////////////////////////////////////
									Thread.Sleep(1000);
									Status = "正在启动 SSR 中";
									Utils.Shell.ExecuteCommandNoWait("START", "RunHiddenConsole.exe", "ssr-local.exe", "-c", "x2tap.txt");

									//////////////////////////////////////////////////
									// 检查 SSR 状态
									//////////////////////////////////////////////////
									Thread.Sleep(4000);
									if (!Utils.CheckTCPPortOpen(2810))
									{
										Status = "检测到 SSR 启动失败";
										Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "ssr-local.exe");
										throw new InvalidOperationException();
									}

									if (!Global.Config.TUNTAP.UseCustomDNS)
									{
										//////////////////////////////////////////////////
										// 启动 dnscrypt-proxy 程序
										//////////////////////////////////////////////////
										Thread.Sleep(1000);
										Status = "正在启动 dnscrypt-proxy 中";
										Utils.Shell.ExecuteCommandNoWait("START", "RunHiddenConsole.exe", "dnscrypt-proxy.exe");

										//////////////////////////////////////////////////
										// 检查 dnscrypt-proxy 状态
										//////////////////////////////////////////////////
										Thread.Sleep(2000);
										if (Process.GetProcessesByName("dnscrypt-proxy").Length != 0)
										{
											foreach (var address in Dns.GetHostAddresses("ea-dns.rubyfish.cn"))
											{
												Utils.Route.Add(address.ToString(), "255.255.255.255", Global.Config.AdapterGateway);
											}

											foreach (var address in Dns.GetHostAddresses("uw-dns.rubyfish.cn"))
											{
												Utils.Route.Add(address.ToString(), "255.255.255.255", Global.Config.AdapterGateway);
											}

											Utils.Route.Add("1.2.4.8", "255.255.255.255", Global.Config.AdapterGateway);
											Utils.Route.Add("8.8.8.8", "255.255.255.255", Global.Config.TUNTAP.Gateway);
										}
										else
										{
											Status = "检测到 dnscrypt-proxy 启动失败";
											Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "ssr-local.exe");
											throw new InvalidOperationException();
										}
									}
									else
									{
										foreach (var address in Global.Config.TUNTAP.DNS.Split(','))
										{
											Utils.Route.Add(address, "255.255.255.255", Global.Config.AdapterGateway);
										}
									}
								}
								else
								{
									throw new NotSupportedException(String.Format("不支持的代理：{0}", ProxyComboBox.Text));
								}

								//////////////////////////////////////////////////
								// 启动 tun2socks 程序
								//////////////////////////////////////////////////
								Thread.Sleep(1000);
								Status = "正在启动 tun2socks 中";
								Utils.Shell.ExecuteCommandNoWait("START", "RunHiddenConsole.exe", "tun2socks.exe", "-enable-dns-cache", "-local-socks-addr", "127.0.0.1:2810", "-tun-address", Global.Config.TUNTAP.Address, "-tun-mask", Global.Config.TUNTAP.Netmask, "-tun-gw", Global.Config.TUNTAP.Gateway, "-tun-dns", Global.Config.TUNTAP.DNS);

								//////////////////////////////////////////////////
								// 检查 tun2socks 状态
								//////////////////////////////////////////////////
								Thread.Sleep(2000);
								if (Process.GetProcessesByName("tun2socks").Length == 0)
								{
									Status = "检测到 tun2socks 启动失败";
									Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "dnscrypt-proxy.exe");
									Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "ssr-local.exe");
									throw new InvalidOperationException();
								}

								//////////////////////////////////////////////////
								// 配置路由表
								//////////////////////////////////////////////////
								Thread.Sleep(1000);
								Status = "正在配置 路由表 中";
								if (ProxyComboBox.Text.StartsWith("[SSR]"))
								{
									foreach (var address in Dns.GetHostAddresses(Global.ShadowsocksRProxies[ProxyComboBox.SelectedIndex - Global.V2RayProxies.Count - Global.ShadowsocksProxies.Count].Address))
									{
										Utils.Route.Add(address.ToString(), "255.255.255.255", Global.Config.AdapterGateway);
									}
								}

								if (ModeComboBox.SelectedIndex == 0 || ModeComboBox.SelectedIndex == 1 || Global.Modes[ModeComboBox.SelectedIndex - 2].Type == 0)
								{
									if (!Utils.Route.Add("0.0.0.0", "0.0.0.0", Global.Config.TUNTAP.Gateway))
									{
										Utils.Route.Delete("0.0.0.0", "0.0.0.0", Global.Config.TUNTAP.Gateway);
										Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "tun2socks.exe");
										Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "dnscrypt-proxy.exe");
										Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "ssr-local.exe");
										Status = "在操作路由表时发生错误";
										Reset();
										MessageBox.Show("在操作路由表时发生错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
										throw new InvalidOperationException();
									}

									if (!Utils.Route.Add("0.0.0.0", "128.0.0.0", Global.Config.TUNTAP.Gateway))
									{
										Utils.Route.Delete("0.0.0.0", "0.0.0.0", Global.Config.TUNTAP.Gateway);
										Utils.Route.Delete("0.0.0.0", "128.0.0.0", Global.Config.TUNTAP.Gateway);
										Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "tun2socks.exe");
										Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "dnscrypt-proxy.exe");
										Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "ssr-local.exe");
										Status = "在操作路由表时发生错误";
										Reset();
										MessageBox.Show("在操作路由表时发生错误", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
										throw new InvalidOperationException();
									}
								}

								//////////////////////////////////////////////////
								// 配置路由表 - 处理 SSR 的绕过中国
								//////////////////////////////////////////////////
								if (ProxyComboBox.Text.StartsWith("[SSR]") && ModeComboBox.SelectedIndex == 1)
								{

								}

								//////////////////////////////////////////////////
								// 配置路由表 - 处理外置规则
								//////////////////////////////////////////////////
								if (ModeComboBox.SelectedIndex != 0 && ModeComboBox.SelectedIndex != 1)
								{
									var mode = Global.Modes[ModeComboBox.SelectedIndex - 2];
									foreach (var rule in mode.Rule)
									{
										var splited = rule.Split('/');
										if (splited.Length == 2)
										{
											if (mode.Type == 1)
											{
												Utils.Route.Add(splited[0], Utils.Route.TranslateCIDR(splited[1]), Global.Config.TUNTAP.Gateway);
											}
											else
											{
												Utils.Route.Add(splited[0], Utils.Route.TranslateCIDR(splited[1]), Global.Config.AdapterGateway);
											}
										}
									}
								}

								//////////////////////////////////////////////////
								// 清理 DNS 缓存
								//////////////////////////////////////////////////
								Thread.Sleep(1000);
								Status = "正在清理 DNS 缓存中";
								Utils.Shell.ExecuteCommandNoWait("IPCONFIG", "/FLUSHDNS");

								//////////////////////////////////////////////////
								// 最后的操作
								//////////////////////////////////////////////////
								Thread.Sleep(1000);
								Status = "已启动，请自行检查网络是否正常";
								Started = true;
								ControlButton.Text = "停止";
								ControlButton.Enabled = true;
							}
							catch (InvalidOperationException)
							{
								Reset();
								ControlButton.Text = "启动";
							}
							catch (Exception ex)
							{
								MessageBox.Show(ex.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
								Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "tun2socks.exe");
								Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "dnscrypt-proxy.exe");
								Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "wv2ray.exe");
								Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "ssr-local.exe");
								Reset();
								ControlButton.Text = "启动";
							}
						});
                    }
                    else
                    {
                        MessageBox.Show("未检测到 TUN/TAP 适配器，请检查 TAP-Windows 驱动是否正确安装！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("请选择一个代理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                ControlButton.Text = "执行中";
                ControlButton.Enabled = false;

				Task.Run(() =>
				{
					//////////////////////////////////////////////////
					// 配置路由表
					//////////////////////////////////////////////////
					Thread.Sleep(1000);
					Status = "正在重置 路由表 中";
					Utils.Route.Delete("0.0.0.0", "0.0.0.0", Global.Config.TUNTAP.Gateway);
					Utils.Route.Delete("0.0.0.0", "128.0.0.0", Global.Config.TUNTAP.Gateway);
					if (ModeComboBox.SelectedIndex != 0 && ModeComboBox.SelectedIndex != 1)
					{
						foreach (var rule in Global.Modes[ModeComboBox.SelectedIndex - 2].Rule)
						{
							var splited = rule.Split('/');
							if (splited.Length == 2)
							{
								Utils.Route.Delete(splited[0]);
							}
						}
					}

					//////////////////////////////////////////////////
					// 配置路由表 - 处理 SSR 的绕过中国
					//////////////////////////////////////////////////
					if (ProxyComboBox.Text.StartsWith("[SSR]") && ModeComboBox.SelectedIndex == 1)
					{

					}

					//////////////////////////////////////////////////
					// 关闭 tun2socks 程序
					//////////////////////////////////////////////////
					Thread.Sleep(1000);
					Status = "正在停止 tun2socks 中";
					Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "tun2socks.exe");

					//////////////////////////////////////////////////
					// 停止 SSR 程序
					//////////////////////////////////////////////////
					if (!ProxyComboBox.Text.StartsWith("[SSR]"))
					{
						Thread.Sleep(1000);
						Status = "正在停止 v2ray 中";
						Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "wv2ray.exe");
					}
					else
					{
						Thread.Sleep(1000);
						Status = "正在停止 dnscrypt-proxy 中";
						Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "dnscrypt-proxy.exe");

						Thread.Sleep(1000);
						Status = "正在停止 SSR 中";
						Utils.Shell.ExecuteCommandNoWait("TASKKILL", "/F", "/T", "/IM", "ssr-local.exe");
					}

					Status = "已停止";
					Started = false;
					Reset();
				});
            }
        }

		private void Reset(bool type = true)
		{
			ProxyComboBox.Enabled = type;
			ModeComboBox.Enabled = type;
			AddV2RayServerButton.Enabled = type;
			AddShadowsocksServerButton.Enabled = type;
			AddShadowsocksRServerButton.Enabled = type;
			DeleteButton.Enabled = type;
			EditButton.Enabled = type;
			SubscribeButton.Enabled = type;
			AdvancedButton.Enabled = type;
			ControlButton.Text = "启动";
			ControlButton.Enabled = type;
		}
	}
}