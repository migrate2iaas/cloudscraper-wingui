namespace CloudScraper
{
    partial class ChooseCloudForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChooseCloudForm));
            this.backButton = new System.Windows.Forms.Button();
            this.amazonButton = new System.Windows.Forms.Button();
            this.windowsAzureButton = new System.Windows.Forms.Button();
            this.elasticHostsButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.logoPicture = new System.Windows.Forms.PictureBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(150, 263);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // amazonButton
            // 
            this.amazonButton.Location = new System.Drawing.Point(255, 22);
            this.amazonButton.Name = "amazonButton";
            this.amazonButton.Size = new System.Drawing.Size(189, 70);
            this.amazonButton.TabIndex = 2;
            this.amazonButton.Text = "Amazon EC2";
            this.amazonButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.amazonButton.UseVisualStyleBackColor = true;
            // 
            // windowsAzureButton
            // 
            this.windowsAzureButton.Enabled = false;
            this.windowsAzureButton.Location = new System.Drawing.Point(255, 98);
            this.windowsAzureButton.Name = "windowsAzureButton";
            this.windowsAzureButton.Size = new System.Drawing.Size(189, 70);
            this.windowsAzureButton.TabIndex = 3;
            this.windowsAzureButton.Text = "Windows Azure (Not supported)";
            this.windowsAzureButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.windowsAzureButton.UseVisualStyleBackColor = true;
            // 
            // elasticHostsButton
            // 
            this.elasticHostsButton.Location = new System.Drawing.Point(255, 174);
            this.elasticHostsButton.Name = "elasticHostsButton";
            this.elasticHostsButton.Size = new System.Drawing.Size(189, 70);
            this.elasticHostsButton.TabIndex = 4;
            this.elasticHostsButton.Text = "ElasticHosts";
            this.elasticHostsButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.elasticHostsButton.UseVisualStyleBackColor = true;
            // 
            // helpButton
            // 
            this.helpButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.helpButton.FlatAppearance.BorderSize = 0;
            this.helpButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpButton.ForeColor = System.Drawing.Color.Transparent;
            this.helpButton.Location = new System.Drawing.Point(544, 0);
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
            this.logoPicture.Location = new System.Drawing.Point(-2, 0);
            this.logoPicture.Name = "logoPicture";
            this.logoPicture.Size = new System.Drawing.Size(137, 299);
            this.logoPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPicture.TabIndex = 18;
            this.logoPicture.TabStop = false;
            // 
            // ChooseCloudForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.logoPicture);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.elasticHostsButton);
            this.Controls.Add(this.windowsAzureButton);
            this.Controls.Add(this.amazonButton);
            this.Controls.Add(this.backButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseCloudForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose Your Cloud";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.ChooseCloudLoad);
            ((System.ComponentModel.ISupportInitialize)(this.logoPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button amazonButton;
        private System.Windows.Forms.Button windowsAzureButton;
        private System.Windows.Forms.Button elasticHostsButton;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.PictureBox logoPicture;
        private System.Windows.Forms.ToolTip toolTip;
    }
}