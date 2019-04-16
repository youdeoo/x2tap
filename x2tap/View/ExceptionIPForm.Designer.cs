namespace x2tap.View
{
	partial class ExceptionIPForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionIPForm));
			this.AddGroupBox = new System.Windows.Forms.GroupBox();
			this.AddButton = new System.Windows.Forms.Button();
			this.NetmaskComboBox = new System.Windows.Forms.ComboBox();
			this.AddressTextBox = new System.Windows.Forms.TextBox();
			this.AddressLabel = new System.Windows.Forms.Label();
			this.NetmaskLabel = new System.Windows.Forms.Label();
			this.ListGroupBox = new System.Windows.Forms.GroupBox();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.IPListBox = new System.Windows.Forms.ListBox();
			this.ControlButton = new System.Windows.Forms.Button();
			this.AddGroupBox.SuspendLayout();
			this.ListGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// AddGroupBox
			// 
			this.AddGroupBox.Controls.Add(this.AddButton);
			this.AddGroupBox.Controls.Add(this.NetmaskComboBox);
			this.AddGroupBox.Controls.Add(this.AddressTextBox);
			this.AddGroupBox.Controls.Add(this.AddressLabel);
			this.AddGroupBox.Controls.Add(this.NetmaskLabel);
			this.AddGroupBox.Location = new System.Drawing.Point(12, 12);
			this.AddGroupBox.Name = "AddGroupBox";
			this.AddGroupBox.Size = new System.Drawing.Size(310, 105);
			this.AddGroupBox.TabIndex = 0;
			this.AddGroupBox.TabStop = false;
			// 
			// AddButton
			// 
			this.AddButton.Location = new System.Drawing.Point(229, 76);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(75, 23);
			this.AddButton.TabIndex = 4;
			this.AddButton.Text = "添加";
			this.AddButton.UseVisualStyleBackColor = true;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// NetmaskComboBox
			// 
			this.NetmaskComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.NetmaskComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.NetmaskComboBox.FormattingEnabled = true;
			this.NetmaskComboBox.Location = new System.Drawing.Point(44, 45);
			this.NetmaskComboBox.Name = "NetmaskComboBox";
			this.NetmaskComboBox.Size = new System.Drawing.Size(260, 24);
			this.NetmaskComboBox.TabIndex = 2;
			this.NetmaskComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBox_DrawItem);
			// 
			// AddressTextBox
			// 
			this.AddressTextBox.Location = new System.Drawing.Point(44, 16);
			this.AddressTextBox.Name = "AddressTextBox";
			this.AddressTextBox.Size = new System.Drawing.Size(260, 23);
			this.AddressTextBox.TabIndex = 1;
			this.AddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// AddressLabel
			// 
			this.AddressLabel.AutoSize = true;
			this.AddressLabel.Location = new System.Drawing.Point(6, 19);
			this.AddressLabel.Name = "AddressLabel";
			this.AddressLabel.Size = new System.Drawing.Size(44, 17);
			this.AddressLabel.TabIndex = 0;
			this.AddressLabel.Text = "地址：";
			// 
			// NetmaskLabel
			// 
			this.NetmaskLabel.AutoSize = true;
			this.NetmaskLabel.Location = new System.Drawing.Point(6, 48);
			this.NetmaskLabel.Name = "NetmaskLabel";
			this.NetmaskLabel.Size = new System.Drawing.Size(44, 17);
			this.NetmaskLabel.TabIndex = 3;
			this.NetmaskLabel.Text = "掩码：";
			// 
			// ListGroupBox
			// 
			this.ListGroupBox.Controls.Add(this.DeleteButton);
			this.ListGroupBox.Controls.Add(this.IPListBox);
			this.ListGroupBox.Location = new System.Drawing.Point(12, 123);
			this.ListGroupBox.Name = "ListGroupBox";
			this.ListGroupBox.Size = new System.Drawing.Size(310, 308);
			this.ListGroupBox.TabIndex = 1;
			this.ListGroupBox.TabStop = false;
			// 
			// DeleteButton
			// 
			this.DeleteButton.Location = new System.Drawing.Point(229, 279);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(75, 23);
			this.DeleteButton.TabIndex = 1;
			this.DeleteButton.Text = "删除";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// IPListBox
			// 
			this.IPListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.IPListBox.FormattingEnabled = true;
			this.IPListBox.Location = new System.Drawing.Point(6, 22);
			this.IPListBox.Name = "IPListBox";
			this.IPListBox.Size = new System.Drawing.Size(298, 251);
			this.IPListBox.TabIndex = 0;
			this.IPListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_DrawItem);
			// 
			// ControlButton
			// 
			this.ControlButton.Location = new System.Drawing.Point(247, 437);
			this.ControlButton.Name = "ControlButton";
			this.ControlButton.Size = new System.Drawing.Size(75, 23);
			this.ControlButton.TabIndex = 2;
			this.ControlButton.Text = "保存";
			this.ControlButton.UseVisualStyleBackColor = true;
			this.ControlButton.Click += new System.EventHandler(this.ControlButton_Click);
			// 
			// ExceptionIPForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(334, 472);
			this.Controls.Add(this.ControlButton);
			this.Controls.Add(this.ListGroupBox);
			this.Controls.Add(this.AddGroupBox);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "ExceptionIPForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "全局例外 IP";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExceptionIPForm_FormClosing);
			this.Load += new System.EventHandler(this.ExceptionIPForm_Load);
			this.AddGroupBox.ResumeLayout(false);
			this.AddGroupBox.PerformLayout();
			this.ListGroupBox.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox AddGroupBox;
		private System.Windows.Forms.Label AddressLabel;
		private System.Windows.Forms.TextBox AddressTextBox;
		private System.Windows.Forms.Label NetmaskLabel;
		private System.Windows.Forms.ComboBox NetmaskComboBox;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.GroupBox ListGroupBox;
		private System.Windows.Forms.ListBox IPListBox;
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.Button ControlButton;
	}
}