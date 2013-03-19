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
            this.label1 = new System.Windows.Forms.Label();
            this.helpButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.awsIdTextBox = new System.Windows.Forms.TextBox();
            this.awsIdLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(236, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Load your previously saved transfer task";
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
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-4, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(137, 299);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            // 
            // awsIdTextBox
            // 
            this.awsIdTextBox.Location = new System.Drawing.Point(258, 212);
            this.awsIdTextBox.MaxLength = 20;
            this.awsIdTextBox.Name = "awsIdTextBox";
            this.awsIdTextBox.Size = new System.Drawing.Size(220, 20);
            this.awsIdTextBox.TabIndex = 19;
            this.awsIdTextBox.TextChanged += new System.EventHandler(this.AwsIdChanged);
            // 
            // awsIdLabel
            // 
            this.awsIdLabel.AutoSize = true;
            this.awsIdLabel.Location = new System.Drawing.Point(182, 215);
            this.awsIdLabel.Name = "awsIdLabel";
            this.awsIdLabel.Size = new System.Drawing.Size(69, 13);
            this.awsIdLabel.TabIndex = 20;
            this.awsIdLabel.Text = "You AWS Id:";
            // 
            // ResumeTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.awsIdTextBox);
            this.Controls.Add(this.awsIdLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.label1);
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
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Resume your transfer task";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox awsIdTextBox;
        private System.Windows.Forms.Label awsIdLabel;
    }
}