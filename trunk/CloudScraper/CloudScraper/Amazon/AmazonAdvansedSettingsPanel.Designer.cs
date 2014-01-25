namespace CloudScraper
{
    partial class AmazonAdvansedSettingsPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
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
            this.pnlAdvanced = new System.Windows.Forms.Panel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.vpcComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlAdvanced.SuspendLayout();
            this.SuspendLayout();
            // 
            // folderKeyLabel
            // 
            this.folderKeyLabel.Location = new System.Drawing.Point(0, 32);
            this.folderKeyLabel.Name = "folderKeyLabel";
            this.folderKeyLabel.Size = new System.Drawing.Size(213, 20);
            this.folderKeyLabel.TabIndex = 33;
            this.folderKeyLabel.Text = "Specify folder (key):";
            this.folderKeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // folderKeyBox
            // 
            this.folderKeyBox.Location = new System.Drawing.Point(216, 32);
            this.folderKeyBox.MaxLength = 80;
            this.folderKeyBox.Name = "folderKeyBox";
            this.folderKeyBox.Size = new System.Drawing.Size(237, 20);
            this.folderKeyBox.TabIndex = 32;
            this.folderKeyBox.TextChanged += new System.EventHandler(this.folderKeyBox_TextChanged);
            this.folderKeyBox.Leave += new System.EventHandler(this.folderKeyBox_Leave);
            this.folderKeyBox.MouseHover += new System.EventHandler(this.folderKeyBox_MouseHover);
            // 
            // advancedCheckBox
            // 
            this.advancedCheckBox.AutoSize = true;
            this.advancedCheckBox.Location = new System.Drawing.Point(90, 8);
            this.advancedCheckBox.Name = "advancedCheckBox";
            this.advancedCheckBox.Size = new System.Drawing.Size(114, 17);
            this.advancedCheckBox.TabIndex = 31;
            this.advancedCheckBox.Text = "Advanced settings";
            this.advancedCheckBox.UseVisualStyleBackColor = true;
            this.advancedCheckBox.CheckedChanged += new System.EventHandler(this.advancedCheckBox_CheckedChanged);
            this.advancedCheckBox.Leave += new System.EventHandler(this.advancedCheckBox_Leave);
            // 
            // groupLabel
            // 
            this.groupLabel.Location = new System.Drawing.Point(3, 112);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(213, 21);
            this.groupLabel.TabIndex = 30;
            this.groupLabel.Text = "Specify your security group:";
            this.groupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // zoneLabel
            // 
            this.zoneLabel.Location = new System.Drawing.Point(0, 85);
            this.zoneLabel.Name = "zoneLabel";
            this.zoneLabel.Size = new System.Drawing.Size(216, 21);
            this.zoneLabel.TabIndex = 29;
            this.zoneLabel.Text = "Specify your availability zone:";
            this.zoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typeLabel
            // 
            this.typeLabel.Location = new System.Drawing.Point(0, 58);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(216, 21);
            this.typeLabel.TabIndex = 26;
            this.typeLabel.Text = "Specify your cloud server type:";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bucketTextBox
            // 
            this.bucketTextBox.Location = new System.Drawing.Point(216, 6);
            this.bucketTextBox.MaxLength = 40;
            this.bucketTextBox.Name = "bucketTextBox";
            this.bucketTextBox.Size = new System.Drawing.Size(237, 20);
            this.bucketTextBox.TabIndex = 25;
            this.bucketTextBox.TextChanged += new System.EventHandler(this.bucketTextBox_TextChanged);
            this.bucketTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bucketTextBox_KeyPress);
            this.bucketTextBox.Leave += new System.EventHandler(this.bucketTextBox_Leave);
            this.bucketTextBox.MouseHover += new System.EventHandler(this.bucketTextBox_MouseHover);
            // 
            // bucketLabel
            // 
            this.bucketLabel.Location = new System.Drawing.Point(0, 6);
            this.bucketLabel.Name = "bucketLabel";
            this.bucketLabel.Size = new System.Drawing.Size(213, 20);
            this.bucketLabel.TabIndex = 24;
            this.bucketLabel.Text = "Specify your S3 Bucket:";
            this.bucketLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // zoneComboBox
            // 
            this.zoneComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.zoneComboBox.FormattingEnabled = true;
            this.zoneComboBox.Location = new System.Drawing.Point(216, 85);
            this.zoneComboBox.MaxLength = 10;
            this.zoneComboBox.Name = "zoneComboBox";
            this.zoneComboBox.Size = new System.Drawing.Size(237, 21);
            this.zoneComboBox.TabIndex = 34;
            this.zoneComboBox.SelectedIndexChanged += new System.EventHandler(this.zoneComboBox_SelectedIndexChanged);
            this.zoneComboBox.TextChanged += new System.EventHandler(this.zoneComboBox_TextChanged);
            this.zoneComboBox.Leave += new System.EventHandler(this.zoneComboBox_Leave);
            this.zoneComboBox.MouseEnter += new System.EventHandler(this.zoneComboBox_MouseEnter);
            this.zoneComboBox.MouseHover += new System.EventHandler(this.zoneComboBox_MouseHover);
            // 
            // groupComboBox
            // 
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(216, 112);
            this.groupComboBox.MaxLength = 10;
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(237, 21);
            this.groupComboBox.TabIndex = 35;
            this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.groupComboBox_SelectedIndexChanged);
            this.groupComboBox.TextChanged += new System.EventHandler(this.groupComboBox_TextChanged);
            this.groupComboBox.Leave += new System.EventHandler(this.groupComboBox_Leave);
            this.groupComboBox.MouseEnter += new System.EventHandler(this.groupComboBox_MouseEnter);
            this.groupComboBox.MouseHover += new System.EventHandler(this.groupComboBox_MouseHover);
            // 
            // serverTypeComboBox
            // 
            this.serverTypeComboBox.FormattingEnabled = true;
            this.serverTypeComboBox.Location = new System.Drawing.Point(216, 58);
            this.serverTypeComboBox.Name = "serverTypeComboBox";
            this.serverTypeComboBox.Size = new System.Drawing.Size(237, 21);
            this.serverTypeComboBox.TabIndex = 27;
            this.serverTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.serverTypeComboBox_SelectedIndexChanged);
            this.serverTypeComboBox.Leave += new System.EventHandler(this.serverTypeComboBox_Leave);
            this.serverTypeComboBox.MouseEnter += new System.EventHandler(this.serverTypeComboBox_MouseEnter);
            this.serverTypeComboBox.MouseHover += new System.EventHandler(this.serverTypeComboBox_MouseHover);
            // 
            // pnlAdvanced
            // 
            this.pnlAdvanced.Controls.Add(this.folderKeyLabel);
            this.pnlAdvanced.Controls.Add(this.bucketLabel);
            this.pnlAdvanced.Controls.Add(this.folderKeyBox);
            this.pnlAdvanced.Controls.Add(this.serverTypeComboBox);
            this.pnlAdvanced.Controls.Add(this.vpcComboBox);
            this.pnlAdvanced.Controls.Add(this.groupComboBox);
            this.pnlAdvanced.Controls.Add(this.label1);
            this.pnlAdvanced.Controls.Add(this.groupLabel);
            this.pnlAdvanced.Controls.Add(this.zoneComboBox);
            this.pnlAdvanced.Controls.Add(this.zoneLabel);
            this.pnlAdvanced.Controls.Add(this.bucketTextBox);
            this.pnlAdvanced.Controls.Add(this.typeLabel);
            this.pnlAdvanced.Enabled = false;
            this.pnlAdvanced.Location = new System.Drawing.Point(0, 31);
            this.pnlAdvanced.Name = "pnlAdvanced";
            this.pnlAdvanced.Size = new System.Drawing.Size(534, 185);
            this.pnlAdvanced.TabIndex = 36;
            // 
            // vpcComboBox
            // 
            this.vpcComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.vpcComboBox.FormattingEnabled = true;
            this.vpcComboBox.Location = new System.Drawing.Point(216, 139);
            this.vpcComboBox.MaxLength = 10;
            this.vpcComboBox.Name = "vpcComboBox";
            this.vpcComboBox.Size = new System.Drawing.Size(237, 21);
            this.vpcComboBox.TabIndex = 35;
            this.vpcComboBox.SelectedIndexChanged += new System.EventHandler(this.vpcComboBox_SelectedIndexChanged);
            this.vpcComboBox.TextChanged += new System.EventHandler(this.vpcComboBox_TextChanged);
            this.vpcComboBox.Leave += new System.EventHandler(this.vpcComboBox_Leave);
            this.vpcComboBox.MouseEnter += new System.EventHandler(this.vpcComboBox_MouseEnter);
            this.vpcComboBox.MouseHover += new System.EventHandler(this.vpcComboBox_MouseHover);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 21);
            this.label1.TabIndex = 30;
            this.label1.Text = "Specify your VPC subnet:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AmazonAdvansedSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.advancedCheckBox);
            this.Controls.Add(this.pnlAdvanced);
            this.Name = "AmazonAdvansedSettingsPanel";
            this.Size = new System.Drawing.Size(534, 216);
            this.pnlAdvanced.ResumeLayout(false);
            this.pnlAdvanced.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlAdvanced;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label folderKeyLabel;
        private System.Windows.Forms.TextBox folderKeyBox;
        private System.Windows.Forms.Label groupLabel;
        private System.Windows.Forms.Label zoneLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.TextBox bucketTextBox;
        private System.Windows.Forms.Label bucketLabel;
        private System.Windows.Forms.ComboBox zoneComboBox;
        private System.Windows.Forms.ComboBox groupComboBox;
        private System.Windows.Forms.ComboBox serverTypeComboBox;
        private System.Windows.Forms.CheckBox advancedCheckBox;
        private System.Windows.Forms.ComboBox vpcComboBox;
        private System.Windows.Forms.Label label1;
    }
}
