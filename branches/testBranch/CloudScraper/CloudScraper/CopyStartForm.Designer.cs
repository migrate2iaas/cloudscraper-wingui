namespace CloudScraper
{
    partial class CopyStartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyStartForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.startButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.messageGridView = new System.Windows.Forms.DataGridView();
            this.finishButton = new System.Windows.Forms.Button();
            this.fullOutputButton = new System.Windows.Forms.Button();
            this.helpButton = new System.Windows.Forms.Button();
            this.mailButton = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.inProgressPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.messageGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inProgressPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.startButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("startButton.BackgroundImage")));
            this.startButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.startButton.Location = new System.Drawing.Point(480, 263);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.backButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("backButton.BackgroundImage")));
            this.backButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backButton.Location = new System.Drawing.Point(12, 263);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // messageGridView
            // 
            this.messageGridView.AllowUserToAddRows = false;
            this.messageGridView.AllowUserToDeleteRows = false;
            this.messageGridView.AllowUserToResizeColumns = false;
            this.messageGridView.AllowUserToResizeRows = false;
            this.messageGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.messageGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.messageGridView.BackgroundColor = System.Drawing.Color.White;
            this.messageGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.messageGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.messageGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.messageGridView.Location = new System.Drawing.Point(13, 29);
            this.messageGridView.MultiSelect = false;
            this.messageGridView.Name = "messageGridView";
            this.messageGridView.ReadOnly = true;
            this.messageGridView.RowHeadersVisible = false;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.messageGridView.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.messageGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.messageGridView.ShowCellErrors = false;
            this.messageGridView.ShowCellToolTips = false;
            this.messageGridView.ShowEditingIcon = false;
            this.messageGridView.ShowRowErrors = false;
            this.messageGridView.Size = new System.Drawing.Size(542, 217);
            this.messageGridView.TabIndex = 3;
            this.messageGridView.VirtualMode = true;
            // 
            // finishButton
            // 
            this.finishButton.Location = new System.Drawing.Point(480, 263);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(75, 23);
            this.finishButton.TabIndex = 4;
            this.finishButton.Text = "Finish";
            this.finishButton.UseVisualStyleBackColor = true;
            this.finishButton.Visible = false;
            this.finishButton.Click += new System.EventHandler(this.FinishButtonClick);
            // 
            // fullOutputButton
            // 
            this.fullOutputButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.fullOutputButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fullOutputButton.BackgroundImage")));
            this.fullOutputButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fullOutputButton.Location = new System.Drawing.Point(293, 263);
            this.fullOutputButton.Name = "fullOutputButton";
            this.fullOutputButton.Size = new System.Drawing.Size(102, 23);
            this.fullOutputButton.TabIndex = 5;
            this.fullOutputButton.Text = "Show full output";
            this.fullOutputButton.UseVisualStyleBackColor = false;
            this.fullOutputButton.Visible = false;
            this.fullOutputButton.Click += new System.EventHandler(this.FullOutputButtonClick);
            // 
            // helpButton
            // 
            this.helpButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.helpButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.helpButton.FlatAppearance.BorderSize = 0;
            this.helpButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.helpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.helpButton.ForeColor = System.Drawing.Color.Transparent;
            this.helpButton.Location = new System.Drawing.Point(544, 1);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(23, 22);
            this.helpButton.TabIndex = 20;
            this.helpButton.Tag = "Help";
            this.helpButton.UseVisualStyleBackColor = false;
            this.helpButton.Click += new System.EventHandler(this.HelpButtonClick);
            // 
            // mailButton
            // 
            this.mailButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mailButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mailButton.BackgroundImage")));
            this.mailButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mailButton.Location = new System.Drawing.Point(160, 263);
            this.mailButton.Name = "mailButton";
            this.mailButton.Size = new System.Drawing.Size(127, 23);
            this.mailButton.TabIndex = 21;
            this.mailButton.Text = "Send Report to Support";
            this.mailButton.UseVisualStyleBackColor = false;
            this.mailButton.Visible = false;
            this.mailButton.Click += new System.EventHandler(this.MailButtonClick);
            // 
            // inProgressPictureBox
            // 
            this.inProgressPictureBox.Enabled = false;
            this.inProgressPictureBox.Image = global::CloudScraper.Properties.Resources.ajax_loader_1_;
            this.inProgressPictureBox.Location = new System.Drawing.Point(411, 263);
            this.inProgressPictureBox.Name = "inProgressPictureBox";
            this.inProgressPictureBox.Size = new System.Drawing.Size(53, 23);
            this.inProgressPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.inProgressPictureBox.TabIndex = 22;
            this.inProgressPictureBox.TabStop = false;
            // 
            // CopyStartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.inProgressPictureBox);
            this.Controls.Add(this.mailButton);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.fullOutputButton);
            this.Controls.Add(this.messageGridView);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.finishButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopyStartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer progress";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.CopyStartFormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CopyStartFormKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.messageGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inProgressPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.DataGridView messageGridView;
        private System.Windows.Forms.Button finishButton;
        private System.Windows.Forms.Button fullOutputButton;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Button mailButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox inProgressPictureBox;
    }
}