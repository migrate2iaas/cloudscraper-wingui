namespace CloudScraper
{
    partial class ResumeTransferForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResumeTransferForm));
            this.backButton = new System.Windows.Forms.Button();
            this.resumeUploadCheckBox = new System.Windows.Forms.CheckBox();
            this.redeployUploadCheckBox = new System.Windows.Forms.CheckBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.resumeTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.mainLabel = new System.Windows.Forms.Label();
            this.helpButton = new System.Windows.Forms.Button();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.awsIdTextBox = new System.Windows.Forms.TextBox();
            this.awsIdLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(370, 263);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // resumeUploadCheckBox
            // 
            this.resumeUploadCheckBox.AutoSize = true;
            this.resumeUploadCheckBox.Location = new System.Drawing.Point(139, 154);
            this.resumeUploadCheckBox.Name = "resumeUploadCheckBox";
            this.resumeUploadCheckBox.Size = new System.Drawing.Size(294, 17);
            this.resumeUploadCheckBox.TabIndex = 1;
            this.resumeUploadCheckBox.Text = "Resume Upload (check if your images is already created)";
            this.resumeUploadCheckBox.UseVisualStyleBackColor = true;
            this.resumeUploadCheckBox.CheckedChanged += new System.EventHandler(this.ResumeUploadCheckedChanged);
            // 
            // redeployUploadCheckBox
            // 
            this.redeployUploadCheckBox.AutoSize = true;
            this.redeployUploadCheckBox.Location = new System.Drawing.Point(139, 177);
            this.redeployUploadCheckBox.Name = "redeployUploadCheckBox";
            this.redeployUploadCheckBox.Size = new System.Drawing.Size(413, 17);
            this.redeployUploadCheckBox.TabIndex = 2;
            this.redeployUploadCheckBox.Text = "Redeploy Uploaded Image (creates cloud server from the image already uploaded)";
            this.redeployUploadCheckBox.UseVisualStyleBackColor = true;
            this.redeployUploadCheckBox.CheckedChanged += new System.EventHandler(this.RedeployUploadCheckedChanged);
            // 
            // nextButton
            // 
            this.nextButton.Enabled = false;
            this.nextButton.Location = new System.Drawing.Point(480, 263);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButtonClick);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(484, 100);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButtonClick);
            // 
            // resumeTextBox
            // 
            this.resumeTextBox.Location = new System.Drawing.Point(139, 102);
            this.resumeTextBox.Name = "resumeTextBox";
            this.resumeTextBox.Size = new System.Drawing.Size(339, 20);
            this.resumeTextBox.TabIndex = 5;
            this.resumeTextBox.TextChanged += new System.EventHandler(this.OnPathChanged);
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Location = new System.Drawing.Point(236, 63);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(197, 13);
            this.mainLabel.TabIndex = 6;
            this.mainLabel.Text = "Load your previously saved transfer task";
            // 
            // helpButton
            // 
            this.helpButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.helpButton.FlatAppearance.BorderSize = 0;
            this.helpButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpButton.ForeColor = System.Drawing.Color.Transparent;
            this.helpButton.Location = new System.Drawing.Point(545, 1);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(23, 22);
            this.helpButton.TabIndex = 13;
            this.helpButton.Tag = "Help";
            this.helpButton.UseVisualStyleBackColor = false;
            this.helpButton.Click += new System.EventHandler(this.HelpButtonClick);
            // 
            // logoPicture
            // 
            this.logoPicture.ErrorImage = null;
            this.logoPicture.Location = new System.Drawing.Point(-4, 1);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(137, 299);
            this.logoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPicture.TabIndex = 18;
            this.logoPicture.TabStop = false;
            // 
            // awsIdTextBox
            // 
            this.awsIdTextBox.Location = new System.Drawing.Point(239, 213);
            this.awsIdTextBox.MaxLength = 40;
            this.awsIdTextBox.Name = "awsIdTextBox";
            this.awsIdTextBox.PasswordChar = '*';
            this.awsIdTextBox.Size = new System.Drawing.Size(296, 20);
            this.awsIdTextBox.TabIndex = 19;
            this.awsIdTextBox.TextChanged += new System.EventHandler(this.AwsIdChanged);
            // 
            // awsIdLabel
            // 
            this.awsIdLabel.AutoSize = true;
            this.awsIdLabel.Location = new System.Drawing.Point(155, 216);
            this.awsIdLabel.Name = "awsIdLabel";
            this.awsIdLabel.Size = new System.Drawing.Size(78, 13);
            this.awsIdLabel.TabIndex = 20;
            this.awsIdLabel.Text = "You AWS Key:";
            // 
            // ResumeTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.awsIdTextBox);
            this.Controls.Add(this.awsIdLabel);
            this.Controls.Add(this.logoPicture);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.resumeTextBox);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.redeployUploadCheckBox);
            this.Controls.Add(this.resumeUploadCheckBox);
            this.Controls.Add(this.backButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResumeTransferForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resume your transfer task";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.ResumeTransferLoad);
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.CheckBox resumeUploadCheckBox;
        private System.Windows.Forms.CheckBox redeployUploadCheckBox;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox resumeTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.PictureBox logoPicture;
        private System.Windows.Forms.TextBox awsIdTextBox;
        private System.Windows.Forms.Label awsIdLabel;
    }
}