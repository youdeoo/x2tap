using System;
using System.Drawing;
using System.Windows.Forms;

namespace x2tap.View
{
	public partial class ExceptionIPForm : Form
	{
		public ExceptionIPForm()
		{
			InitializeComponent();
		}

		private void ExceptionIPForm_Load(object sender, EventArgs e)
		{
			foreach (var item in Global.ExceptionIPs)
			{
				IPListBox.Items.Add(item);
			}

			for (int i = 32; i >= 0; i--)
			{
				NetmaskComboBox.Items.Add(i);
			}
			NetmaskComboBox.SelectedIndex = 0;
		}

		private void ExceptionIPForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Global.Views.AdvancedForm.Show();
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

					e.Graphics.DrawString(IPListBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, sf);
				}
			}
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			IPListBox.Items.Add(String.Format("{0}/{1}", AddressTextBox.Text, NetmaskComboBox.Text));
		}

		private void DeleteButton_Click(object sender, EventArgs e)
		{
			if (IPListBox.SelectedIndex != -1)
			{
				var index = IPListBox.SelectedIndex;
				IPListBox.Items.RemoveAt(index);

				index--;
				if (IPListBox.Items.Count > 0 && index < IPListBox.Items.Count)
				{
					IPListBox.SelectedIndex = index;
				}
			}
			else
			{
				MessageBox.Show("请选择一个项目", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void ControlButton_Click(object sender, EventArgs e)
		{
			Global.ExceptionIPs.Clear();
			for (int i = 0; i < IPListBox.Items.Count; i++)
			{
				Global.ExceptionIPs.Add(IPListBox.Items[i].ToString());
			}

			MessageBox.Show("保存成功", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
