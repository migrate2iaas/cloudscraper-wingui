namespace CloudScraper
{
    partial class CloudParametersForm
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
            this.backButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.awsIDtextBox = new System.Windows.Forms.TextBox();
            this.awsKeyLabel = new System.Windows.Forms.Label();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.regionLabel = new System.Windows.Forms.Label();
            this.awsIdLabel = new System.Windows.Forms.Label();
            this.awsKeyTextBox = new System.Windows.Forms.TextBox();
            this.bucketLabel = new System.Windows.Forms.Label();
            this.bucketTextBox = new System.Windows.Forms.TextBox();
            this.cloudServerTypeLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.serverTypeComboBox = new System.Windows.Forms.ComboBox();
            this.availabilityLabel = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.securityGroupLabel = new System.Windows.Forms.Label();
            this.nextButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(13, 227);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "<Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(469, 209);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.awsKeyTextBox);
            this.tabPage1.Controls.Add(this.awsIDtextBox);
            this.tabPage1.Controls.Add(this.awsKeyLabel);
            this.tabPage1.Controls.Add(this.awsIdLabel);
            this.tabPage1.Controls.Add(this.regionComboBox);
            this.tabPage1.Controls.Add(this.regionLabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(461, 183);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.securityGroupLabel);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.availabilityLabel);
            this.tabPage2.Controls.Add(this.serverTypeComboBox);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.cloudServerTypeLabel);
            this.tabPage2.Controls.Add(this.bucketTextBox);
            this.tabPage2.Controls.Add(this.bucketLabel);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(461, 183);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // awsIDtextBox
            // 
            this.awsIDtextBox.Location = new System.Drawing.Point(173, 72);
            this.awsIDtextBox.Name = "awsIDtextBox";
            this.awsIDtextBox.Size = new System.Drawing.Size(220, 20);
            this.awsIDtextBox.TabIndex = 3;
            // 
            // awsKeyLabel
            // 
            this.awsKeyLabel.AutoSize = true;
            this.awsKeyLabel.Location = new System.Drawing.Point(55, 102);
            this.awsKeyLabel.Name = "awsKeyLabel";
            this.awsKeyLabel.Size = new System.Drawing.Size(112, 13);
            this.awsKeyLabel.TabIndex = 6;
            this.awsKeyLabel.Text = "You AWS Secret Key:";
            // 
            // regionComboBox
            // 
            this.regionComboBox.FormattingEnabled = true;
            this.regionComboBox.Location = new System.Drawing.Point(173, 40);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(220, 21);
            this.regionComboBox.TabIndex = 1;
            // 
            // regionLabel
            // 
            this.regionLabel.AutoSize = true;
            this.regionLabel.Location = new System.Drawing.Point(84, 43);
            this.regionLabel.Name = "regionLabel";
            this.regionLabel.Size = new System.Drawing.Size(83, 13);
            this.regionLabel.TabIndex = 2;
            this.regionLabel.Text = "Choose Region:";
            // 
            // awsIdLabel
            // 
            this.awsIdLabel.AutoSize = true;
            this.awsIdLabel.Location = new System.Drawing.Point(98, 75);
            this.awsIdLabel.Name = "awsIdLabel";
            this.awsIdLabel.Size = new System.Drawing.Size(69, 13);
            this.awsIdLabel.TabIndex = 4;
            this.awsIdLabel.Text = "You AWS Id:";
            // 
            // awsKeyTextBox
            // 
            this.awsKeyTextBox.Location = new System.Drawing.Point(174, 102);
            this.awsKeyTextBox.Name = "awsKeyTextBox";
            this.awsKeyTextBox.PasswordChar = '*';
            this.awsKeyTextBox.Size = new System.Drawing.Size(219, 20);
            this.awsKeyTextBox.TabIndex = 7;
            // 
            // bucketLabel
            // 
            this.bucketLabel.AutoSize = true;
            this.bucketLabel.Location = new System.Drawing.Point(44, 22);
            this.bucketLabel.Name = "bucketLabel";
            this.bucketLabel.Size = new System.Drawing.Size(121, 13);
            this.bucketLabel.TabIndex = 0;
            this.bucketLabel.Text = "Specify your S3 Bucket:";
            // 
            // bucketTextBox
            // 
            this.bucketTextBox.Location = new System.Drawing.Point(171, 19);
            this.bucketTextBox.Name = "bucketTextBox";
            this.bucketTextBox.Size = new System.Drawing.Size(235, 20);
            this.bucketTextBox.TabIndex = 1;
            // 
            // cloudServerTypeLabel
            // 
            this.cloudServerTypeLabel.AutoSize = true;
            this.cloudServerTypeLabel.Location = new System.Drawing.Point(14, 59);
            this.cloudServerTypeLabel.Name = "cloudServerTypeLabel";
            this.cloudServerTypeLabel.Size = new System.Drawing.Size(152, 13);
            this.cloudServerTypeLabel.TabIndex = 2;
            this.cloudServerTypeLabel.Text = "Specify your cloud server type:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(171, 92);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(235, 20);
            this.textBox1.TabIndex = 3;
            // 
            // serverTypeComboBox
            // 
            this.serverTypeComboBox.FormattingEnabled = true;
            this.serverTypeComboBox.Location = new System.Drawing.Point(172, 56);
            this.serverTypeComboBox.Name = "serverTypeComboBox";
            this.serverTypeComboBox.Size = new System.Drawing.Size(234, 21);
            this.serverTypeComboBox.TabIndex = 4;
            // 
            // availabilityLabel
            // 
            this.availabilityLabel.AutoSize = true;
            this.availabilityLabel.Location = new System.Drawing.Point(20, 95);
            this.availabilityLabel.Name = "availabilityLabel";
            this.availabilityLabel.Size = new System.Drawing.Size(145, 13);
            this.availabilityLabel.TabIndex = 5;
            this.availabilityLabel.Text = "Specify your availability zone:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(172, 139);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(234, 20);
            this.textBox2.TabIndex = 6;
            // 
            // securityGroupLabel
            // 
            this.securityGroupLabel.AutoSize = true;
            this.securityGroupLabel.Location = new System.Drawing.Point(28, 142);
            this.securityGroupLabel.Name = "securityGroupLabel";
            this.securityGroupLabel.Size = new System.Drawing.Size(137, 13);
            this.securityGroupLabel.TabIndex = 7;
            this.securityGroupLabel.Text = "Specify your security group:";
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(403, 227);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 10;
            this.nextButton.Text = "Next>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // CloudParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 262);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.backButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloudParametersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CloudParametersForm";
            this.TopMost = true;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox awsKeyTextBox;
        private System.Windows.Forms.TextBox awsIDtextBox;
        private System.Windows.Forms.Label awsKeyLabel;
        private System.Windows.Forms.Label awsIdLabel;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.Label regionLabel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label securityGroupLabel;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label availabilityLabel;
        private System.Windows.Forms.ComboBox serverTypeComboBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label cloudServerTypeLabel;
        private System.Windows.Forms.TextBox bucketTextBox;
        private System.Windows.Forms.Label bucketLabel;
        private System.Windows.Forms.Button nextButton;
    }
}