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
            this.resumeLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(12, 172);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "<Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // resumeUploadCheckBox
            // 
            this.resumeUploadCheckBox.AutoSize = true;
            this.resumeUploadCheckBox.Location = new System.Drawing.Point(24, 113);
            this.resumeUploadCheckBox.Name = "resumeUploadCheckBox";
            this.resumeUploadCheckBox.Size = new System.Drawing.Size(294, 17);
            this.resumeUploadCheckBox.TabIndex = 1;
            this.resumeUploadCheckBox.Text = "Resume Upload (check if your images is already created)";
            this.resumeUploadCheckBox.UseVisualStyleBackColor = true;
            // 
            // redeployUploadCheckBox
            // 
            this.redeployUploadCheckBox.AutoSize = true;
            this.redeployUploadCheckBox.Location = new System.Drawing.Point(24, 136);
            this.redeployUploadCheckBox.Name = "redeployUploadCheckBox";
            this.redeployUploadCheckBox.Size = new System.Drawing.Size(413, 17);
            this.redeployUploadCheckBox.TabIndex = 2;
            this.redeployUploadCheckBox.Text = "Redeploy Uploaded Image (creates cloud server from the image already uploaded)";
            this.redeployUploadCheckBox.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            this.nextButton.Enabled = false;
            this.nextButton.Location = new System.Drawing.Point(369, 172);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(369, 58);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // resumeTextBox
            // 
            this.resumeTextBox.Location = new System.Drawing.Point(24, 61);
            this.resumeTextBox.Name = "resumeTextBox";
            this.resumeTextBox.Size = new System.Drawing.Size(339, 20);
            this.resumeTextBox.TabIndex = 5;
            this.resumeTextBox.TextChanged += new System.EventHandler(this.OnPathChanged);
            // 
            // resumeLabel
            // 
            this.resumeLabel.AutoSize = true;
            this.resumeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resumeLabel.Location = new System.Drawing.Point(85, 21);
            this.resumeLabel.Name = "resumeLabel";
            this.resumeLabel.Size = new System.Drawing.Size(265, 17);
            this.resumeLabel.TabIndex = 6;
            this.resumeLabel.Text = "Load your previously saved transfer task";
            // 
            // ResumeTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 207);
            this.Controls.Add(this.resumeLabel);
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
            this.Text = "Resume your transfer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.On_closed);
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
        private System.Windows.Forms.Label resumeLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}