namespace CloudScraper
{
    partial class SaveTransferTaskForm
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
            this.saveTransferTextBox = new System.Windows.Forms.TextBox();
            this.saveTransferTask = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.nextButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveTransferTextBox
            // 
            this.saveTransferTextBox.Location = new System.Drawing.Point(13, 40);
            this.saveTransferTextBox.Name = "saveTransferTextBox";
            this.saveTransferTextBox.Size = new System.Drawing.Size(168, 20);
            this.saveTransferTextBox.TabIndex = 0;
            // 
            // saveTransferTask
            // 
            this.saveTransferTask.Location = new System.Drawing.Point(187, 40);
            this.saveTransferTask.Name = "saveTransferTask";
            this.saveTransferTask.Size = new System.Drawing.Size(75, 23);
            this.saveTransferTask.TabIndex = 1;
            this.saveTransferTask.Text = "Save...";
            this.saveTransferTask.UseVisualStyleBackColor = true;
            this.saveTransferTask.Click += new System.EventHandler(this.saveTransferTask_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(197, 227);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = "Next>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(13, 226);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 3;
            this.backButton.Text = "<Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // SaveTransferTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.saveTransferTask);
            this.Controls.Add(this.saveTransferTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveTransferTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SaveTransferTaskForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox saveTransferTextBox;
        private System.Windows.Forms.Button saveTransferTask;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button backButton;
    }
}