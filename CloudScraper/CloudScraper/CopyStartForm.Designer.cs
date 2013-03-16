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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyStartForm));
            this.startButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.messageGridView = new System.Windows.Forms.DataGridView();
            this.finishButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.messageGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(480, 263);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // backButton
            // 
            this.backButton.Location = new System.Drawing.Point(12, 263);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "<< Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // messageGridView
            // 
            this.messageGridView.AllowUserToAddRows = false;
            this.messageGridView.AllowUserToDeleteRows = false;
            this.messageGridView.AllowUserToResizeColumns = false;
            this.messageGridView.AllowUserToResizeRows = false;
            this.messageGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.messageGridView.BackgroundColor = System.Drawing.Color.White;
            this.messageGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.messageGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.messageGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.messageGridView.Location = new System.Drawing.Point(13, 12);
            this.messageGridView.MultiSelect = false;
            this.messageGridView.Name = "messageGridView";
            this.messageGridView.ReadOnly = true;
            this.messageGridView.RowHeadersVisible = false;
            this.messageGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.messageGridView.ShowCellErrors = false;
            this.messageGridView.ShowCellToolTips = false;
            this.messageGridView.ShowEditingIcon = false;
            this.messageGridView.ShowRowErrors = false;
            this.messageGridView.Size = new System.Drawing.Size(542, 234);
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
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // CopyStartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 298);
            this.Controls.Add(this.finishButton);
            this.Controls.Add(this.messageGridView);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.startButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopyStartForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer progress";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.CopyStartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.messageGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.DataGridView messageGridView;
        private System.Windows.Forms.Button finishButton;
    }
}