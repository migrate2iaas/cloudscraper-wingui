namespace CloudScraper
{
    partial class MailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailForm));
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.mailTextBox = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.commentsTextBox = new System.Windows.Forms.TextBox();
            this.commentsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userTextBox
            // 
            this.userTextBox.Location = new System.Drawing.Point(81, 24);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(168, 20);
            this.userTextBox.TabIndex = 0;
            this.userTextBox.TextChanged += new System.EventHandler(this.UserChanged);
            // 
            // mailTextBox
            // 
            this.mailTextBox.Location = new System.Drawing.Point(81, 59);
            this.mailTextBox.Name = "mailTextBox";
            this.mailTextBox.Size = new System.Drawing.Size(168, 20);
            this.mailTextBox.TabIndex = 1;
            this.mailTextBox.TextChanged += new System.EventHandler(this.MailTextChanged);
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(12, 27);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(63, 13);
            this.userNameLabel.TabIndex = 2;
            this.userNameLabel.Text = "User Name:";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(38, 62);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(38, 13);
            this.emailLabel.TabIndex = 3;
            this.emailLabel.Text = "E-mail:";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(174, 182);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(12, 182);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // commentsTextBox
            // 
            this.commentsTextBox.Location = new System.Drawing.Point(12, 112);
            this.commentsTextBox.Multiline = true;
            this.commentsTextBox.Name = "commentsTextBox";
            this.commentsTextBox.Size = new System.Drawing.Size(237, 64);
            this.commentsTextBox.TabIndex = 6;
            this.commentsTextBox.TextChanged += new System.EventHandler(this.CommentsChanged);
            // 
            // commentsLabel
            // 
            this.commentsLabel.AutoSize = true;
            this.commentsLabel.Location = new System.Drawing.Point(17, 96);
            this.commentsLabel.Name = "commentsLabel";
            this.commentsLabel.Size = new System.Drawing.Size(59, 13);
            this.commentsLabel.TabIndex = 7;
            this.commentsLabel.Text = "Comments:";
            // 
            // MailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 220);
            this.Controls.Add(this.commentsLabel);
            this.Controls.Add(this.commentsTextBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.userNameLabel);
            this.Controls.Add(this.mailTextBox);
            this.Controls.Add(this.userTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MailForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "E-mail Report";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.TextBox mailTextBox;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox commentsTextBox;
        private System.Windows.Forms.Label commentsLabel;
    }
}