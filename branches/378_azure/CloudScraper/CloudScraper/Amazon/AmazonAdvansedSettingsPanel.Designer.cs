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
            this.advancedCheckBox = new System.Windows.Forms.CheckBox();
            this.pnlAdvanced = new System.Windows.Forms.Panel();
            this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.serverTypeComboBox = new System.Windows.Forms.ComboBox();
            this.groupComboBox = new System.Windows.Forms.ComboBox();
            this.folderKeyBox = new System.Windows.Forms.TextBox();
            this.zoneComboBox = new System.Windows.Forms.ComboBox();
            this.folderKeyLabel = new System.Windows.Forms.Label();
            this.bucketLabel = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.zoneLabel = new System.Windows.Forms.Label();
            this.tagSelectVpc = new System.Windows.Forms.Label();
            this.bucketTextBox = new System.Windows.Forms.TextBox();
            this.groupLabel = new System.Windows.Forms.Label();
            this.tableLayoutVpc = new System.Windows.Forms.TableLayoutPanel();
            this.vpcComboBox = new System.Windows.Forms.ComboBox();
            this.subnetComboBox = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlAdvanced.SuspendLayout();
            this.tableLayout.SuspendLayout();
            this.tableLayoutVpc.SuspendLayout();
            this.SuspendLayout();
            // 
            // advancedCheckBox
            // 
            this.advancedCheckBox.AutoSize = true;
            this.advancedCheckBox.Location = new System.Drawing.Point(6, 8);
            this.advancedCheckBox.Name = "advancedCheckBox";
            this.advancedCheckBox.Size = new System.Drawing.Size(114, 17);
            this.advancedCheckBox.TabIndex = 0;
            this.advancedCheckBox.Text = "Advanced settings";
            this.advancedCheckBox.UseVisualStyleBackColor = true;
            this.advancedCheckBox.CheckedChanged += new System.EventHandler(this.advancedCheckBox_CheckedChanged);
            // 
            // pnlAdvanced
            // 
            this.pnlAdvanced.Controls.Add(this.tableLayout);
            this.pnlAdvanced.Enabled = false;
            this.pnlAdvanced.Location = new System.Drawing.Point(0, 31);
            this.pnlAdvanced.Name = "pnlAdvanced";
            this.pnlAdvanced.Size = new System.Drawing.Size(528, 179);
            this.pnlAdvanced.TabIndex = 36;
            // 
            // tableLayout
            // 
            this.tableLayout.ColumnCount = 2;
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.tableLayout.Controls.Add(this.serverTypeComboBox, 1, 2);
            this.tableLayout.Controls.Add(this.groupComboBox, 1, 4);
            this.tableLayout.Controls.Add(this.folderKeyBox, 1, 1);
            this.tableLayout.Controls.Add(this.zoneComboBox, 1, 3);
            this.tableLayout.Controls.Add(this.folderKeyLabel, 0, 1);
            this.tableLayout.Controls.Add(this.bucketLabel, 0, 0);
            this.tableLayout.Controls.Add(this.typeLabel, 0, 2);
            this.tableLayout.Controls.Add(this.zoneLabel, 0, 3);
            this.tableLayout.Controls.Add(this.tagSelectVpc, 0, 5);
            this.tableLayout.Controls.Add(this.bucketTextBox, 1, 0);
            this.tableLayout.Controls.Add(this.groupLabel, 0, 4);
            this.tableLayout.Controls.Add(this.tableLayoutVpc, 1, 5);
            this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayout.Location = new System.Drawing.Point(0, 0);
            this.tableLayout.Name = "tableLayout";
            this.tableLayout.RowCount = 6;
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayout.Size = new System.Drawing.Size(528, 179);
            this.tableLayout.TabIndex = 0;
            // 
            // serverTypeComboBox
            // 
            this.serverTypeComboBox.FormattingEnabled = true;
            this.serverTypeComboBox.Location = new System.Drawing.Point(171, 61);
            this.serverTypeComboBox.Name = "serverTypeComboBox";
            this.serverTypeComboBox.Size = new System.Drawing.Size(354, 21);
            this.serverTypeComboBox.TabIndex = 36;
            this.serverTypeComboBox.TextChanged += new System.EventHandler(this.serverTypeComboBox_TextChanged);
            // 
            // groupComboBox
            // 
            this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.groupComboBox.FormattingEnabled = true;
            this.groupComboBox.Location = new System.Drawing.Point(171, 119);
            this.groupComboBox.MaxLength = 10;
            this.groupComboBox.Name = "groupComboBox";
            this.groupComboBox.Size = new System.Drawing.Size(354, 21);
            this.groupComboBox.TabIndex = 38;
            this.groupComboBox.TextChanged += new System.EventHandler(this.groupComboBox_TextChanged);
            // 
            // folderKeyBox
            // 
            this.folderKeyBox.Location = new System.Drawing.Point(171, 32);
            this.folderKeyBox.MaxLength = 80;
            this.folderKeyBox.Name = "folderKeyBox";
            this.folderKeyBox.Size = new System.Drawing.Size(354, 20);
            this.folderKeyBox.TabIndex = 35;
            this.folderKeyBox.TextChanged += new System.EventHandler(this.folderKeyBox_TextChanged);
            // 
            // zoneComboBox
            // 
            this.zoneComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.zoneComboBox.FormattingEnabled = true;
            this.zoneComboBox.Location = new System.Drawing.Point(171, 90);
            this.zoneComboBox.MaxLength = 10;
            this.zoneComboBox.Name = "zoneComboBox";
            this.zoneComboBox.Size = new System.Drawing.Size(354, 21);
            this.zoneComboBox.TabIndex = 37;
            this.zoneComboBox.TextChanged += new System.EventHandler(this.zoneComboBox_TextChanged);
            // 
            // folderKeyLabel
            // 
            this.folderKeyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderKeyLabel.Location = new System.Drawing.Point(3, 29);
            this.folderKeyLabel.Name = "folderKeyLabel";
            this.folderKeyLabel.Size = new System.Drawing.Size(162, 29);
            this.folderKeyLabel.TabIndex = 45;
            this.folderKeyLabel.Text = "Specify folder (key):";
            this.folderKeyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bucketLabel
            // 
            this.bucketLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bucketLabel.Location = new System.Drawing.Point(3, 0);
            this.bucketLabel.Name = "bucketLabel";
            this.bucketLabel.Size = new System.Drawing.Size(162, 29);
            this.bucketLabel.TabIndex = 40;
            this.bucketLabel.Text = "Specify your S3 Bucket:";
            this.bucketLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // typeLabel
            // 
            this.typeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeLabel.Location = new System.Drawing.Point(3, 58);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(162, 29);
            this.typeLabel.TabIndex = 41;
            this.typeLabel.Text = "Specify your cloud server type:";
            this.typeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // zoneLabel
            // 
            this.zoneLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoneLabel.Location = new System.Drawing.Point(3, 87);
            this.zoneLabel.Name = "zoneLabel";
            this.zoneLabel.Size = new System.Drawing.Size(162, 29);
            this.zoneLabel.TabIndex = 42;
            this.zoneLabel.Text = "Specify your availability zone:";
            this.zoneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tagSelectVpc
            // 
            this.tagSelectVpc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagSelectVpc.Location = new System.Drawing.Point(3, 145);
            this.tagSelectVpc.Name = "tagSelectVpc";
            this.tagSelectVpc.Size = new System.Drawing.Size(162, 34);
            this.tagSelectVpc.TabIndex = 44;
            this.tagSelectVpc.Text = "Specify your VPC subnet:";
            this.tagSelectVpc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bucketTextBox
            // 
            this.bucketTextBox.Location = new System.Drawing.Point(171, 3);
            this.bucketTextBox.MaxLength = 40;
            this.bucketTextBox.Name = "bucketTextBox";
            this.bucketTextBox.Size = new System.Drawing.Size(354, 20);
            this.bucketTextBox.TabIndex = 34;
            this.bucketTextBox.TextChanged += new System.EventHandler(this.bucketTextBox_TextChanged);
            this.bucketTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.bucketTextBox_KeyPress);
            this.bucketTextBox.Leave += new System.EventHandler(this.bucketTextBox_Leave);
            // 
            // groupLabel
            // 
            this.groupLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupLabel.Location = new System.Drawing.Point(3, 116);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(162, 29);
            this.groupLabel.TabIndex = 43;
            this.groupLabel.Text = "Specify your security group:";
            this.groupLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutVpc
            // 
            this.tableLayoutVpc.ColumnCount = 2;
            this.tableLayoutVpc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutVpc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutVpc.Controls.Add(this.vpcComboBox, 0, 0);
            this.tableLayoutVpc.Controls.Add(this.subnetComboBox, 1, 0);
            this.tableLayoutVpc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutVpc.Location = new System.Drawing.Point(168, 145);
            this.tableLayoutVpc.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutVpc.Name = "tableLayoutVpc";
            this.tableLayoutVpc.RowCount = 1;
            this.tableLayoutVpc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutVpc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutVpc.Size = new System.Drawing.Size(360, 34);
            this.tableLayoutVpc.TabIndex = 46;
            // 
            // vpcComboBox
            // 
            this.vpcComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.vpcComboBox.FormattingEnabled = true;
            this.vpcComboBox.Location = new System.Drawing.Point(3, 3);
            this.vpcComboBox.Name = "vpcComboBox";
            this.vpcComboBox.Size = new System.Drawing.Size(174, 21);
            this.vpcComboBox.TabIndex = 39;
            this.vpcComboBox.TextChanged += new System.EventHandler(this.vpcComboBox_TextChanged);
            // 
            // subnetComboBox
            // 
            this.subnetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.subnetComboBox.Enabled = false;
            this.subnetComboBox.FormattingEnabled = true;
            this.subnetComboBox.Location = new System.Drawing.Point(183, 3);
            this.subnetComboBox.Name = "subnetComboBox";
            this.subnetComboBox.Size = new System.Drawing.Size(174, 21);
            this.subnetComboBox.TabIndex = 40;
            this.subnetComboBox.TextChanged += new System.EventHandler(this.subnetComboBox_TextChanged);
            // 
            // AmazonAdvansedSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.advancedCheckBox);
            this.Controls.Add(this.pnlAdvanced);
            this.Name = "AmazonAdvansedSettingsPanel";
            this.Size = new System.Drawing.Size(528, 210);
            this.pnlAdvanced.ResumeLayout(false);
            this.tableLayout.ResumeLayout(false);
            this.tableLayout.PerformLayout();
            this.tableLayoutVpc.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlAdvanced;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox advancedCheckBox;
        private System.Windows.Forms.TableLayoutPanel tableLayout;
        private System.Windows.Forms.ComboBox serverTypeComboBox;
        private System.Windows.Forms.ComboBox groupComboBox;
        private System.Windows.Forms.TextBox folderKeyBox;
        private System.Windows.Forms.ComboBox zoneComboBox;
        private System.Windows.Forms.Label folderKeyLabel;
        private System.Windows.Forms.Label bucketLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.Label zoneLabel;
        private System.Windows.Forms.Label tagSelectVpc;
        private System.Windows.Forms.TextBox bucketTextBox;
        private System.Windows.Forms.Label groupLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutVpc;
        private System.Windows.Forms.ComboBox vpcComboBox;
        private System.Windows.Forms.ComboBox subnetComboBox;
    }
}
