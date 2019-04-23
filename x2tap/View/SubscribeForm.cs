using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
			var count = new int[] { 0, 0, 0 };
			var v2rayProxies = new List<Objects.Server.V2Ray>();
			var ShadowsocksProxies = new List<Objects.Server.Shadowsocks>();
			var ShadowsocksRProxies = new List<Objects.Server.ShadowsocksR>();
			foreach (string link in Global.SubscriptionLinks)
			{
				using (var client = new Override.WebClient())
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

							response = Utils.UrlSafeBase64Decode(response);
							using (var sr = new StringReader(response))
							{
								string text;

								while ((text = sr.ReadLine()) != null)
								{
									count[0]++;

									if (text.StartsWith("vmess://"))
									{
										v2rayProxies.Add(Utils.Parse.v2ray(text));
									}
									else if (text.StartsWith("ss://"))
									{
										ShadowsocksProxies.Add(Utils.Parse.Shadowsocks(text));
									}
									else if (text.StartsWith("ssr://"))
									{
										ShadowsocksRProxies.Add(Utils.Parse.ShadowsocksR(text));
									}
									else
									{
										count[2]++;
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						count[1]++;

						if (MessageBox.Show(string.Format("在处理订阅链接：\"{0}\" 时发生错误：{1}\n\n是否终止导入？", link, ex), "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
						{
							continue;
						}
					}
				}
			}

			Global.V2RayProxies = v2rayProxies;
			Global.ShadowsocksProxies = ShadowsocksProxies;
			Global.ShadowsocksRProxies = ShadowsocksRProxies;
			Global.Views.MainForm.InitProxies();
			MessageBox.Show(String.Format("总共 {0} 条\n成功导入 {1} 条\n导入失败 {2} 条\n无法处理 {3} 条", count[0], count[0] - count[1] - count[2], count[1], count[2]), "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

        private void SubscribeTextButton_Click(object sender, EventArgs e)
        {
            if (SubscribeTextTextBox.Text != "")
            {
                using (var sr = new StringReader(SubscribeTextTextBox.Text))
                {
					var count = new int[] { 0, 0, 0 };
                    string text;

                    while ((text = sr.ReadLine()) != null)
                    {
						count[0]++;

						try
						{
							if (text.StartsWith("vmess://"))
							{
								Global.V2RayProxies.Add(Utils.Parse.v2ray(text));
							}
							else if (text.StartsWith("ss://"))
							{
								Global.ShadowsocksProxies.Add(Utils.Parse.Shadowsocks(text));
							}
							else if (text.StartsWith("ssr://"))
							{
								Global.ShadowsocksRProxies.Add(Utils.Parse.ShadowsocksR(text));
							}
							else
							{
								count[2]++;
							}
						}
                        catch (NotSupportedException ex)
						{
							MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						catch (Exception)
						{
							count[1]++;
						}
					}

					Global.Views.MainForm.InitProxies();
					MessageBox.Show(String.Format("总共 {0} 条\n成功导入 {1} 条\n导入失败 {2} 条\n无法处理 {3} 条", count[0], count[0] - count[1] - count[2], count[1], count[2]), "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
            }
            else
            {
                MessageBox.Show("文本为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
	}
}