namespace CloudScraper
{
    partial class CloudParametersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloudParametersForm));
            this.backButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.keyLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.regionLabel = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.drivesListLabel = new System.Windows.Forms.Label();
            this.drivesListBox = new System.Windows.Forms.CheckedListBox();
            this.deduplcationCheckBox = new System.Windows.Forms.CheckBox();
            this.folderKeyLabel = new System.Windows.Forms.Label();
            this.folderKeyBox = new System.Windows.Forms.TextBox();
            this.advancedCheckBox = new System.Windows.Forms.CheckBox();
            this.groupLabel = new System.Windows.Forms.Label();
            this.zoneLabel = new System.Windows.Forms.Label();
            this.serverTypeComboBox = new System.Windows.Forms.ComboBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.bucketTextBox = new System.Windows.Forms.TextBox();
            this.bucketLabel = new System.Windows.Forms.Label();
            this.zoneComboBox = new System.Windows.Forms.ComboBox();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
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
            this.tabPage1.Controls.Add(this.keyTextBox);
            this.tabPage1.Controls.Add(this.idTextBox);
            this.tabPage1.Controls.Add(this.keyLabel);
            this.tabPage1.Controls.Add(this.idLabel);
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
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(193, 140);
            this.keyTextBox.MaxLength = 40;
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.PasswordChar = '*';
            this.keyTextBox.Size = new System.Drawing.Size(219, 20);
            this.keyTextBox.TabIndex = 7;
            this.keyTextBox.TextChanged += new System.EventHandler(this.KeyChanged);
            this.keyTextBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(193, 90);
            this.idTextBox.MaxLength = 20;
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(220, 20);
            this.idTextBox.TabIndex = 3;
            this.idTextBox.TextChanged += new System.EventHandler(this.IDChanged);
            this.idTextBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
            // 
            // keyLabel
            // 
            this.keyLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.keyLabel.Location = new System.Drawing.Point(0, 126);
            this.keyLabel.Name = "keyLabel";
            this.keyLabel.Size = new System.Drawing.Size(194, 47);
            this.keyLabel.TabIndex = 6;
            this.keyLabel.Text = "Key Label:";
            this.keyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // idLabel
            // 
            this.idLabel.Location = new System.Drawing.Point(7, 74);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(186, 50);
            this.idLabel.TabIndex = 4;
            this.idLabel.Text = "Id Label:";
            this.idLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // regionComboBox
            // 
            this.regionComboBox.Location = new System.Drawing.Point(192, 38);
            this.regionComboBox.Name = "regionComboBox";
            this.regionComboBox.Size = new System.Drawing.Size(220, 21);
            this.regionComboBox.TabIndex = 1;
            this.regionComboBox.SelectedIndexChanged += new System.EventHandler(this.RegionListBoxChanged);
            // 
            // regionLabel
            // 
            this.regionLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.regionLabel.Location = new System.Drawing.Point(6, 23);
            this.regionLabel.Name = "regionLabel";
            this.regionLabel.Size = new System.Drawing.Size(187, 48);
            this.regionLabel.TabIndex = 2;
            this.regionLabel.Text = "Region Label:";
            this.regionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.drivesListLabel);
            this.tabPage2.Controls.Add(this.drivesListBox);
            this.tabPage2.Controls.Add(this.deduplcationCheckBox);
            this.tabPage2.Controls.Add(this.folderKeyLabel);
            this.tabPage2.Controls.Add(this.folderKeyBox);
            this.tabPage2.Controls.Add(this.advancedCheckBox);
            this.tabPage2.Controls.Add(this.groupLabel);
            this.tabPage2.Controls.Add(this.zoneLabel);
            this.tabPage2.Controls.Add(this.serverTypeComboBox);
            this.tabPage2.Controls.Add(this.typeLabel);
            this.tabPage2.Controls.Add(this.bucketTextBox);
            this.tabPage2.Controls.Add(this.bucketLabel);
            this.tabPage2.Controls.Add(this.zoneComboBox);
            this.tabPage2.Controls.Add(this.groupComboBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(534, 216);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Advanced";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // drivesListLabel
            // 
            this.drivesListLabel.AutoSize = true;
            this.drivesListLabel.Location = new System.Drawing.Point(6, 92);
            this.drivesListLabel.Name = "drivesListLabel";
            this.drivesListLabel.Size = new System.Drawing.Size(145, 13);
            this.drivesListLabel.TabIndex = 15;
            this.drivesListLabel.Text = "Choose drives to be avoided:";
            // 
            // drivesListBox
            // 
            this.drivesListBox.FormattingEnabled = true;
            this.drivesListBox.HorizontalScrollbar = true;
            this.drivesListBox.Location = new System.Drawing.Point(0, 111);
            this.drivesListBox.Name = "drivesListBox";
            this.drivesListBox.Size = new System.Drawing.Size(534, 109);
            this.drivesListBox.TabIndex = 14;
            this.drivesListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.DrivesSelect);
            // 
            // deduplcationCheckBox
            // 
            this.deduplcationCheckBox.AutoSize = true;
            this.deduplcationCheckBox.Location = new System.Drawing.Point(90, 58);
            this.deduplcationCheckBox.Name = "deduplcationCheckBox";
            this.deduplcationCheckBox.Size = new System.Drawing.Size(111, 17);
            this.deduplcationCheckBox.TabIndex = 13;
            this.deduplcationCheckBox.Text = "Use deduplication";
            this.deduplcationCheckBox.UseVisualStyleBackColor = true;
            this.deduplcationCheckBox.CheckedChanged += new System.EventHandler(this.UseDeduplicationChecked);
            // 
            // folderKeyLabel
            // 
            this.folderKeyLabel.Enabled = false;
            this.folderKeyLabel.Location = new System.Drawing.Point(0, 64);
            this.folderKeyLabel.Name = "folderKeyLabel";
            this.folderKeyLabel.Size = new System.Drawing.Size(213, 41);
            this.folderKeyLabel.TabIndex = 10;
            this.folderKeyLabel.Text = "Specify folder (key):";
            this.folderKeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // folderKeyBox
            // 
            this.folderKeyBox.Enabled = false;
            this.folderKeyBox.Location = new System.Drawing.Point(216, 75);
            this.folderKeyBox.MaxLength = 20;
            this.folderKeyBox.Name = "folderKeyBox";
            this.folderKeyBox.Size = new System.Drawing.Size(235, 20);
            this.folderKeyBox.TabIndex = 9;
            this.folderKeyBox.TextChanged += new System.EventHandler(this.FolderKeyChanged);
            this.folderKeyBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
            // 
            // advancedCheckBox
            // 
            this.advancedCheckBox.AutoSize = true;
            this.advancedCheckBox.Location = new System.Drawing.Point(90, 15);
            this.advancedCheckBox.Name = "advancedCheckBox";
            this.advancedCheckBox.Size = new System.Drawing.Size(114, 17);
            this.advancedCheckBox.TabIndex = 8;
            this.advancedCheckBox.Text = "Advanced settings";
            this.advancedCheckBox.UseVisualStyleBackColor = true;
            this.advancedCheckBox.CheckedChanged += new System.EventHandler(this.AdvancedChecked);
            // 
            // groupLabel
            // 
            this.groupLabel.Enabled = false;
            this.groupLabel.Location = new System.Drawing.Point(3, 167);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(213, 45);
            this.groupLabel.TabIndex = 7;
            this.groupLabel.Text = "Specify your security group:";
            this.groupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // zoneLabel
            // 
            this.zoneLabel.Enabled = false;
            this.zoneLabel.Location = new System.Drawing.Point(0, 130);
            this.zoneLabel.Name = "zoneLabel";
            this.zoneLabel.Size = new System.Drawing.Size(216, 50);
            this.zoneLabel.TabIndex = 5;
            this.zoneLabel.Text = "Specify your availability zone:";
            this.zoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // serverTypeComboBox
            // 
            this.serverTypeComboBox.Enabled = false;
            this.serverTypeComboBox.FormattingEnabled = true;
            this.serverTypeComboBox.Location = new System.Drawing.Point(217, 110);
            this.serverTypeComboBox.Name = "serverTypeComboBox";
            this.serverTypeComboBox.Size = new System.Drawing.Size(234, 21);
            this.serverTypeComboBox.TabIndex = 4;
            this.serverTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ServerTypeChanged);
            // 
            // typeLabel
            // 
            this.typeLabel.Enabled = false;
            this.typeLabel.Location = new System.Drawing.Point(0, 97);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(216, 46);
            this.typeLabel.TabIndex = 2;
            this.typeLabel.Text = "Specify your cloud server type:";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bucketTextBox
            // 
            this.bucketTextBox.Enabled = false;
            this.bucketTextBox.Location = new System.Drawing.Point(216, 38);
            this.bucketTextBox.MaxLength = 40;
            this.bucketTextBox.Name = "bucketTextBox";
            this.bucketTextBox.Size = new System.Drawing.Size(235, 20);
            this.bucketTextBox.TabIndex = 1;
            this.bucketTextBox.TextChanged += new System.EventHandler(this.BucketChanged);
            this.bucketTextBox.Leave += new System.EventHandler(this.BucketTextBoxLeave);
            this.bucketTextBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
            // 
            // bucketLabel
            // 
            this.bucketLabel.Enabled = false;
            this.bucketLabel.Location = new System.Drawing.Point(3, 32);
            this.bucketLabel.Name = "bucketLabel";
            this.bucketLabel.Size = new System.Drawing.Size(213, 30);
            this.bucketLabel.TabIndex = 0;
            this.bucketLabel.Text = "Specify your S3 Bucket:";
            this.bucketLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // zoneComboBox
            // 
            this.zoneComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.zoneComboBox.Enabled = false;
            this.zoneComboBox.FormattingEnabled = true;
            this.zoneComboBox.Location = new System.Drawing.Point(216, 146);
            this.zoneComboBox.MaxLength = 10;
            this.zoneComboBox.Name = "zoneComboBox";
            this.zoneComboBox.Size = new System.Drawing.Size(237, 21);
            this.zoneComboBox.TabIndex = 11;
            this.zoneComboBox.SelectedIndexChanged += new System.EventHandler(this.ZoneComboBoxIndexChanged);
            this.zoneComboBox.TextChanged += new System.EventHandler(this.ZoneComboBoxTextChanged);
            this.zoneComboBox.MouseEnter += new System.EventHandler(this.TextBoxMouseEnter);
            this.zoneComboBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
            // 
            // groupComboBox
            // 
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.groupComboBox.Enabled = false;
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(216, 180);
            this.groupComboBox.MaxLength = 10;
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(237, 21);
            this.groupComboBox.TabIndex = 12;
            this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.GroupComboBoxIndexChanged);
            this.groupComboBox.TextChanged += new System.EventHandler(this.GroupComboBoxTextChanged);
            this.groupComboBox.MouseEnter += new System.EventHandler(this.TextBoxMouseEnter);
            this.groupComboBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
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
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 1500;
            this.toolTip.AutoPopDelay = 15000;
            this.toolTip.InitialDelay = 1500;
            this.toolTip.ReshowDelay = 600;
            // 
            // CloudParametersForm
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
            this.Name = "CloudParametersForm";
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

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        protected System.Windows.Forms.Label regionLabel;
        protected System.Windows.Forms.TextBox keyTextBox;
        protected System.Windows.Forms.TextBox idTextBox;
        protected System.Windows.Forms.Label keyLabel;
        protected System.Windows.Forms.Label idLabel;
        protected System.Windows.Forms.ComboBox regionComboBox;
        protected System.Windows.Forms.Label groupLabel;
        protected System.Windows.Forms.Label zoneLabel;
        protected System.Windows.Forms.ComboBox serverTypeComboBox;
        protected System.Windows.Forms.Label typeLabel;
        protected System.Windows.Forms.TextBox bucketTextBox;
        protected System.Windows.Forms.Label bucketLabel;
        protected System.Windows.Forms.CheckBox advancedCheckBox;
        protected System.Windows.Forms.Label folderKeyLabel;
        protected System.Windows.Forms.TextBox folderKeyBox;
        protected System.Windows.Forms.ComboBox zoneComboBox;
        protected System.Windows.Forms.ComboBox groupComboBox;
        protected System.Windows.Forms.Button backButton;
        protected System.Windows.Forms.Button nextButton;
        protected System.Windows.Forms.Button testButton;
        protected System.Windows.Forms.Button helpButton;
        protected System.Windows.Forms.ToolTip toolTip;
        protected System.Windows.Forms.CheckBox deduplcationCheckBox;
        protected System.Windows.Forms.CheckedListBox drivesListBox;
        protected System.Windows.Forms.Label drivesListLabel;
    }
}