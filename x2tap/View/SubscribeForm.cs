using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using x2tap.Override;
using x2tap.Utils;

namespace x2tap.View
{
    public partial class SubscribeForm : Form
    {
        public SubscribeForm()
        {
            InitializeComponent();
        }

        private void SubscribeForm_Load(object sender, EventArgs e)
        {
			foreach (var link in Global.SubscriptionLinks)
			{
				SubscribeLinksListBox.Items.Add(link);
			}
        }

        private void SubscribeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.Views.MainForm.Show();
        }

		private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index != -1)
			{
				e.DrawBackground();
				e.DrawFocusRectangle();

				using (var sf = new StringFormat())
				{
					sf.Alignment = StringAlignment.Center;
					sf.LineAlignment = StringAlignment.Center;

					e.Graphics.DrawString(SubscribeLinksListBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, sf);
				}
			}
		}

		private void AddSubscribeLinkButton_Click(object sender, EventArgs e)
		{
			string link = Interaction.InputBox("请输入订阅链接", "添加订阅链接", "");
			if (link != "")
			{
				Global.SubscriptionLinks.Add(link);
				SubscribeLinksListBox.Items.Add(link);
			}
		}

		private void DeleteSubscribeLinkButton_Click(object sender, EventArgs e)
		{
			if (SubscribeLinksListBox.SelectedIndex != -1)
			{
				Global.SubscriptionLinks.RemoveAt(SubscribeLinksListBox.SelectedIndex);
				SubscribeLinksListBox.Items.RemoveAt(SubscribeLinksListBox.SelectedIndex);
			}
			else
			{
				MessageBox.Show("请选择一个订阅链接", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void SubscribeLinksButton_Click(object sender, EventArgs e)
        {
			var count = 0;
			var v2rayProxies = new List<Objects.Server.v2ray>();
			var ShadowsocksProxies = new List<Objects.Server.Shadowsocks>();
			foreach (string link in Global.SubscriptionLinks)
			{
				using (var client = new WebClient())
				{
					try
					{
						var response = client.DownloadString(link);
						if (response != "")
						{
							if (response.Length % 4 != 0)
							{
								for (var i = 0; i < response.Length % 4; i++)
								{
									response += "=";
								}
							}

							response = Encoding.UTF8.GetString(Convert.FromBase64String(response));

							using (var sr = new StringReader(response))
							{
								string text;

								while ((text = sr.ReadLine()) != null)
								{
									count++;

									if (text.StartsWith("vmess://"))
									{
										v2rayProxies.Add(Parse.v2ray(text));
									}
									else if (text.StartsWith("ss://"))
									{
										ShadowsocksProxies.Add(Parse.Shadowsocks(text));
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						if (MessageBox.Show(string.Format("在处理订阅链接 \"{0}\" 时发生错误：{1}\n\n是否终止导入？", link, ex.Message), "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
						{
							continue;
						}
					}
				}
			}

			Global.v2rayProxies = v2rayProxies;
			Global.ShadowsocksProxies = ShadowsocksProxies;
			Global.Views.MainForm.InitProxies();
			MessageBox.Show(string.Format("成功导入 {0} 条代理", count), "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

        private void SubscribeTextButton_Click(object sender, EventArgs e)
        {
            if (SubscribeTextTextBox.Text != "")
            {
                Global.v2rayProxies.Clear();
                Global.ShadowsocksProxies.Clear();

                using (var sr = new StringReader(SubscribeTextTextBox.Text))
                {
                    var i = 0;
                    string text;

                    while ((text = sr.ReadLine()) != null)
                    {
                        i++;

                        if (text.StartsWith("vmess://"))
                        {
                            Global.v2rayProxies.Add(Parse.v2ray(text));
                        }
                        else if (text.StartsWith("ss://"))
                        {
                            Global.ShadowsocksProxies.Add(Parse.Shadowsocks(text));
                        }
                    }

					Global.Views.MainForm.InitProxies();
                    MessageBox.Show(string.Format("成功导入 {0} 条代理", i), "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("文本为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
	}
}