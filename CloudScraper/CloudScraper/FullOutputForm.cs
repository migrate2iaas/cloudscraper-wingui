using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CloudScraper.Properties;

namespace CloudScraper
{
    public partial class FullOutputForm : Form
    {
        public FullOutputForm(string path)
        {
            InitializeComponent();

            this.Text = Settings.Default.FullOutputHeader;
            //using (StreamReader stream = new StreamReader(path))
            //{
                this.fullOutputBox.Lines = File.ReadAllLines(path);


            //}
        }
    }
}
