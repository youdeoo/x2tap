using System;
using System.Drawing;
using System.Windows.Forms;

namespace x2tap.View
{
    public partial class AdvancedForm : Form
    {
        public AdvancedForm()
        {
            InitializeComponent();
        }

        private void AdvancedForm_Load(object sender, EventArgs e)
        {
            V2RayLoggingLevelComboBox.SelectedIndex = Global.Config.V2RayLoggingLevel;
			TUNTAPAddressTextBox.Text = Global.Config.TUNTAP.Address;
			TUNTAPNetmaskTextBox.Text = Global.Config.TUNTAP.Netmask;
			TUNTAPGatewayTextBox.Text = Global.Config.TUNTAP.Gateway;
			TUNTAPDNSTextBox.Text = Global.Config.TUNTAP.DNS;
			TUNTAPUseCustomDNSCheckBox.Checked = Global.Config.TUNTAP.UseCustomDNS;
		}

        private void AdvancedForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void GlobalExceptionIPButton_Click(object sender, EventArgs e)
		{
			(Global.Views.ExceptionIPForm = new ExceptionIPForm()).Show();
			Hide();
		}

		private void ControlButton_Click(object sender, EventArgs e)
        {
            Global.Config.V2RayLoggingLevel = V2RayLoggingLevelComboBox.SelectedIndex;
			Global.Config.TUNTAP.Address = TUNTAPAddressTextBox.Text;
			Global.Config.TUNTAP.Netmask = TUNTAPNetmaskTextBox.Text;
			Global.Config.TUNTAP.Gateway = TUNTAPGatewayTextBox.Text;
			Global.Config.TUNTAP.DNS = TUNTAPDNSTextBox.Text;
			Global.Config.TUNTAP.UseCustomDNS = TUNTAPUseCustomDNSCheckBox.Checked;

            MessageBox.Show("保存成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Global.Views.MainForm.Show();
            Close();
        }
	}
}