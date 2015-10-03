namespace CloudScraper
{
    partial class NewResumeForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewResumeForm));
            this.startNewButton = new System.Windows.Forms.Button();
            this.resumeButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.AddLicenseButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startNewButton
            // 
            this.startNewButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.startNewButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("startNewButton.BackgroundImage")));
            this.startNewButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.startNewButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.startNewButton.Location = new System.Drawing.Point(196, 50);
            this.startNewButton.Name = "startNewButton";
            this.startNewButton.Size = new System.Drawing.Size(160, 54);
            this.startNewButton.TabIndex = 0;
            this.startNewButton.Tag = "";
            this.startNewButton.Text = "Start New Transfer...";
            this.startNewButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.startNewButton.UseVisualStyleBackColor = false;
            this.startNewButton.Click += new System.EventHandler(this.StartNewButtonClick);
            // 
            // resumeButton
            // 
            this.resumeButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.resumeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("resumeButton.BackgroundImage")));
            this.resumeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.resumeButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.resumeButton.Location = new System.Drawing.Point(196, 123);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(160, 54);
            this.resumeButton.TabIndex = 1;
            this.resumeButton.Tag = "";
            this.resumeButton.Text = "Resume Existing Transfer...";
            this.resumeButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.resumeButton.UseVisualStyleBackColor = false;
            this.resumeButton.Click += new System.EventHandler(this.ResumeButtonClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AddLicenseButton
            // 
            this.AddLicenseButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AddLicenseButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("AddLicenseButton.BackgroundImage")));
            this.AddLicenseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddLicenseButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.AddLicenseButton.Location = new System.Drawing.Point(196, 195);
            this.AddLicenseButton.Name = "AddLicenseButton";
            this.AddLicenseButton.Size = new System.Drawing.Size(160, 54);
            this.AddLicenseButton.TabIndex = 19;
            this.AddLicenseButton.Tag = "";
            this.AddLicenseButton.Text = "Add License";
            this.AddLicenseButton.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AddLicenseButton.UseVisualStyleBackColor = false;
            this.AddLicenseButton.Click += new System.EventHandler(this.AddLicenseButton_Click_1);
            // 
            // helpButton
            // 
            this.helpButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.helpButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.helpButton.FlatAppearance.BorderSize = 0;
            this.helpButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpButton.ForeColor = System.Drawing.Color.Transparent;
            this.helpButton.Location = new System.Drawing.Point(544, 1);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(23, 22);
            this.helpButton.TabIndex = 18;
            this.helpButton.Tag = "Help";
            this.helpButton.UseVisualStyleBackColor = false;
            this.helpButton.Click += new System.EventHandler(this.HelpButtonClick);
            // 
            // NewResumeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.AddLicenseButton);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.resumeButton);
            this.Controls.Add(this.startNewButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewResumeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloudscraper Server Copy";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.NewResumeFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startNewButton;
        private System.Windows.Forms.Button resumeButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button AddLicenseButton;
        private System.Windows.Forms.Button helpButton;
    }
}

