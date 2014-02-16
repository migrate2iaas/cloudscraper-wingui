namespace CloudScraper.Azure
{
    partial class AzureAdvancedSettingsPanel
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
        private void InitializeComponent( )
        {
            this.components = new System.ComponentModel.Container();
            this.comboContainer = new System.Windows.Forms.ComboBox();
            this.tagContainer = new System.Windows.Forms.Label();
            this.chkDeployVm = new System.Windows.Forms.CheckBox();
            this.layoutTableTop = new System.Windows.Forms.TableLayoutPanel();
            this.pnlVirtualMachineSettingsHolder = new System.Windows.Forms.Panel();
            this.layoutTableBottom = new System.Windows.Forms.TableLayoutPanel();
            this.tagID = new System.Windows.Forms.Label();
            this.comboAffinity = new System.Windows.Forms.ComboBox();
            this.tagThumbprint = new System.Windows.Forms.Label();
            this.btnCreateCertificate = new System.Windows.Forms.Button();
            this.tagAffinityGroup = new System.Windows.Forms.Label();
            this.textSubscriptionId = new System.Windows.Forms.TextBox();
            this.textThumbprint = new System.Windows.Forms.TextBox();
            this.tagSubnets = new System.Windows.Forms.Label();
            this.comboSubnets = new System.Windows.Forms.ComboBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.layoutTableTop.SuspendLayout();
            this.pnlVirtualMachineSettingsHolder.SuspendLayout();
            this.layoutTableBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboContainer
            // 
            this.comboContainer.FormattingEnabled = true;
            this.comboContainer.Location = new System.Drawing.Point(171, 5);
            this.comboContainer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.comboContainer.MaxDropDownItems = 5;
            this.comboContainer.Name = "comboContainer";
            this.comboContainer.Size = new System.Drawing.Size(354, 21);
            this.comboContainer.TabIndex = 0;
            this.comboContainer.TextChanged += new System.EventHandler(this.comboContainer_TextChanged);
            // 
            // tagContainer
            // 
            this.tagContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagContainer.Location = new System.Drawing.Point(3, 0);
            this.tagContainer.Name = "tagContainer";
            this.tagContainer.Size = new System.Drawing.Size(162, 28);
            this.tagContainer.TabIndex = 24;
            this.tagContainer.Text = "Container Name:";
            this.tagContainer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDeployVm
            // 
            this.chkDeployVm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDeployVm.AutoSize = true;
            this.chkDeployVm.Location = new System.Drawing.Point(30, 31);
            this.chkDeployVm.Name = "chkDeployVm";
            this.chkDeployVm.Size = new System.Drawing.Size(135, 23);
            this.chkDeployVm.TabIndex = 1;
            this.chkDeployVm.Text = "Deploy Virtual Machine";
            this.chkDeployVm.UseVisualStyleBackColor = true;
            this.chkDeployVm.CheckedChanged += new System.EventHandler(this.azureDeployVirtualMachineCheckBox_CheckedChanged);
            // 
            // layoutTableTop
            // 
            this.layoutTableTop.ColumnCount = 2;
            this.layoutTableTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.layoutTableTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.layoutTableTop.Controls.Add(this.tagContainer, 0, 0);
            this.layoutTableTop.Controls.Add(this.comboContainer, 1, 0);
            this.layoutTableTop.Controls.Add(this.chkDeployVm, 0, 1);
            this.layoutTableTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutTableTop.Location = new System.Drawing.Point(0, 0);
            this.layoutTableTop.Name = "layoutTableTop";
            this.layoutTableTop.RowCount = 2;
            this.layoutTableTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutTableTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutTableTop.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.layoutTableTop.Size = new System.Drawing.Size(528, 57);
            this.layoutTableTop.TabIndex = 44;
            // 
            // pnlVirtualMachineSettingsHolder
            // 
            this.pnlVirtualMachineSettingsHolder.Controls.Add(this.layoutTableBottom);
            this.pnlVirtualMachineSettingsHolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVirtualMachineSettingsHolder.Location = new System.Drawing.Point(0, 57);
            this.pnlVirtualMachineSettingsHolder.Name = "pnlVirtualMachineSettingsHolder";
            this.pnlVirtualMachineSettingsHolder.Size = new System.Drawing.Size(528, 153);
            this.pnlVirtualMachineSettingsHolder.TabIndex = 45;
            // 
            // layoutTableBottom
            // 
            this.layoutTableBottom.ColumnCount = 2;
            this.layoutTableBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.layoutTableBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.layoutTableBottom.Controls.Add(this.tagID, 0, 0);
            this.layoutTableBottom.Controls.Add(this.comboAffinity, 1, 3);
            this.layoutTableBottom.Controls.Add(this.tagThumbprint, 0, 1);
            this.layoutTableBottom.Controls.Add(this.btnCreateCertificate, 1, 2);
            this.layoutTableBottom.Controls.Add(this.tagAffinityGroup, 0, 3);
            this.layoutTableBottom.Controls.Add(this.textSubscriptionId, 1, 0);
            this.layoutTableBottom.Controls.Add(this.textThumbprint, 1, 1);
            this.layoutTableBottom.Controls.Add(this.tagSubnets, 0, 4);
            this.layoutTableBottom.Controls.Add(this.comboSubnets, 1, 4);
            this.layoutTableBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutTableBottom.Location = new System.Drawing.Point(0, 0);
            this.layoutTableBottom.Name = "layoutTableBottom";
            this.layoutTableBottom.RowCount = 5;
            this.layoutTableBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutTableBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutTableBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutTableBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutTableBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.layoutTableBottom.Size = new System.Drawing.Size(528, 153);
            this.layoutTableBottom.TabIndex = 0;
            // 
            // tagID
            // 
            this.tagID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagID.Location = new System.Drawing.Point(3, 0);
            this.tagID.Name = "tagID";
            this.tagID.Size = new System.Drawing.Size(162, 30);
            this.tagID.TabIndex = 44;
            this.tagID.Text = "Subscription ID:";
            this.tagID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboAffinity
            // 
            this.comboAffinity.FormattingEnabled = true;
            this.comboAffinity.Location = new System.Drawing.Point(171, 95);
            this.comboAffinity.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.comboAffinity.MaxLength = 10;
            this.comboAffinity.Name = "comboAffinity";
            this.comboAffinity.Size = new System.Drawing.Size(354, 21);
            this.comboAffinity.TabIndex = 5;
            this.comboAffinity.TextChanged += new System.EventHandler(this.comboAffinity_TextChanged);
            // 
            // tagThumbprint
            // 
            this.tagThumbprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagThumbprint.Location = new System.Drawing.Point(3, 30);
            this.tagThumbprint.Name = "tagThumbprint";
            this.tagThumbprint.Size = new System.Drawing.Size(162, 30);
            this.tagThumbprint.TabIndex = 45;
            this.tagThumbprint.Text = "Certificate Thumbprint:";
            this.tagThumbprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCreateCertificate
            // 
            this.btnCreateCertificate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCreateCertificate.Location = new System.Drawing.Point(171, 63);
            this.btnCreateCertificate.Name = "btnCreateCertificate";
            this.btnCreateCertificate.Size = new System.Drawing.Size(209, 24);
            this.btnCreateCertificate.TabIndex = 4;
            this.btnCreateCertificate.Text = "Create and upload new certificate";
            this.btnCreateCertificate.UseVisualStyleBackColor = true;
            this.btnCreateCertificate.Click += new System.EventHandler(this.btnCreateCertificate_Click);
            // 
            // tagAffinityGroup
            // 
            this.tagAffinityGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagAffinityGroup.Location = new System.Drawing.Point(3, 90);
            this.tagAffinityGroup.Name = "tagAffinityGroup";
            this.tagAffinityGroup.Size = new System.Drawing.Size(162, 30);
            this.tagAffinityGroup.TabIndex = 43;
            this.tagAffinityGroup.Text = "Affinity Group/ Virtual Network:";
            this.tagAffinityGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textSubscriptionId
            // 
            this.textSubscriptionId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSubscriptionId.Location = new System.Drawing.Point(171, 5);
            this.textSubscriptionId.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textSubscriptionId.Name = "textSubscriptionId";
            this.textSubscriptionId.Size = new System.Drawing.Size(354, 20);
            this.textSubscriptionId.TabIndex = 2;
            this.textSubscriptionId.TextChanged += new System.EventHandler(this.textSubscriptionId_TextChanged);
            // 
            // textThumbprint
            // 
            this.textThumbprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textThumbprint.Location = new System.Drawing.Point(171, 35);
            this.textThumbprint.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.textThumbprint.MaxLength = 40;
            this.textThumbprint.Name = "textThumbprint";
            this.textThumbprint.Size = new System.Drawing.Size(354, 20);
            this.textThumbprint.TabIndex = 3;
            this.textThumbprint.TextChanged += new System.EventHandler(this.textThumbprint_TextChanged);
            // 
            // tagSubnets
            // 
            this.tagSubnets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagSubnets.Location = new System.Drawing.Point(3, 120);
            this.tagSubnets.Name = "tagSubnets";
            this.tagSubnets.Size = new System.Drawing.Size(162, 33);
            this.tagSubnets.TabIndex = 46;
            this.tagSubnets.Text = "Subnets:";
            this.tagSubnets.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboSubnets
            // 
            this.comboSubnets.FormattingEnabled = true;
            this.comboSubnets.Location = new System.Drawing.Point(171, 125);
            this.comboSubnets.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.comboSubnets.Name = "comboSubnets";
            this.comboSubnets.Size = new System.Drawing.Size(354, 21);
            this.comboSubnets.TabIndex = 6;
            this.comboSubnets.TextChanged += new System.EventHandler(this.comboSubnets_TextChanged);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 1500;
            this.toolTip.AutoPopDelay = 15000;
            this.toolTip.InitialDelay = 1500;
            this.toolTip.ReshowDelay = 600;
            // 
            // AzureAdvancedSettingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlVirtualMachineSettingsHolder);
            this.Controls.Add(this.layoutTableTop);
            this.Name = "AzureAdvancedSettingsPanel";
            this.Size = new System.Drawing.Size(528, 210);
            this.layoutTableTop.ResumeLayout(false);
            this.layoutTableTop.PerformLayout();
            this.pnlVirtualMachineSettingsHolder.ResumeLayout(false);
            this.layoutTableBottom.ResumeLayout(false);
            this.layoutTableBottom.PerformLayout();
            this.layoutTableTop.ResumeLayout(false);
            this.layoutTableTop.PerformLayout();
            this.pnlVirtualMachineSettingsHolder.ResumeLayout(false);
            this.layoutTableBottom.ResumeLayout(false);
            this.layoutTableBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboContainer;
        private System.Windows.Forms.Label tagContainer;
        private System.Windows.Forms.CheckBox chkDeployVm;
        private System.Windows.Forms.TableLayoutPanel layoutTableTop;
        private System.Windows.Forms.Panel pnlVirtualMachineSettingsHolder;
        private System.Windows.Forms.TableLayoutPanel layoutTableBottom;
        private System.Windows.Forms.Label tagID;
        private System.Windows.Forms.ComboBox comboAffinity;
        private System.Windows.Forms.Label tagThumbprint;
        private System.Windows.Forms.Button btnCreateCertificate;
        private System.Windows.Forms.TextBox textSubscriptionId;
        private System.Windows.Forms.Label tagAffinityGroup;
        private System.Windows.Forms.TextBox textThumbprint;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label tagSubnets;
        private System.Windows.Forms.ComboBox comboSubnets;

    }
}
