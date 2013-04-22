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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveTransferTaskForm));
            this.saveTransferTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.nextButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.mainLabel = new System.Windows.Forms.Label();
            this.helpButton = new System.Windows.Forms.Button();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // saveTransferTextBox
            // 
            this.saveTransferTextBox.Location = new System.Drawing.Point(172, 125);
            this.saveTransferTextBox.Name = "saveTransferTextBox";
            this.saveTransferTextBox.Size = new System.Drawing.Size(303, 20);
            this.saveTransferTextBox.TabIndex = 0;
            this.saveTransferTextBox.TextChanged += new System.EventHandler(this.SaveTransferChanged);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(481, 123);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.saveTransferTask_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(480, 263);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 2;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButtonClick);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(370, 263);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 3;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // mainLabel
            // 
            this.mainLabel.Location = new System.Drawing.Point(141, 1);
            this.mainLabel.Name = "mainLabel";
            this.mainLabel.Size = new System.Drawing.Size(414, 121);
            this.mainLabel.TabIndex = 4;
            this.mainLabel.Text = "Select path to save transfer file:";
            this.mainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // helpButton
            // 
            this.helpButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.helpButton.FlatAppearance.BorderSize = 0;
            this.helpButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpButton.ForeColor = System.Drawing.Color.Transparent;
            this.helpButton.Location = new System.Drawing.Point(543, 1);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(23, 22);
            this.helpButton.TabIndex = 14;
            this.helpButton.Tag = "Help";
            this.helpButton.UseVisualStyleBackColor = false;
            this.helpButton.Click += new System.EventHandler(this.HelpButtonClick);
            // 
            // logoPicture
            // 
            this.logoPicture.ErrorImage = null;
            this.logoPicture.Location = new System.Drawing.Point(-2, 1);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(137, 299);
            this.logoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPicture.TabIndex = 15;
            this.logoPicture.TabStop = false;
            // 
            // SaveTransferTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.logoPicture);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.mainLabel);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.saveTransferTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SaveTransferTaskForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Save transfer task";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.SaveTransferTaskLoad);
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox saveTransferTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Label mainLabel;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.PictureBox logoPicture;
    }
}