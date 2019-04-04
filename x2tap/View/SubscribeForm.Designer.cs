namespace x2tap.View
{
    partial class SubscribeForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubscribeForm));
			this.SubscribeLinksGroupBox = new System.Windows.Forms.GroupBox();
			this.SubscribeLinksListBox = new System.Windows.Forms.ListBox();
			this.AddSubscribeLinkButton = new System.Windows.Forms.Button();
			this.SubscribeLinksButton = new System.Windows.Forms.Button();
			this.SubscribeTextGroupBox = new System.Windows.Forms.GroupBox();
			this.SubscribeTextButton = new System.Windows.Forms.Button();
			this.SubscribeTextTextBox = new System.Windows.Forms.TextBox();
			this.DeleteSubscribeLinkButton = new System.Windows.Forms.Button();
			this.SubscribeLinksGroupBox.SuspendLayout();
			this.SubscribeTextGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// SubscribeLinksGroupBox
			// 
			this.SubscribeLinksGroupBox.Controls.Add(this.DeleteSubscribeLinkButton);
			this.SubscribeLinksGroupBox.Controls.Add(this.SubscribeLinksListBox);
			this.SubscribeLinksGroupBox.Controls.Add(this.AddSubscribeLinkButton);
			this.SubscribeLinksGroupBox.Controls.Add(this.SubscribeLinksButton);
			this.SubscribeLinksGroupBox.Location = new System.Drawing.Point(12, 12);
			this.SubscribeLinksGroupBox.Name = "SubscribeLinksGroupBox";
			this.SubscribeLinksGroupBox.Size = new System.Drawing.Size(661, 196);
			this.SubscribeLinksGroupBox.TabIndex = 0;
			this.SubscribeLinksGroupBox.TabStop = false;
			this.SubscribeLinksGroupBox.Text = "从订阅链接";
			// 
			// SubscribeLinksListBox
			// 
			this.SubscribeLinksListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.SubscribeLinksListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.SubscribeLinksListBox.FormattingEnabled = true;
			this.SubscribeLinksListBox.ItemHeight = 17;
			this.SubscribeLinksListBox.Location = new System.Drawing.Point(6, 22);
			this.SubscribeLinksListBox.Name = "SubscribeLinksListBox";
			this.SubscribeLinksListBox.Size = new System.Drawing.Size(649, 138);
			this.SubscribeLinksListBox.TabIndex = 3;
			this.SubscribeLinksListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ListBox_DrawItem);
			// 
			// AddSubscribeLinkButton
			// 
			this.AddSubscribeLinkButton.Location = new System.Drawing.Point(392, 168);
			this.AddSubscribeLinkButton.Name = "AddSubscribeLinkButton";
			this.AddSubscribeLinkButton.Size = new System.Drawing.Size(88, 23);
			this.AddSubscribeLinkButton.TabIndex = 2;
			this.AddSubscribeLinkButton.Text = "添加订阅链接";
			this.AddSubscribeLinkButton.UseVisualStyleBackColor = true;
			this.AddSubscribeLinkButton.Click += new System.EventHandler(this.AddSubscribeLinkButton_Click);
			// 
			// SubscribeLinksButton
			// 
			this.SubscribeLinksButton.Location = new System.Drawing.Point(580, 168);
			this.SubscribeLinksButton.Name = "SubscribeLinksButton";
			this.SubscribeLinksButton.Size = new System.Drawing.Size(75, 23);
			this.SubscribeLinksButton.TabIndex = 1;
			this.SubscribeLinksButton.Text = "更新订阅";
			this.SubscribeLinksButton.UseVisualStyleBackColor = true;
			this.SubscribeLinksButton.Click += new System.EventHandler(this.SubscribeLinksButton_Click);
			// 
			// SubscribeTextGroupBox
			// 
			this.SubscribeTextGroupBox.Controls.Add(this.SubscribeTextButton);
			this.SubscribeTextGroupBox.Controls.Add(this.SubscribeTextTextBox);
			this.SubscribeTextGroupBox.Location = new System.Drawing.Point(12, 214);
			this.SubscribeTextGroupBox.Name = "SubscribeTextGroupBox";
			this.SubscribeTextGroupBox.Size = new System.Drawing.Size(661, 277);
			this.SubscribeTextGroupBox.TabIndex = 1;
			this.SubscribeTextGroupBox.TabStop = false;
			this.SubscribeTextGroupBox.Text = "从文本";
			// 
			// SubscribeTextButton
			// 
			this.SubscribeTextButton.Location = new System.Drawing.Point(580, 248);
			this.SubscribeTextButton.Name = "SubscribeTextButton";
			this.SubscribeTextButton.Size = new System.Drawing.Size(75, 23);
			this.SubscribeTextButton.TabIndex = 1;
			this.SubscribeTextButton.Text = "订阅";
			this.SubscribeTextButton.UseVisualStyleBackColor = true;
			this.SubscribeTextButton.Click += new System.EventHandler(this.SubscribeTextButton_Click);
			// 
			// SubscribeTextTextBox
			// 
			this.SubscribeTextTextBox.Location = new System.Drawing.Point(6, 22);
			this.SubscribeTextTextBox.Multiline = true;
			this.SubscribeTextTextBox.Name = "SubscribeTextTextBox";
			this.SubscribeTextTextBox.Size = new System.Drawing.Size(649, 220);
			this.SubscribeTextTextBox.TabIndex = 0;
			// 
			// DeleteSubscribeLinkButton
			// 
			this.DeleteSubscribeLinkButton.Location = new System.Drawing.Point(486, 168);
			this.DeleteSubscribeLinkButton.Name = "DeleteSubscribeLinkButton";
			this.DeleteSubscribeLinkButton.Size = new System.Drawing.Size(88, 23);
			this.DeleteSubscribeLinkButton.TabIndex = 4;
			this.DeleteSubscribeLinkButton.Text = "删除订阅链接";
			this.DeleteSubscribeLinkButton.UseVisualStyleBackColor = true;
			this.DeleteSubscribeLinkButton.Click += new System.EventHandler(this.DeleteSubscribeLinkButton_Click);
			// 
			// SubscribeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(685, 501);
			this.Controls.Add(this.SubscribeTextGroupBox);
			this.Controls.Add(this.SubscribeLinksGroupBox);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "SubscribeForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "订阅";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SubscribeForm_FormClosing);
			this.Load += new System.EventHandler(this.SubscribeForm_Load);
			this.SubscribeLinksGroupBox.ResumeLayout(false);
			this.SubscribeTextGroupBox.ResumeLayout(false);
			this.SubscribeTextGroupBox.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SubscribeLinksGroupBox;
        private System.Windows.Forms.Button SubscribeLinksButton;
        private System.Windows.Forms.GroupBox SubscribeTextGroupBox;
        private System.Windows.Forms.Button SubscribeTextButton;
        private System.Windows.Forms.TextBox SubscribeTextTextBox;
		private System.Windows.Forms.Button AddSubscribeLinkButton;
		private System.Windows.Forms.ListBox SubscribeLinksListBox;
		private System.Windows.Forms.Button DeleteSubscribeLinkButton;
	}
}