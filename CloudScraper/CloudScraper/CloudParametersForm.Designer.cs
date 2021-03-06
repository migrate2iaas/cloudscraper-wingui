﻿namespace CloudScraper
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
            this.tabPanel = new System.Windows.Forms.TabControl();
            this.pageBasic = new System.Windows.Forms.TabPage();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.keyLabel = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.regionComboBox = new System.Windows.Forms.ComboBox();
            this.regionLabel = new System.Windows.Forms.Label();
            this.pageAdvanced = new System.Windows.Forms.TabPage();
            this.placeholderForAdvanced = new System.Windows.Forms.Panel();
            this.compressionUpDown = new System.Windows.Forms.NumericUpDown();
            this.ehCompressionLabel = new System.Windows.Forms.Label();
            this.azureCreateNewCertificateButton = new System.Windows.Forms.Button();
            this.azureSubscriptionId = new System.Windows.Forms.TextBox();
            this.azureDeployVirtualMachineCheckBox = new System.Windows.Forms.CheckBox();
            this.azureContainerComboBox = new System.Windows.Forms.ComboBox();
            this.selectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.drivesListLabel = new System.Windows.Forms.Label();
            this.deduplcationCheckBox = new System.Windows.Forms.CheckBox();
            this.folderKeyLabel = new System.Windows.Forms.Label();
            this.folderKeyBox = new System.Windows.Forms.TextBox();
            this.advancedCheckBox = new System.Windows.Forms.CheckBox();
            this.groupLabel = new System.Windows.Forms.Label();
            this.zoneLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.bucketTextBox = new System.Windows.Forms.TextBox();
            this.bucketLabel = new System.Windows.Forms.Label();
            this.zoneComboBox = new System.Windows.Forms.ComboBox();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.serverTypeComboBox = new System.Windows.Forms.ComboBox();
            this.drivesDataGridView = new System.Windows.Forms.DataGridView();
            this.nextButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tabPanel.SuspendLayout();
            this.pageBasic.SuspendLayout();
            this.pageAdvanced.SuspendLayout();
            this.placeholderForAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compressionUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drivesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backButton.BackgroundImage")));
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backButton.Location = new System.Drawing.Point(13, 263);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 0;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // tabPanel
            // 
            this.tabPanel.Controls.Add(this.pageBasic);
            this.tabPanel.Controls.Add(this.pageAdvanced);
            this.tabPanel.Location = new System.Drawing.Point(13, 12);
            this.tabPanel.Multiline = true;
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new System.Drawing.Size(542, 242);
            this.tabPanel.TabIndex = 9;
            this.tabPanel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            // 
            // pageBasic
            // 
            this.pageBasic.BackColor = System.Drawing.Color.White;
            this.pageBasic.Controls.Add(this.keyTextBox);
            this.pageBasic.Controls.Add(this.idTextBox);
            this.pageBasic.Controls.Add(this.keyLabel);
            this.pageBasic.Controls.Add(this.idLabel);
            this.pageBasic.Controls.Add(this.regionComboBox);
            this.pageBasic.Controls.Add(this.regionLabel);
            this.pageBasic.Location = new System.Drawing.Point(4, 22);
            this.pageBasic.Name = "pageBasic";
            this.pageBasic.Padding = new System.Windows.Forms.Padding(3);
            this.pageBasic.Size = new System.Drawing.Size(534, 216);
            this.pageBasic.TabIndex = 0;
            this.pageBasic.Text = "Basic";
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
            this.keyTextBox.Leave += new System.EventHandler(this.OnLeaveEnter);
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
            this.idTextBox.Leave += new System.EventHandler(this.OnLeaveEnter);
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
            this.keyLabel.Click += new System.EventHandler(this.keyLabel_Click);
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
            this.regionComboBox.Leave += new System.EventHandler(this.OnLeaveEnter);
            this.regionComboBox.MouseEnter += new System.EventHandler(this.TextBoxMouseEnter);
            this.regionComboBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
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
            this.regionLabel.Click += new System.EventHandler(this.regionLabel_Click);
            // 
            // pageAdvanced
            // 
            this.pageAdvanced.Controls.Add(this.placeholderForAdvanced);
            this.pageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.pageAdvanced.Name = "pageAdvanced";
            this.pageAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.pageAdvanced.Size = new System.Drawing.Size(534, 216);
            this.pageAdvanced.TabIndex = 1;
            this.pageAdvanced.Text = "Advanced";
            this.pageAdvanced.UseVisualStyleBackColor = true;
            // 
            // placeholderForAdvanced
            // 
            this.placeholderForAdvanced.BackColor = System.Drawing.Color.White;
            this.placeholderForAdvanced.Controls.Add(this.compressionUpDown);
            this.placeholderForAdvanced.Controls.Add(this.ehCompressionLabel);
            this.placeholderForAdvanced.Controls.Add(this.azureCreateNewCertificateButton);
            this.placeholderForAdvanced.Controls.Add(this.azureSubscriptionId);
            this.placeholderForAdvanced.Controls.Add(this.azureDeployVirtualMachineCheckBox);
            this.placeholderForAdvanced.Controls.Add(this.azureContainerComboBox);
            this.placeholderForAdvanced.Controls.Add(this.selectAllCheckBox);
            this.placeholderForAdvanced.Controls.Add(this.drivesListLabel);
            this.placeholderForAdvanced.Controls.Add(this.deduplcationCheckBox);
            this.placeholderForAdvanced.Controls.Add(this.folderKeyLabel);
            this.placeholderForAdvanced.Controls.Add(this.folderKeyBox);
            this.placeholderForAdvanced.Controls.Add(this.advancedCheckBox);
            this.placeholderForAdvanced.Controls.Add(this.groupLabel);
            this.placeholderForAdvanced.Controls.Add(this.zoneLabel);
            this.placeholderForAdvanced.Controls.Add(this.typeLabel);
            this.placeholderForAdvanced.Controls.Add(this.bucketTextBox);
            this.placeholderForAdvanced.Controls.Add(this.bucketLabel);
            this.placeholderForAdvanced.Controls.Add(this.zoneComboBox);
            this.placeholderForAdvanced.Controls.Add(this.groupComboBox);
            this.placeholderForAdvanced.Controls.Add(this.serverTypeComboBox);
            this.placeholderForAdvanced.Controls.Add(this.drivesDataGridView);
            this.placeholderForAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeholderForAdvanced.Location = new System.Drawing.Point(3, 3);
            this.placeholderForAdvanced.Name = "placeholderForAdvanced";
            this.placeholderForAdvanced.Size = new System.Drawing.Size(528, 210);
            this.placeholderForAdvanced.TabIndex = 0;
            // 
            // compressionUpDown
            // 
            this.compressionUpDown.Location = new System.Drawing.Point(364, 55);
            this.compressionUpDown.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.compressionUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.compressionUpDown.Name = "compressionUpDown";
            this.compressionUpDown.Size = new System.Drawing.Size(35, 20);
            this.compressionUpDown.TabIndex = 23;
            this.compressionUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // ehCompressionLabel
            // 
            this.ehCompressionLabel.AutoSize = true;
            this.ehCompressionLabel.Location = new System.Drawing.Point(262, 58);
            this.ehCompressionLabel.Name = "ehCompressionLabel";
            this.ehCompressionLabel.Size = new System.Drawing.Size(96, 13);
            this.ehCompressionLabel.TabIndex = 22;
            this.ehCompressionLabel.Text = "Compression Level";
            // 
            // azureCreateNewCertificateButton
            // 
            this.azureCreateNewCertificateButton.Enabled = false;
            this.azureCreateNewCertificateButton.Location = new System.Drawing.Point(216, 178);
            this.azureCreateNewCertificateButton.Name = "azureCreateNewCertificateButton";
            this.azureCreateNewCertificateButton.Size = new System.Drawing.Size(183, 23);
            this.azureCreateNewCertificateButton.TabIndex = 20;
            this.azureCreateNewCertificateButton.Text = "Create and upload new certificate";
            this.azureCreateNewCertificateButton.UseVisualStyleBackColor = true;
            this.azureCreateNewCertificateButton.Click += new System.EventHandler(this.AzureCreateNewCertificateButtonClick);
            this.azureCreateNewCertificateButton.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
            // 
            // azureSubscriptionId
            // 
            this.azureSubscriptionId.Enabled = false;
            this.azureSubscriptionId.Location = new System.Drawing.Point(216, 109);
            this.azureSubscriptionId.Name = "azureSubscriptionId";
            this.azureSubscriptionId.Size = new System.Drawing.Size(235, 20);
            this.azureSubscriptionId.TabIndex = 19;
            this.azureSubscriptionId.TextChanged += new System.EventHandler(this.AzureSubscriptionIdTextChanged);
            this.azureSubscriptionId.Leave += new System.EventHandler(this.OnLeaveEnter);
            this.azureSubscriptionId.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
            // 
            // azureDeployVirtualMachineCheckBox
            // 
            this.azureDeployVirtualMachineCheckBox.AutoSize = true;
            this.azureDeployVirtualMachineCheckBox.Enabled = false;
            this.azureDeployVirtualMachineCheckBox.Location = new System.Drawing.Point(90, 77);
            this.azureDeployVirtualMachineCheckBox.Name = "azureDeployVirtualMachineCheckBox";
            this.azureDeployVirtualMachineCheckBox.Size = new System.Drawing.Size(135, 17);
            this.azureDeployVirtualMachineCheckBox.TabIndex = 18;
            this.azureDeployVirtualMachineCheckBox.Text = "Deploy Virtual Machine";
            this.azureDeployVirtualMachineCheckBox.UseVisualStyleBackColor = true;
            this.azureDeployVirtualMachineCheckBox.CheckedChanged += new System.EventHandler(this.AzureDeployVirtualMachineChecked);
            this.azureDeployVirtualMachineCheckBox.Leave += new System.EventHandler(this.OnLeaveEnter);
            // 
            // azureContainerComboBox
            // 
            this.azureContainerComboBox.Enabled = false;
            this.azureContainerComboBox.FormattingEnabled = true;
            this.azureContainerComboBox.Location = new System.Drawing.Point(216, 38);
            this.azureContainerComboBox.MaxDropDownItems = 5;
            this.azureContainerComboBox.Name = "azureContainerComboBox";
            this.azureContainerComboBox.Size = new System.Drawing.Size(235, 21);
            this.azureContainerComboBox.TabIndex = 17;
            this.azureContainerComboBox.TextChanged += new System.EventHandler(this.AzureContainerChanged);
            this.azureContainerComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BucketKeyPress);
            this.azureContainerComboBox.Leave += new System.EventHandler(this.OnLeaveEnter);
            this.azureContainerComboBox.MouseEnter += new System.EventHandler(this.TextBoxMouseEnter);
            this.azureContainerComboBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
            // 
            // selectAllCheckBox
            // 
            this.selectAllCheckBox.AutoSize = true;
            this.selectAllCheckBox.Location = new System.Drawing.Point(433, 91);
            this.selectAllCheckBox.Name = "selectAllCheckBox";
            this.selectAllCheckBox.Size = new System.Drawing.Size(69, 17);
            this.selectAllCheckBox.TabIndex = 16;
            this.selectAllCheckBox.Text = "Select all";
            this.selectAllCheckBox.UseVisualStyleBackColor = true;
            this.selectAllCheckBox.CheckedChanged += new System.EventHandler(this.SelectAll);
            this.selectAllCheckBox.Leave += new System.EventHandler(this.OnLeaveEnter);
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
            this.deduplcationCheckBox.Leave += new System.EventHandler(this.OnLeaveEnter);
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
            this.folderKeyBox.MaxLength = 80;
            this.folderKeyBox.Name = "folderKeyBox";
            this.folderKeyBox.Size = new System.Drawing.Size(235, 20);
            this.folderKeyBox.TabIndex = 9;
            this.folderKeyBox.TextChanged += new System.EventHandler(this.FolderKeyChanged);
            this.folderKeyBox.Leave += new System.EventHandler(this.OnLeaveEnter);
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
            this.advancedCheckBox.Leave += new System.EventHandler(this.OnLeaveEnter);
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
            this.bucketTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BucketKeyPress);
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
            this.zoneComboBox.Leave += new System.EventHandler(this.OnLeaveEnter);
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
            this.groupComboBox.Leave += new System.EventHandler(this.OnLeaveEnter);
            this.groupComboBox.MouseEnter += new System.EventHandler(this.TextBoxMouseEnter);
            this.groupComboBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
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
            this.serverTypeComboBox.Leave += new System.EventHandler(this.OnLeaveEnter);
            this.serverTypeComboBox.MouseEnter += new System.EventHandler(this.TextBoxMouseEnter);
            this.serverTypeComboBox.MouseHover += new System.EventHandler(this.TextBoxMouseEnter);
            // 
            // drivesDataGridView
            // 
            this.drivesDataGridView.AllowUserToAddRows = false;
            this.drivesDataGridView.AllowUserToDeleteRows = false;
            this.drivesDataGridView.AllowUserToResizeRows = false;
            this.drivesDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.drivesDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.drivesDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.drivesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.drivesDataGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.drivesDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.drivesDataGridView.Location = new System.Drawing.Point(0, 106);
            this.drivesDataGridView.MultiSelect = false;
            this.drivesDataGridView.Name = "drivesDataGridView";
            this.drivesDataGridView.RowHeadersVisible = false;
            this.drivesDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.drivesDataGridView.Size = new System.Drawing.Size(528, 104);
            this.drivesDataGridView.TabIndex = 5;
            this.drivesDataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OnSelect);
            // 
            // nextButton
            // 
            this.nextButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nextButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextButton.BackgroundImage")));
            this.nextButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nextButton.Location = new System.Drawing.Point(476, 263);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 10;
            this.nextButton.Text = "Next >>";
            this.nextButton.UseVisualStyleBackColor = false;
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
            this.testButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.testButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("testButton.BackgroundImage")));
            this.testButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.testButton.Location = new System.Drawing.Point(359, 263);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(95, 23);
            this.testButton.TabIndex = 12;
            this.testButton.Text = "Test Connection";
            this.testButton.UseVisualStyleBackColor = false;
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
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.nextButton);
            this.Controls.Add(this.tabPanel);
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
            this.tabPanel.ResumeLayout(false);
            this.pageBasic.ResumeLayout(false);
            this.pageBasic.PerformLayout();
            this.pageAdvanced.ResumeLayout(false);
            this.placeholderForAdvanced.ResumeLayout(false);
            this.placeholderForAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.compressionUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drivesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel placeholderForAdvanced;
        private System.Windows.Forms.TabPage pageBasic;
        protected System.Windows.Forms.TabPage pageAdvanced;
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
        protected System.Windows.Forms.Label drivesListLabel;
        protected System.Windows.Forms.DataGridView drivesDataGridView;
        protected System.Windows.Forms.CheckBox selectAllCheckBox;
        protected System.Windows.Forms.ComboBox azureContainerComboBox;
        protected System.Windows.Forms.CheckBox azureDeployVirtualMachineCheckBox;
        protected System.Windows.Forms.TextBox azureSubscriptionId;
        protected System.Windows.Forms.Button azureCreateNewCertificateButton;
        protected System.Windows.Forms.Label ehCompressionLabel;
        protected System.Windows.Forms.NumericUpDown compressionUpDown;
        private System.Windows.Forms.TabControl tabPanel;
    }
}