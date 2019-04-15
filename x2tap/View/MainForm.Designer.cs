namespace x2tap.View
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.v2rayConfigurationGroupBox = new System.Windows.Forms.GroupBox();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.EditButton = new System.Windows.Forms.Button();
			this.SubscribeButton = new System.Windows.Forms.Button();
			this.ModeComboBox = new System.Windows.Forms.ComboBox();
			this.ModeLabel = new System.Windows.Forms.Label();
			this.ProxyComboBox = new System.Windows.Forms.ComboBox();
			this.ProxyLabel = new System.Windows.Forms.Label();
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.UplinkSpeedLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.DownlinkSpeedLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.UsedBandwidthLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.AdvancedButton = new System.Windows.Forms.Button();
			this.ControlButton = new System.Windows.Forms.Button();
			this.MenuStrip = new System.Windows.Forms.MenuStrip();
			this.ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AddV2RayServerButton = new System.Windows.Forms.ToolStripMenuItem();
			this.AddShadowsocksServerButton = new System.Windows.Forms.ToolStripMenuItem();
			this.AddShadowsocksRServerButton = new System.Windows.Forms.ToolStripMenuItem();
			this.AddSocks5ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TelegramGroupButton = new System.Windows.Forms.ToolStripMenuItem();
			this.TelegramChannelButton = new System.Windows.Forms.ToolStripMenuItem();
			this.GithubButton = new System.Windows.Forms.ToolStripMenuItem();
			this.v2rayConfigurationGroupBox.SuspendLayout();
			this.StatusStrip.SuspendLayout();
			this.MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// v2rayConfigurationGroupBox
			// 
			this.v2rayConfigurationGroupBox.Controls.Add(this.DeleteButton);
			this.v2rayConfigurationGroupBox.Controls.Add(this.EditButton);
			this.v2rayConfigurationGroupBox.Controls.Add(this.SubscribeButton);
			this.v2rayConfigurationGroupBox.Controls.Add(this.ModeComboBox);
			this.v2rayConfigurationGroupBox.Controls.Add(this.ModeLabel);
			this.v2rayConfigurationGroupBox.Controls.Add(this.ProxyComboBox);
			this.v2rayConfigurationGroupBox.Controls.Add(this.ProxyLabel);
			this.v2rayConfigurationGroupBox.Location = new System.Drawing.Point(12, 28);
			this.v2rayConfigurationGroupBox.Name = "v2rayConfigurationGroupBox";
			this.v2rayConfigurationGroupBox.Size = new System.Drawing.Size(687, 108);
			this.v2rayConfigurationGroupBox.TabIndex = 0;
			this.v2rayConfigurationGroupBox.TabStop = false;
			this.v2rayConfigurationGroupBox.Text = "配置信息";
			// 
			// DeleteButton
			// 
			this.DeleteButton.Location = new System.Drawing.Point(444, 78);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(75, 23);
			this.DeleteButton.TabIndex = 6;
			this.DeleteButton.Text = "删除";
			this.DeleteButton.UseVisualStyleBackColor = true;
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// EditButton
			// 
			this.EditButton.Location = new System.Drawing.Point(525, 78);
			this.EditButton.Name = "EditButton";
			this.EditButton.Size = new System.Drawing.Size(75, 23);
			this.EditButton.TabIndex = 5;
			this.EditButton.Text = "编辑";
			this.EditButton.UseVisualStyleBackColor = true;
			this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
			// 
			// SubscribeButton
			// 
			this.SubscribeButton.Location = new System.Drawing.Point(606, 78);
			this.SubscribeButton.Name = "SubscribeButton";
			this.SubscribeButton.Size = new System.Drawing.Size(75, 23);
			this.SubscribeButton.TabIndex = 4;
			this.SubscribeButton.Text = "订阅";
			this.SubscribeButton.UseVisualStyleBackColor = true;
			this.SubscribeButton.Click += new System.EventHandler(this.SubscribeButton_Click);
			// 
			// ModeComboBox
			// 
			this.ModeComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.ModeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ModeComboBox.FormattingEnabled = true;
			this.ModeComboBox.Items.AddRange(new object[] {
            "[内置规则] 绕过局域网和中国",
            "[内置规则] 绕过局域网"});
			this.ModeComboBox.Location = new System.Drawing.Point(44, 48);
			this.ModeComboBox.Name = "ModeComboBox";
			this.ModeComboBox.Size = new System.Drawing.Size(637, 24);
			this.ModeComboBox.TabIndex = 3;
			this.ModeComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBox_DrawItem);
			// 
			// ModeLabel
			// 
			this.ModeLabel.AutoSize = true;
			this.ModeLabel.Location = new System.Drawing.Point(8, 52);
			this.ModeLabel.Name = "ModeLabel";
			this.ModeLabel.Size = new System.Drawing.Size(44, 17);
			this.ModeLabel.TabIndex = 2;
			this.ModeLabel.Text = "模式：";
			// 
			// ProxyComboBox
			// 
			this.ProxyComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.ProxyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ProxyComboBox.FormattingEnabled = true;
			this.ProxyComboBox.Location = new System.Drawing.Point(44, 18);
			this.ProxyComboBox.Name = "ProxyComboBox";
			this.ProxyComboBox.Size = new System.Drawing.Size(637, 24);
			this.ProxyComboBox.TabIndex = 1;
			this.ProxyComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ComboBox_DrawItem);
			// 
			// ProxyLabel
			// 
			this.ProxyLabel.AutoSize = true;
			this.ProxyLabel.Location = new System.Drawing.Point(8, 22);
			this.ProxyLabel.Name = "ProxyLabel";
			this.ProxyLabel.Size = new System.Drawing.Size(44, 17);
			this.ProxyLabel.TabIndex = 0;
			this.ProxyLabel.Text = "代理：";
			// 
			// StatusStrip
			// 
			this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UplinkSpeedLabel,
            this.DownlinkSpeedLabel,
            this.UsedBandwidthLabel,
            this.StatusLabel});
			this.StatusStrip.Location = new System.Drawing.Point(0, 174);
			this.StatusStrip.Name = "StatusStrip";
			this.StatusStrip.Size = new System.Drawing.Size(711, 22);
			this.StatusStrip.SizingGrip = false;
			this.StatusStrip.TabIndex = 1;
			// 
			// UplinkSpeedLabel
			// 
			this.UplinkSpeedLabel.Name = "UplinkSpeedLabel";
			this.UplinkSpeedLabel.Size = new System.Drawing.Size(64, 17);
			this.UplinkSpeedLabel.Text = "↑：0 KB/s";
			// 
			// DownlinkSpeedLabel
			// 
			this.DownlinkSpeedLabel.Name = "DownlinkSpeedLabel";
			this.DownlinkSpeedLabel.Size = new System.Drawing.Size(64, 17);
			this.DownlinkSpeedLabel.Text = "↓：0 KB/s";
			// 
			// UsedBandwidthLabel
			// 
			this.UsedBandwidthLabel.Name = "UsedBandwidthLabel";
			this.UsedBandwidthLabel.Size = new System.Drawing.Size(83, 17);
			this.UsedBandwidthLabel.Text = "已使用：0 KB";
			// 
			// StatusLabel
			// 
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(116, 17);
			this.StatusLabel.Text = "状态：请下达命令！";
			// 
			// AdvancedButton
			// 
			this.AdvancedButton.Location = new System.Drawing.Point(12, 142);
			this.AdvancedButton.Name = "AdvancedButton";
			this.AdvancedButton.Size = new System.Drawing.Size(75, 23);
			this.AdvancedButton.TabIndex = 2;
			this.AdvancedButton.Text = "高级设置";
			this.AdvancedButton.UseVisualStyleBackColor = true;
			this.AdvancedButton.Click += new System.EventHandler(this.AdvancedButton_Click);
			// 
			// ControlButton
			// 
			this.ControlButton.Location = new System.Drawing.Point(624, 142);
			this.ControlButton.Name = "ControlButton";
			this.ControlButton.Size = new System.Drawing.Size(75, 23);
			this.ControlButton.TabIndex = 3;
			this.ControlButton.Text = "启动";
			this.ControlButton.UseVisualStyleBackColor = true;
			this.ControlButton.Click += new System.EventHandler(this.ControlButton_Click);
			// 
			// MenuStrip
			// 
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServerToolStripMenuItem,
            this.AboutToolStripMenuItem});
			this.MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.Size = new System.Drawing.Size(711, 25);
			this.MenuStrip.TabIndex = 5;
			this.MenuStrip.Text = "menuStrip1";
			// 
			// ServerToolStripMenuItem
			// 
			this.ServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddV2RayServerButton,
            this.AddShadowsocksServerButton,
            this.AddShadowsocksRServerButton,
            this.AddSocks5ServerToolStripMenuItem});
			this.ServerToolStripMenuItem.Name = "ServerToolStripMenuItem";
			this.ServerToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
			this.ServerToolStripMenuItem.Text = "服务器";
			// 
			// AddV2RayServerButton
			// 
			this.AddV2RayServerButton.Name = "AddV2RayServerButton";
			this.AddV2RayServerButton.Size = new System.Drawing.Size(193, 22);
			this.AddV2RayServerButton.Text = "添加 [V2Ray] 服务器";
			this.AddV2RayServerButton.Click += new System.EventHandler(this.AddV2RayServerButton_Click);
			// 
			// AddShadowsocksServerButton
			// 
			this.AddShadowsocksServerButton.Name = "AddShadowsocksServerButton";
			this.AddShadowsocksServerButton.Size = new System.Drawing.Size(193, 22);
			this.AddShadowsocksServerButton.Text = "添加 [SS] 服务器";
			this.AddShadowsocksServerButton.Click += new System.EventHandler(this.AddShadowsocksServerButton_Click);
			// 
			// AddShadowsocksRServerButton
			// 
			this.AddShadowsocksRServerButton.Name = "AddShadowsocksRServerButton";
			this.AddShadowsocksRServerButton.Size = new System.Drawing.Size(193, 22);
			this.AddShadowsocksRServerButton.Text = "添加 [SSR] 服务器";
			this.AddShadowsocksRServerButton.Click += new System.EventHandler(this.AddShadowsocksRServerButton_Click);
			// 
			// AddSocks5ServerToolStripMenuItem
			// 
			this.AddSocks5ServerToolStripMenuItem.Name = "AddSocks5ServerToolStripMenuItem";
			this.AddSocks5ServerToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			this.AddSocks5ServerToolStripMenuItem.Text = "添加 [Socks5] 服务器";
			this.AddSocks5ServerToolStripMenuItem.Click += new System.EventHandler(this.AddSocks5ServerToolStripMenuItem_Click);
			// 
			// AboutToolStripMenuItem
			// 
			this.AboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TelegramGroupButton,
            this.TelegramChannelButton,
            this.GithubButton});
			this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
			this.AboutToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
			this.AboutToolStripMenuItem.Text = "关于";
			// 
			// TelegramGroupButton
			// 
			this.TelegramGroupButton.Name = "TelegramGroupButton";
			this.TelegramGroupButton.Size = new System.Drawing.Size(180, 22);
			this.TelegramGroupButton.Text = "Telegram 群组";
			this.TelegramGroupButton.Click += new System.EventHandler(this.TelegramGroupButton_Click);
			// 
			// TelegramChannelButton
			// 
			this.TelegramChannelButton.Name = "TelegramChannelButton";
			this.TelegramChannelButton.Size = new System.Drawing.Size(180, 22);
			this.TelegramChannelButton.Text = "Telegram 频道";
			this.TelegramChannelButton.Click += new System.EventHandler(this.TelegramChannelButton_Click);
			// 
			// GithubButton
			// 
			this.GithubButton.Name = "GithubButton";
			this.GithubButton.Size = new System.Drawing.Size(180, 22);
			this.GithubButton.Text = "Github";
			this.GithubButton.Click += new System.EventHandler(this.GithubButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(711, 196);
			this.Controls.Add(this.ControlButton);
			this.Controls.Add(this.AdvancedButton);
			this.Controls.Add(this.StatusStrip);
			this.Controls.Add(this.MenuStrip);
			this.Controls.Add(this.v2rayConfigurationGroupBox);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.MenuStrip;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "x2tap";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.v2rayConfigurationGroupBox.ResumeLayout(false);
			this.v2rayConfigurationGroupBox.PerformLayout();
			this.StatusStrip.ResumeLayout(false);
			this.StatusStrip.PerformLayout();
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox v2rayConfigurationGroupBox;
        private System.Windows.Forms.ComboBox ProxyComboBox;
        private System.Windows.Forms.Label ProxyLabel;
        private System.Windows.Forms.ComboBox ModeComboBox;
        private System.Windows.Forms.Label ModeLabel;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button SubscribeButton;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel UplinkSpeedLabel;
        private System.Windows.Forms.Button AdvancedButton;
        private System.Windows.Forms.Button ControlButton;
        private System.Windows.Forms.ToolStripStatusLabel DownlinkSpeedLabel;
        private System.Windows.Forms.ToolStripStatusLabel UsedBandwidthLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.MenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem ServerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AddV2RayServerButton;
		private System.Windows.Forms.ToolStripMenuItem AddShadowsocksServerButton;
		private System.Windows.Forms.ToolStripMenuItem AddShadowsocksRServerButton;
		private System.Windows.Forms.ToolStripMenuItem AddSocks5ServerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem TelegramGroupButton;
		private System.Windows.Forms.ToolStripMenuItem TelegramChannelButton;
		private System.Windows.Forms.ToolStripMenuItem GithubButton;
	}
}