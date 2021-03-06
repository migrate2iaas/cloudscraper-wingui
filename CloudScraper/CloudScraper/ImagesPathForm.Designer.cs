﻿namespace CloudScraper
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImagesPathForm));
            this.totalSpaceLabel = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.browseButton = new System.Windows.Forms.Button();
            this.browseTextBox = new System.Windows.Forms.TextBox();
            this.backButton = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.freeSpaceLabel = new System.Windows.Forms.Label();
            this.freeSpace = new System.Windows.Forms.Label();
            this.totalSpace = new System.Windows.Forms.Label();
            this.mainLabel = new System.Windows.Forms.Label();
            this.helpButton = new System.Windows.Forms.Button();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.errorPicture = new System.Windows.Forms.PictureBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // totalSpaceLabel
            // 
            this.totalSpaceLabel.AutoSize = true;
            this.totalSpaceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalSpaceLabel.Location = new System.Drawing.Point(169, 209);
            this.totalSpaceLabel.Name = "totalSpaceLabel";
            this.totalSpaceLabel.Size = new System.Drawing.Size(135, 13);
            this.totalSpaceLabel.TabIndex = 6;
            this.totalSpaceLabel.Text = "Total Space Required:";
            this.totalSpaceLabel.Click += new System.EventHandler(this.totalSpaceLabel_Click);
            // 
            // browseButton
            // 
            this.browseButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.browseButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("browseButton.BackgroundImage")));
            this.browseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.browseButton.Location = new System.Drawing.Point(481, 122);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = false;
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
            this.backButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backButton.BackgroundImage")));
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backButton.Location = new System.Drawing.Point(371, 263);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 2;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nextButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextButton.BackgroundImage")));
            this.nextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nextButton.Location = new System.Drawing.Point(480, 263);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = false;
            this.nextButton.Click += new System.EventHandler(this.NextButtonClick);
            // 
            // freeSpaceLabel
            // 
            this.freeSpaceLabel.AutoSize = true;
            this.freeSpaceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freeSpaceLabel.Location = new System.Drawing.Point(169, 233);
            this.freeSpaceLabel.Name = "freeSpaceLabel";
            this.freeSpaceLabel.Size = new System.Drawing.Size(76, 13);
            this.freeSpaceLabel.TabIndex = 4;
            this.freeSpaceLabel.Text = "Free Space:";
            // 
            // freeSpace
            // 
            this.freeSpace.AutoSize = true;
            this.freeSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freeSpace.Location = new System.Drawing.Point(255, 233);
            this.freeSpace.Name = "freeSpace";
            this.freeSpace.Size = new System.Drawing.Size(31, 13);
            this.freeSpace.TabIndex = 5;
            this.freeSpace.Text = "0GB";
            // 
            // totalSpace
            // 
            this.totalSpace.AutoSize = true;
            this.totalSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalSpace.Location = new System.Drawing.Point(303, 209);
            this.totalSpace.Name = "totalSpace";
            this.totalSpace.Size = new System.Drawing.Size(31, 13);
            this.totalSpace.TabIndex = 7;
            this.totalSpace.Text = "0GB";
            // 
            // mainLabel
            // 
            this.mainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainLabel.Location = new System.Drawing.Point(140, 9);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(415, 110);
            this.mainLabel.TabIndex = 8;
            this.mainLabel.Text = "Please choose folder where to store your server disk images:";
            this.mainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mainLabel.Click += new System.EventHandler(this.mainLabel_Click);
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
            this.logoPicture.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.logoPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.logoPicture.ErrorImage = null;
            this.logoPicture.Location = new System.Drawing.Point(-3, 1);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(137, 299);
            this.logoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPicture.TabIndex = 16;
            this.logoPicture.TabStop = false;
            this.logoPicture.Click += new System.EventHandler(this.logoPicture_Click);
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
            this.errorPicture.Click += new System.EventHandler(this.errorPicture_Click);
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
            this.errorLabel.Click += new System.EventHandler(this.errorLabel_Click);
            // 
            // ImagesPathForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Images Location...";
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
        private System.Windows.Forms.Label totalSpace;
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.PictureBox logoPicture;
        private System.Windows.Forms.PictureBox errorPicture;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label totalSpaceLabel;
    }
}