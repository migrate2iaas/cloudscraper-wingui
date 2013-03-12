namespace CloudScraper
{
    partial class ImagesPathForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagesPathForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.browseButton = new System.Windows.Forms.Button();
            this.browseTextBox = new System.Windows.Forms.TextBox();
            this.backButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.freeSpaceLabel = new System.Windows.Forms.Label();
            this.freeSpace = new System.Windows.Forms.Label();
            this.totalSpaceLabel = new System.Windows.Forms.Label();
            this.totalSpace = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(370, 60);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // browseTextBox
            // 
            this.browseTextBox.Location = new System.Drawing.Point(12, 62);
            this.browseTextBox.Name = "browseTextBox";
            this.browseTextBox.Size = new System.Drawing.Size(352, 20);
            this.browseTextBox.TabIndex = 1;
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(12, 144);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "<Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(370, 144);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // freeSpaceLabel
            // 
            this.freeSpaceLabel.AutoSize = true;
            this.freeSpaceLabel.Location = new System.Drawing.Point(13, 119);
            this.freeSpaceLabel.Name = "freeSpaceLabel";
            this.freeSpaceLabel.Size = new System.Drawing.Size(62, 13);
            this.freeSpaceLabel.TabIndex = 4;
            this.freeSpaceLabel.Text = "FreeSpace:";
            // 
            // freeSpace
            // 
            this.freeSpace.AutoSize = true;
            this.freeSpace.Location = new System.Drawing.Point(75, 119);
            this.freeSpace.Name = "freeSpace";
            this.freeSpace.Size = new System.Drawing.Size(28, 13);
            this.freeSpace.TabIndex = 5;
            this.freeSpace.Text = "0GB";
            // 
            // totalSpaceLabel
            // 
            this.totalSpaceLabel.AutoSize = true;
            this.totalSpaceLabel.Location = new System.Drawing.Point(13, 95);
            this.totalSpaceLabel.Name = "totalSpaceLabel";
            this.totalSpaceLabel.Size = new System.Drawing.Size(114, 13);
            this.totalSpaceLabel.TabIndex = 6;
            this.totalSpaceLabel.Text = "Total Space Required:";
            // 
            // totalSpace
            // 
            this.totalSpace.AutoSize = true;
            this.totalSpace.Location = new System.Drawing.Point(124, 95);
            this.totalSpace.Name = "totalSpace";
            this.totalSpace.Size = new System.Drawing.Size(28, 13);
            this.totalSpace.TabIndex = 7;
            this.totalSpace.Text = "0GB";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Please choose folder where to store your server disk images:";
            // 
            // ImagesPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 180);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.totalSpace);
            this.Controls.Add(this.totalSpaceLabel);
            this.Controls.Add(this.freeSpace);
            this.Controls.Add(this.freeSpaceLabel);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.browseTextBox);
            this.Controls.Add(this.browseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImagesPathForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Folder select";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.On_closed);
            this.Load += new System.EventHandler(this.ImagesPathForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox browseTextBox;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label freeSpaceLabel;
        private System.Windows.Forms.Label freeSpace;
        private System.Windows.Forms.Label totalSpaceLabel;
        private System.Windows.Forms.Label totalSpace;
        private System.Windows.Forms.Label label1;
    }
}