namespace x2tap.View
{
    partial class AdvancedForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedForm));
			this.V2RayLoggingLevelGroupBox = new System.Windows.Forms.GroupBox();
			this.V2RayLoggingLevelComboBox = new System.Windows.Forms.ComboBox();
			this.ControlButton = new System.Windows.Forms.Button();
			this.TUNTAPGroupBox = new System.Windows.Forms.GroupBox();
			this.TUNTAPDNSTextBox = new System.Windows.Forms.TextBox();
			this.TUNTAPGatewayTextBox = new System.Windows.Forms.TextBox();
			this.TUNTAPNetmaskTextBox = new System.Windows.Forms.TextBox();
			this.TUNTAPNetmaskLabel = new System.Windows.Forms.Label();
			this.TUNTAPAddressTextBox = new System.Windows.Forms.TextBox();
			this.TUNTAPAddressLabel = new System.Windows.Forms.Label();
			this.TUNTAPUseCustomDNSCheckBox = new System.Windows.Forms.CheckBox();
			this.TUNTAPGatewayLabel = new System.Windows.Forms.Label();
			this.TUNTAPDNSLabel = new System.Windows.Forms.Label();
			this.GlobalExceptionIPButton = new System.Windows.Forms.Button();
			this.TUNTAPNameLabel = new System.Windows.Forms.Label();
			this.TUNTAPNameTextBox = new System.Windows.Forms.TextBox();
			this.TUNTAPIndexLabel = new System.Windows.Forms.Label();
			this.V2RayLoggingLevelGroupBox.SuspendLayout();
			this.TUNTAPGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// V2RayLoggingLevelGroupBox
			// 
			this.V2RayLoggingLevelGroupBox.Controls.Add(this.V2RayLoggingLevelComboBox);
			this.V2RayLoggingLevelGroupBox.Location = new System.Drawing.Point(12, 12);
			this.V2RayLoggingLevelGroupBox.Name = "V2RayLoggingLevelGroupBox";
			this.V2RayLoggingLevelGroupBox.Size = new System.Drawing.Size(363, 56);
			this.V2RayLoggingLevelGroupBox.TabIndex = 0;
			this.V2RayLoggingLevelGroupBox.TabStop = false;
			this.V2RayLoggingLevelGroupBox.Text = "V2Ray 日志等级";
			// 
			// V2RayLoggingLevelComboBox
			// 
			this.V2RayLoggingLevelComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.V2RayLoggingLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.V2RayLoggingLevelComboBox.FormattingEnabled = true;
			this.V2RayLoggingLevelComboBox.Items.AddRange(new object[] {
            "调试",
            "信息",
            "警告",
            "错误",
            "无"});
			this.V2RayLoggingLevelComboBox.Location = new System.Drawing.Point(6, 22);
			this.V2RayLoggingLevelComboBox.Name = "V2RayLoggingLevelComboBox";
			this.V2RayLoggingLevelComboBox.Size = new System.Drawing.Size(351, 24);
			this.V2RayLoggingLevelComboBox.TabIndex = 0;
			this.V2RayLoggingLevelComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBox_DrawItem);
			// 
			// ControlButton
			// 
			this.ControlButton.Location = new System.Drawing.Point(300, 272);
			this.ControlButton.Name = "ControlButton";
			this.ControlButton.Size = new System.Drawing.Size(75, 23);
			this.ControlButton.TabIndex = 1;
			this.ControlButton.Text = "保存";
			this.ControlButton.UseVisualStyleBackColor = true;
			this.ControlButton.Click += new System.EventHandler(this.ControlButton_Click);
			// 
			// TUNTAPGroupBox
			// 
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPIndexLabel);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPNameTextBox);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPNameLabel);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPDNSTextBox);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPGatewayTextBox);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPNetmaskTextBox);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPNetmaskLabel);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPAddressTextBox);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPAddressLabel);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPUseCustomDNSCheckBox);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPGatewayLabel);
			this.TUNTAPGroupBox.Controls.Add(this.TUNTAPDNSLabel);
			this.TUNTAPGroupBox.Location = new System.Drawing.Point(12, 74);
			this.TUNTAPGroupBox.Name = "TUNTAPGroupBox";
			this.TUNTAPGroupBox.Size = new System.Drawing.Size(363, 192);
			this.TUNTAPGroupBox.TabIndex = 2;
			this.TUNTAPGroupBox.TabStop = false;
			this.TUNTAPGroupBox.Text = "虚拟网卡";
			// 
			// TUNTAPDNSTextBox
			// 
			this.TUNTAPDNSTextBox.Location = new System.Drawing.Point(48, 135);
			this.TUNTAPDNSTextBox.Name = "TUNTAPDNSTextBox";
			this.TUNTAPDNSTextBox.Size = new System.Drawing.Size(309, 23);
			this.TUNTAPDNSTextBox.TabIndex = 6;
			this.TUNTAPDNSTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TUNTAPGatewayTextBox
			// 
			this.TUNTAPGatewayTextBox.Location = new System.Drawing.Point(48, 106);
			this.TUNTAPGatewayTextBox.Name = "TUNTAPGatewayTextBox";
			this.TUNTAPGatewayTextBox.Size = new System.Drawing.Size(309, 23);
			this.TUNTAPGatewayTextBox.TabIndex = 4;
			this.TUNTAPGatewayTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TUNTAPNetmaskTextBox
			// 
			this.TUNTAPNetmaskTextBox.Location = new System.Drawing.Point(48, 77);
			this.TUNTAPNetmaskTextBox.Name = "TUNTAPNetmaskTextBox";
			this.TUNTAPNetmaskTextBox.Size = new System.Drawing.Size(309, 23);
			this.TUNTAPNetmaskTextBox.TabIndex = 3;
			this.TUNTAPNetmaskTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TUNTAPNetmaskLabel
			// 
			this.TUNTAPNetmaskLabel.AutoSize = true;
			this.TUNTAPNetmaskLabel.Location = new System.Drawing.Point(6, 80);
			this.TUNTAPNetmaskLabel.Name = "TUNTAPNetmaskLabel";
			this.TUNTAPNetmaskLabel.Size = new System.Drawing.Size(44, 17);
			this.TUNTAPNetmaskLabel.TabIndex = 2;
			this.TUNTAPNetmaskLabel.Text = "掩码：";
			// 
			// TUNTAPAddressTextBox
			// 
			this.TUNTAPAddressTextBox.Location = new System.Drawing.Point(48, 48);
			this.TUNTAPAddressTextBox.Name = "TUNTAPAddressTextBox";
			this.TUNTAPAddressTextBox.Size = new System.Drawing.Size(309, 23);
			this.TUNTAPAddressTextBox.TabIndex = 1;
			this.TUNTAPAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TUNTAPAddressLabel
			// 
			this.TUNTAPAddressLabel.AutoSize = true;
			this.TUNTAPAddressLabel.Location = new System.Drawing.Point(6, 51);
			this.TUNTAPAddressLabel.Name = "TUNTAPAddressLabel";
			this.TUNTAPAddressLabel.Size = new System.Drawing.Size(44, 17);
			this.TUNTAPAddressLabel.TabIndex = 0;
			this.TUNTAPAddressLabel.Text = "地址：";
			// 
			// TUNTAPUseCustomDNSCheckBox
			// 
			this.TUNTAPUseCustomDNSCheckBox.AutoSize = true;
			this.TUNTAPUseCustomDNSCheckBox.Location = new System.Drawing.Point(200, 164);
			this.TUNTAPUseCustomDNSCheckBox.Name = "TUNTAPUseCustomDNSCheckBox";
			this.TUNTAPUseCustomDNSCheckBox.Size = new System.Drawing.Size(157, 21);
			this.TUNTAPUseCustomDNSCheckBox.TabIndex = 8;
			this.TUNTAPUseCustomDNSCheckBox.Text = "使用自定义的 DNS 设置";
			this.TUNTAPUseCustomDNSCheckBox.UseVisualStyleBackColor = true;
			// 
			// TUNTAPGatewayLabel
			// 
			this.TUNTAPGatewayLabel.AutoSize = true;
			this.TUNTAPGatewayLabel.Location = new System.Drawing.Point(6, 109);
			this.TUNTAPGatewayLabel.Name = "TUNTAPGatewayLabel";
			this.TUNTAPGatewayLabel.Size = new System.Drawing.Size(44, 17);
			this.TUNTAPGatewayLabel.TabIndex = 5;
			this.TUNTAPGatewayLabel.Text = "网关：";
			// 
			// TUNTAPDNSLabel
			// 
			this.TUNTAPDNSLabel.AutoSize = true;
			this.TUNTAPDNSLabel.Location = new System.Drawing.Point(6, 138);
			this.TUNTAPDNSLabel.Name = "TUNTAPDNSLabel";
			this.TUNTAPDNSLabel.Size = new System.Drawing.Size(46, 17);
			this.TUNTAPDNSLabel.TabIndex = 7;
			this.TUNTAPDNSLabel.Text = "DNS：";
			// 
			// GlobalExceptionIPButton
			// 
			this.GlobalExceptionIPButton.Location = new System.Drawing.Point(12, 272);
			this.GlobalExceptionIPButton.Name = "GlobalExceptionIPButton";
			this.GlobalExceptionIPButton.Size = new System.Drawing.Size(80, 23);
			this.GlobalExceptionIPButton.TabIndex = 3;
			this.GlobalExceptionIPButton.Text = "全局例外 IP";
			this.GlobalExceptionIPButton.UseVisualStyleBackColor = true;
			this.GlobalExceptionIPButton.Click += new System.EventHandler(this.GlobalExceptionIPButton_Click);
			// 
			// TUNTAPNameLabel
			// 
			this.TUNTAPNameLabel.AutoSize = true;
			this.TUNTAPNameLabel.Location = new System.Drawing.Point(6, 22);
			this.TUNTAPNameLabel.Name = "TUNTAPNameLabel";
			this.TUNTAPNameLabel.Size = new System.Drawing.Size(44, 17);
			this.TUNTAPNameLabel.TabIndex = 9;
			this.TUNTAPNameLabel.Text = "名称：";
			// 
			// TUNTAPNameTextBox
			// 
			this.TUNTAPNameTextBox.Location = new System.Drawing.Point(48, 19);
			this.TUNTAPNameTextBox.Name = "TUNTAPNameTextBox";
			this.TUNTAPNameTextBox.ReadOnly = true;
			this.TUNTAPNameTextBox.Size = new System.Drawing.Size(309, 23);
			this.TUNTAPNameTextBox.TabIndex = 10;
			this.TUNTAPNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TUNTAPIndexLabel
			// 
			this.TUNTAPIndexLabel.AutoSize = true;
			this.TUNTAPIndexLabel.Location = new System.Drawing.Point(6, 165);
			this.TUNTAPIndexLabel.Name = "TUNTAPIndexLabel";
			this.TUNTAPIndexLabel.Size = new System.Drawing.Size(51, 17);
			this.TUNTAPIndexLabel.TabIndex = 11;
			this.TUNTAPIndexLabel.Text = "索引：0";
			// 
			// AdvancedForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(387, 306);
			this.Controls.Add(this.GlobalExceptionIPButton);
			this.Controls.Add(this.TUNTAPGroupBox);
			this.Controls.Add(this.ControlButton);
			this.Controls.Add(this.V2RayLoggingLevelGroupBox);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "AdvancedForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "高级设置";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdvancedForm_FormClosing);
			this.Load += new System.EventHandler(this.AdvancedForm_Load);
			this.V2RayLoggingLevelGroupBox.ResumeLayout(false);
			this.TUNTAPGroupBox.ResumeLayout(false);
			this.TUNTAPGroupBox.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox V2RayLoggingLevelGroupBox;
        private System.Windows.Forms.ComboBox V2RayLoggingLevelComboBox;
        private System.Windows.Forms.Button ControlButton;
		private System.Windows.Forms.GroupBox TUNTAPGroupBox;
		private System.Windows.Forms.Label TUNTAPAddressLabel;
		private System.Windows.Forms.TextBox TUNTAPGatewayTextBox;
		private System.Windows.Forms.TextBox TUNTAPNetmaskTextBox;
		private System.Windows.Forms.Label TUNTAPNetmaskLabel;
		private System.Windows.Forms.TextBox TUNTAPAddressTextBox;
		private System.Windows.Forms.Label TUNTAPGatewayLabel;
		private System.Windows.Forms.Label TUNTAPDNSLabel;
		private System.Windows.Forms.TextBox TUNTAPDNSTextBox;
		private System.Windows.Forms.CheckBox TUNTAPUseCustomDNSCheckBox;
		private System.Windows.Forms.Button GlobalExceptionIPButton;
		private System.Windows.Forms.Label TUNTAPNameLabel;
		private System.Windows.Forms.TextBox TUNTAPNameTextBox;
		private System.Windows.Forms.Label TUNTAPIndexLabel;
	}
}