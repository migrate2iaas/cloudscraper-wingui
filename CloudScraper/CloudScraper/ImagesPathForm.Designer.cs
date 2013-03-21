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
            this.mainLabel = new System.Windows.Forms.Label();
            this.helpButton = new System.Windows.Forms.Button();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.errorPicture = new System.Windows.Forms.PictureBox();
            this.errorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(481, 122);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseButtonClick);
            // 
            // browseTextBox
            // 
            this.browseTextBox.Location = new System.Drawing.Point(172, 124);
            this.browseTextBox.Name = "browseTextBox";
            this.browseTextBox.Size = new System.Drawing.Size(303, 20);
            this.browseTextBox.TabIndex = 1;
            this.browseTextBox.TextChanged += new System.EventHandler(this.BrowseTextChanged);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(371, 263);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(480, 263);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButtonClick);
            // 
            // freeSpaceLabel
            // 
            this.freeSpaceLabel.AutoSize = true;
            this.freeSpaceLabel.Location = new System.Drawing.Point(169, 233);
            this.freeSpaceLabel.Name = "freeSpaceLabel";
            this.freeSpaceLabel.Size = new System.Drawing.Size(65, 13);
            this.freeSpaceLabel.TabIndex = 4;
            this.freeSpaceLabel.Text = "Free Space:";
            // 
            // freeSpace
            // 
            this.freeSpace.AutoSize = true;
            this.freeSpace.Location = new System.Drawing.Point(231, 233);
            this.freeSpace.Name = "freeSpace";
            this.freeSpace.Size = new System.Drawing.Size(28, 13);
            this.freeSpace.TabIndex = 5;
            this.freeSpace.Text = "0GB";
            // 
            // totalSpaceLabel
            // 
            this.totalSpaceLabel.AutoSize = true;
            this.totalSpaceLabel.Location = new System.Drawing.Point(169, 209);
            this.totalSpaceLabel.Name = "totalSpaceLabel";
            this.totalSpaceLabel.Size = new System.Drawing.Size(114, 13);
            this.totalSpaceLabel.TabIndex = 6;
            this.totalSpaceLabel.Text = "Total Space Required:";
            // 
            // totalSpace
            // 
            this.totalSpace.AutoSize = true;
            this.totalSpace.Location = new System.Drawing.Point(280, 209);
            this.totalSpace.Name = "totalSpace";
            this.totalSpace.Size = new System.Drawing.Size(28, 13);
            this.totalSpace.TabIndex = 7;
            this.totalSpace.Text = "0GB";
            // 
            // mainLabel
            // 
            this.mainLabel.AutoSize = true;
            this.mainLabel.Location = new System.Drawing.Point(200, 62);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(292, 13);
            this.mainLabel.TabIndex = 8;
            this.mainLabel.Text = "Please choose folder where to store your server disk images:";
            // 
            // helpButton
            // 
            this.helpButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.helpButton.FlatAppearance.BorderSize = 0;
            this.helpButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpButton.ForeColor = System.Drawing.Color.Transparent;
            this.helpButton.Location = new System.Drawing.Point(544, 1);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(23, 22);
            this.helpButton.TabIndex = 12;
            this.helpButton.Tag = "Help";
            this.helpButton.UseVisualStyleBackColor = false;
            this.helpButton.Click += new System.EventHandler(this.HelpButtonClick);
            // 
            // logoPicture
            // 
            this.logoPicture.ErrorImage = null;
            this.logoPicture.Image = ((System.Drawing.Image)(resources.GetObject("logoPicture.Image")));
            this.logoPicture.Location = new System.Drawing.Point(-3, 1);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(137, 299);
            this.logoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPicture.TabIndex = 16;
            this.logoPicture.TabStop = false;
            // 
            // errorPicture
            // 
            this.errorPicture.ErrorImage = null;
            this.errorPicture.Image = global::CloudScraper.Properties.Resources.Warning;
            this.errorPicture.Location = new System.Drawing.Point(172, 257);
            this.errorPicture.Name = "errorPicture";
            this.errorPicture.Size = new System.Drawing.Size(16, 16);
            this.errorPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.errorPicture.TabIndex = 17;
            this.errorPicture.TabStop = false;
            this.errorPicture.Visible = false;
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.errorLabel.Location = new System.Drawing.Point(194, 260);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(115, 13);
            this.errorLabel.TabIndex = 18;
            this.errorLabel.Text = "Not enough space!";
            this.errorLabel.Visible = false;
            // 
            // ImagesPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.errorPicture);
            this.Controls.Add(this.logoPicture);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.mainLabel);
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
            this.Text = "Images Location...";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.ImagesPathFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorPicture)).EndInit();
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
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.PictureBox logoPicture;
        private System.Windows.Forms.PictureBox errorPicture;
        private System.Windows.Forms.Label errorLabel;
    }
}