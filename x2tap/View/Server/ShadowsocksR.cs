using System;
using System.Drawing;
using System.Windows.Forms;

namespace x2tap.View.Server
{
    public partial class ShadowsocksR : Form
    {
        /// <summary>
        ///     服务器配置信息索引
        /// </summary>
        public int Index;

        /// <summary>
        ///     窗体工作模式
        /// </summary>
        public bool Mode;

        /// <summary>
        ///     初始化窗体
        /// </summary>
        /// <param name="mode">是否为编辑模式</param>
        /// <param name="index">服务器配置信息索引</param>
        public ShadowsocksR(bool mode = false, int index = 0)
        {
            InitializeComponent();

            Mode = mode;
            Index = index;

            if (Mode)
            {
                RemarkTextBox.Text = Global.ShadowsocksRProxies[Index].Remark;
                AddressTextBox.Text = Global.ShadowsocksRProxies[Index].Address;
                PortTextBox.Text = Global.ShadowsocksRProxies[Index].Port.ToString();
                EncryptMethodComboBox.SelectedIndex = Global.ShadowsocksRProxies[Index].EncryptMethod;
                PasswordTextBox.Text = Global.ShadowsocksRProxies[Index].Password;
				ProtocolComboBox.SelectedIndex = Global.ShadowsocksRProxies[Index].Protocol;
				ProtocolParamTextBox.Text = Global.ShadowsocksRProxies[Index].ProtocolParam;
				OBFSComboBox.SelectedIndex = Global.ShadowsocksRProxies[Index].OBFS;
				OBFSParamTextBox.Text = Global.ShadowsocksRProxies[Index].OBFSParam;
                ControlButton.Text = "保存";
            }
            else
            {
                EncryptMethodComboBox.SelectedIndex = 0;
            }
        }

        private void ShadowsocksR_Load(object sender, EventArgs e)
        {
        }

        private void ShadowsocksR_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.Views.MainForm.Show();
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

        private void ControlButton_Click(object sender, EventArgs e)
        {
            if (Mode)
            {
                Global.ShadowsocksRProxies[Index] = new Objects.Server.ShadowsocksR
                {
                    Remark = RemarkTextBox.Text,
                    Address = AddressTextBox.Text,
                    Port = int.Parse(PortTextBox.Text),
                    EncryptMethod = EncryptMethodComboBox.SelectedIndex,
                    Password = PasswordTextBox.Text,
					Protocol = ProtocolComboBox.SelectedIndex,
					ProtocolParam = ProtocolParamTextBox.Text,
					OBFS = OBFSComboBox.SelectedIndex,
					OBFSParam = OBFSParamTextBox.Text
				};

                MessageBox.Show("保存成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Global.ShadowsocksRProxies.Add(new Objects.Server.ShadowsocksR
                {
                    Remark = RemarkTextBox.Text,
                    Address = AddressTextBox.Text,
                    Port = int.Parse(PortTextBox.Text),
                    EncryptMethod = EncryptMethodComboBox.SelectedIndex,
                    Password = PasswordTextBox.Text,
					Protocol = ProtocolComboBox.SelectedIndex,
					ProtocolParam = ProtocolParamTextBox.Text,
					OBFS = OBFSComboBox.SelectedIndex,
					OBFSParam = OBFSParamTextBox.Text
                });

                MessageBox.Show("添加成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Global.Views.MainForm.InitProxies();
            Close();
        }
	}
}