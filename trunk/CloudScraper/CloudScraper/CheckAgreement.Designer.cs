namespace CloudScraper
{
    partial class CheckAgreement
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckAgreement));
            this.buttonYes = new System.Windows.Forms.Button();
            this.buttonNo = new System.Windows.Forms.Button();
            this.CheckLabel = new System.Windows.Forms.Label();
            this.LicenseValiditi = new System.Windows.Forms.Label();
            this.Date = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonYes
            // 
            this.buttonYes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonYes.Location = new System.Drawing.Point(61, 99);
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.Size = new System.Drawing.Size(73, 43);
            this.buttonYes.TabIndex = 0;
            this.buttonYes.Text = "Yes";
            this.buttonYes.UseVisualStyleBackColor = true;
            this.buttonYes.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonNo
            // 
            this.buttonNo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonNo.Location = new System.Drawing.Point(185, 99);
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.Size = new System.Drawing.Size(73, 43);
            this.buttonNo.TabIndex = 1;
            this.buttonNo.Text = "No";
            this.buttonNo.UseVisualStyleBackColor = true;
            this.buttonNo.Click += new System.EventHandler(this.button2_Click);
            this.buttonNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.buttonNo_KeyDown);
            // 
            // CheckLabel
            // 
            this.CheckLabel.AutoSize = true;
            this.CheckLabel.Cursor = System.Windows.Forms.Cursors.Cross;
            this.CheckLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CheckLabel.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold);
            this.CheckLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CheckLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CheckLabel.Location = new System.Drawing.Point(33, 52);
            this.CheckLabel.Name = "CheckLabel";
            this.CheckLabel.Size = new System.Drawing.Size(254, 16);
            this.CheckLabel.TabIndex = 2;
            this.CheckLabel.Text = "Do you want to overwrite this file?";
            this.CheckLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // LicenseValiditi
            // 
            this.LicenseValiditi.AutoSize = true;
            this.LicenseValiditi.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold);
            this.LicenseValiditi.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.LicenseValiditi.Location = new System.Drawing.Point(7, 22);
            this.LicenseValiditi.Name = "LicenseValiditi";
            this.LicenseValiditi.Size = new System.Drawing.Size(195, 16);
            this.LicenseValiditi.TabIndex = 3;
            this.LicenseValiditi.Text = "Your license is valid until ";
            this.LicenseValiditi.Click += new System.EventHandler(this.LicenseValiditi_Click);
            // 
            // Date
            // 
            this.Date.AutoSize = true;
            this.Date.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Bold);
            this.Date.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Date.Location = new System.Drawing.Point(208, 22);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(100, 16);
            this.Date.TabIndex = 4;
            this.Date.Text = "dd.mm.yyyy";
            this.Date.Click += new System.EventHandler(this.Date_Click);
            // 
            // CheckAgreement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(318, 161);
            this.Controls.Add(this.Date);
            this.Controls.Add(this.LicenseValiditi);
            this.Controls.Add(this.CheckLabel);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.buttonYes);
            this.Name = "CheckAgreement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CheckAgreement";
            this.Load += new System.EventHandler(this.CheckAgreement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.Label CheckLabel;
        private System.Windows.Forms.Label LicenseValiditi;
        private System.Windows.Forms.Label Date;
    }
}