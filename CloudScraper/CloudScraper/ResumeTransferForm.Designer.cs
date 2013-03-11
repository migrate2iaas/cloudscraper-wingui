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
            this.backButton = new System.Windows.Forms.Button();
            this.resumeUploadCheckBox = new System.Windows.Forms.CheckBox();
            this.redeployUploadCheckBox = new System.Windows.Forms.CheckBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(13, 228);
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
            this.resumeUploadCheckBox.Location = new System.Drawing.Point(56, 36);
            this.resumeUploadCheckBox.Name = "resumeUploadCheckBox";
            this.resumeUploadCheckBox.Size = new System.Drawing.Size(294, 17);
            this.resumeUploadCheckBox.TabIndex = 1;
            this.resumeUploadCheckBox.Text = "Resume Upload (check if your images is already created)";
            this.resumeUploadCheckBox.UseVisualStyleBackColor = true;
            // 
            // redeployUploadCheckBox
            // 
            this.redeployUploadCheckBox.AutoSize = true;
            this.redeployUploadCheckBox.Location = new System.Drawing.Point(56, 60);
            this.redeployUploadCheckBox.Name = "redeployUploadCheckBox";
            this.redeployUploadCheckBox.Size = new System.Drawing.Size(413, 17);
            this.redeployUploadCheckBox.TabIndex = 2;
            this.redeployUploadCheckBox.Text = "Redeploy Uploaded Image (creates cloud server from the image already uploaded)";
            this.redeployUploadCheckBox.UseVisualStyleBackColor = true;
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(498, 228);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // ResumeTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 263);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.redeployUploadCheckBox);
            this.Controls.Add(this.resumeUploadCheckBox);
            this.Controls.Add(this.backButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ResumeTransferForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResumeTransferForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.CheckBox resumeUploadCheckBox;
        private System.Windows.Forms.CheckBox redeployUploadCheckBox;
        private System.Windows.Forms.Button nextButton;
    }
}