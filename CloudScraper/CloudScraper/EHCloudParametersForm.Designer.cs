namespace CloudScraper
{
    partial class EHCloudParametersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EHCloudParametersForm));
            this.backButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.awsKeyTextBox = new System.Windows.Forms.TextBox();
            this.uuidTextBox = new System.Windows.Forms.TextBox();
            this.awsKeyLabel = new System.Windows.Forms.Label();
            this.awsIdLabel = new System.Windows.Forms.Label();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.regionLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.advancedCheckBox = new System.Windows.Forms.CheckBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(13, 263);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(542, 242);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.awsKeyTextBox);
            this.tabPage1.Controls.Add(this.uuidTextBox);
            this.tabPage1.Controls.Add(this.awsKeyLabel);
            this.tabPage1.Controls.Add(this.awsIdLabel);
            this.tabPage1.Controls.Add(this.regionComboBox);
            this.tabPage1.Controls.Add(this.regionLabel);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(534, 216);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Basic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // awsKeyTextBox
            // 
            this.awsKeyTextBox.Location = new System.Drawing.Point(193, 140);
            this.awsKeyTextBox.MaxLength = 40;
            this.awsKeyTextBox.Name = "awsKeyTextBox";
            this.awsKeyTextBox.PasswordChar = '*';
            this.awsKeyTextBox.Size = new System.Drawing.Size(219, 20);
            this.awsKeyTextBox.TabIndex = 7;
            this.awsKeyTextBox.TextChanged += new System.EventHandler(this.ApiKeyChanged);
            // 
            // uuidTextBox
            // 
            this.uuidTextBox.Location = new System.Drawing.Point(193, 90);
            this.uuidTextBox.MaxLength = 36;
            this.uuidTextBox.Name = "uuidTextBox";
            this.uuidTextBox.Size = new System.Drawing.Size(220, 20);
            this.uuidTextBox.TabIndex = 3;
            this.uuidTextBox.TextChanged += new System.EventHandler(this.UUIDChanged);
            // 
            // awsKeyLabel
            // 
            this.awsKeyLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.awsKeyLabel.Location = new System.Drawing.Point(0, 126);
            this.awsKeyLabel.Name = "awsKeyLabel";
            this.awsKeyLabel.Size = new System.Drawing.Size(194, 47);
            this.awsKeyLabel.TabIndex = 6;
            this.awsKeyLabel.Text = "API Secret Key:";
            this.awsKeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // awsIdLabel
            // 
            this.awsIdLabel.Location = new System.Drawing.Point(7, 74);
            this.awsIdLabel.Name = "awsIdLabel";
            this.awsIdLabel.Size = new System.Drawing.Size(186, 50);
            this.awsIdLabel.TabIndex = 4;
            this.awsIdLabel.Text = "User UUID:";
            this.awsIdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // regionComboBox
            // 
            this.regionComboBox.Location = new System.Drawing.Point(193, 38);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(219, 21);
            this.regionComboBox.TabIndex = 8;
            this.regionComboBox.SelectedIndexChanged += new System.EventHandler(this.RegionChanged);
            // 
            // regionLabel
            // 
            this.regionLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.regionLabel.Location = new System.Drawing.Point(6, 23);
            this.regionLabel.Name = "regionLabel";
            this.regionLabel.Size = new System.Drawing.Size(187, 48);
            this.regionLabel.TabIndex = 2;
            this.regionLabel.Text = "Choose Region:";
            this.regionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.advancedCheckBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(534, 216);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // advancedCheckBox
            // 
            this.advancedCheckBox.AutoSize = true;
            this.advancedCheckBox.Location = new System.Drawing.Point(90, 15);
            this.advancedCheckBox.Name = "advancedCheckBox";
            this.advancedCheckBox.Size = new System.Drawing.Size(89, 17);
            this.advancedCheckBox.TabIndex = 8;
            this.advancedCheckBox.Text = "Direct upload";
            this.advancedCheckBox.UseVisualStyleBackColor = true;
            this.advancedCheckBox.CheckedChanged += new System.EventHandler(this.AdvancedChecked);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(476, 263);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 10;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.NextButtonClick);
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
            this.helpButton.TabIndex = 11;
            this.helpButton.Tag = "Help";
            this.helpButton.UseVisualStyleBackColor = false;
            this.helpButton.Click += new System.EventHandler(this.HelpButtonClick);
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(359, 263);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(95, 23);
            this.testButton.TabIndex = 12;
            this.testButton.Text = "Test Connection";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.TestButtonClick);
            // 
            // EHCloudParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.backButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EHCloudParametersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloud Options";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.CloudParametersLoad);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox awsKeyTextBox;
        private System.Windows.Forms.TextBox uuidTextBox;
        private System.Windows.Forms.Label awsKeyLabel;
        private System.Windows.Forms.Label awsIdLabel;
        private System.Windows.Forms.ComboBox regionComboBox;
        private System.Windows.Forms.Label regionLabel;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.CheckBox advancedCheckBox;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.ToolTip toolTip;
    }
}