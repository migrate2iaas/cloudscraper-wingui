using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CloudScraper.Properties;
using System.Reflection;

namespace CloudScraper
{
    public partial class ChooseCloudForm : Form
    {
        ChooseDisksForm chooseDiskForm_;
        CloudParametersForm cloudParametersForm_;
        
        public ChooseCloudForm(ChooseDisksForm chooseDiskForm)
        {
            this.chooseDiskForm_ = chooseDiskForm;

            InitializeComponent();

            //Initialize basic UI strings from settings file. 
            this.amazonButton.Image = new Bitmap(Image.FromFile("Icons\\Amazon.ico"), new Size(32, 32));
            this.windowsAzureButton.Image = new Bitmap(Image.FromFile("Icons\\Azure.ico"), new Size(32, 32));
            this.elasticHostsButton.Image = new Bitmap(Image.FromFile("Icons\\Elastic.ico"), new Size(32, 32));
            this.helpButton.Image = new Bitmap(Image.FromFile("Icons\\Help.png"), new Size(16, 16));
            this.toolTip.SetToolTip(this.helpButton, Settings.Default.HelpButtonToolTip);
            this.Text = Settings.Default.S3Header;
            this.backButton.Text = Settings.Default.S3BackButtonText;
            this.toolTip.SetToolTip(this.backButton, Settings.Default.S3BackButtonToolTip);

            //set buttons
            this.amazonButton.Text = Settings.Default.S3AmazonButtonText;
            this.toolTip.SetToolTip(this.amazonButton, Settings.Default.S3AmazonButtonToolTip);
            this.windowsAzureButton.Text = Settings.Default.S3WindowsAzureButtonText;
            this.toolTip.SetToolTip(this.windowsAzureButton, Settings.Default.S3WindowsAzureButtonToolTip);
            this.elasticHostsButton.Text = Settings.Default.S3ElasticHostsButtonText;
            this.toolTip.SetToolTip(this.elasticHostsButton, Settings.Default.S3ElasticHostsButtonToolTip);


            this.amazonButton.Visible = false;
            this.elasticHostsButton.Visible = false;
            this.windowsAzureButton.Visible = false;

            //! please, comment what is going on in here
            //! it could be quite confusing 
            foreach (string str in Settings.Default.Buttons)
            {
                string buttonName = str.Split(new char[] { Settings.Default.Separator }, 8)[0];
                string coordinats = str.Split(new char[] { Settings.Default.Separator }, 8)[1];
                string size = str.Split(new char[] { Settings.Default.Separator }, 8)[2];
                string buttonText = str.Split(new char[] { Settings.Default.Separator }, 8)[3];
                string toolTip = str.Split(new char[] { Settings.Default.Separator }, 8)[4];
                string icon = str.Split(new char[] { Settings.Default.Separator }, 8)[5];
                string iconSize = str.Split(new char[] { Settings.Default.Separator }, 8)[6];
                bool enable = Convert.ToBoolean(str.Split(new char[] { Settings.Default.Separator }, 8)[7]);

                Button button = new Button();
                button.Name = buttonName;
                button.Size =  new Size(Convert.ToInt32(size.Split(new char[] { ',' }, 2)[0]), 
                    Convert.ToInt32(size.Split(new char[] { ',' }, 2)[1]));
                button.Location = new Point(Convert.ToInt32(coordinats.Split(new char[] { ',' }, 2)[0]),
                    Convert.ToInt32(coordinats.Split(new char[] { ',' }, 2)[1]));
                button.Text = buttonText;
                this.toolTip.SetToolTip(button, toolTip);
                button.Image = new Bitmap(Image.FromFile(icon), new Size(Convert.ToInt32(iconSize.Split(new char[] { ',' }, 2)[0]),
                    Convert.ToInt32(iconSize.Split(new char[] { ',' }, 2)[1])));
                button.TabIndex = 2;
                button.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
                button.UseVisualStyleBackColor = true;
                button.Enabled = enable;
                button.Click += new System.EventHandler(this.ButtonClick);
                this.Controls.Add(button);
            }
                        
            this.logoPicture.Image = new Bitmap(Image.FromFile("Icons\\logo4a.png"));
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            this.Hide();
            this.chooseDiskForm_.StartPosition = FormStartPosition.Manual;
            this.chooseDiskForm_.Location = this.Location;
            this.chooseDiskForm_.Show();
        }

        //private void AmazonButtonClick(object sender, EventArgs e)
        //{
        //    this.Hide();

        //    if (this.cloudParametersForm_ == null || !(this.cloudParametersForm_ is AmazonCloudParameters))
        //    {
        //        this.cloudParametersForm_ = new AmazonCloudParameters(this);
        //        //this.cloudParametersForm_ = new CloudParametersForm(this);
        //    }

        //    cloudParametersForm_.ShowDialog();
        //}

        //private void ElasticHostsButtonClick(object sender, EventArgs e)
        //{
        //    this.Hide();

        //    if (this.cloudParametersForm_ == null || !(this.cloudParametersForm_ is EHCloudParameters))
        //    {
        //        //this.eHCloudParametersForm_ = new EHCloudParametersForm(this);
        //        this.cloudParametersForm_ = new EHCloudParameters(this);
        //    }

        //    this.cloudParametersForm_.ShowDialog();
        //    //eHCloudParametersForm_.ShowDialog();
        //}

        private void ButtonClick(object sender, EventArgs e)
        {
            //! some more comments, please!
            //! imagine I have no point what reflection is.
            //! And is there any way to get name of "this" assembly? What if , e.g. the version or filename changes? 
            this.Hide();
            string assemblyName = "CloudScraper." + (sender as Button).Name + 
                ", " + Assembly.GetExecutingAssembly().FullName;
            
            if (this.cloudParametersForm_ == null || !(this.cloudParametersForm_.GetType().AssemblyQualifiedName == assemblyName))
            {
                this.cloudParametersForm_ =
                    (CloudParametersForm)Activator.CreateInstance(Type.GetType(assemblyName), new Object[1] { this });
            }
                        
            this.cloudParametersForm_.ShowDialog();
        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            this.chooseDiskForm_.Close();
        }

        private void HelpButtonClick(object sender, EventArgs e)
        {
            //Start help url.
            System.Diagnostics.Process.Start(Settings.Default.S3Link);
        }

        private void ChooseCloudLoad(object sender, EventArgs e)
        {
            //For show  window in same position as prev.
            this.StartPosition = FormStartPosition.Manual;
            this.Location = this.chooseDiskForm_.Location;
        }

    }
}
