namespace CloudScraper
{
    partial class FullOutputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FullOutputForm));
            this.fullOutputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // fullOutputBox
            // 
            this.fullOutputBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fullOutputBox.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fullOutputBox.Location = new System.Drawing.Point(0, 0);
            this.fullOutputBox.Multiline = true;
            this.fullOutputBox.Name = "fullOutputBox";
            this.fullOutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.fullOutputBox.Size = new System.Drawing.Size(421, 429);
            this.fullOutputBox.TabIndex = 0;
            // 
            // FullOutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 429);
            this.Controls.Add(this.fullOutputBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FullOutputForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Full output";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fullOutputBox;

    }
}