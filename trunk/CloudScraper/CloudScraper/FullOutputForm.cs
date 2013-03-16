using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CloudScraper
{
    public partial class FullOutputForm : Form
    {
        public FullOutputForm(string path)
        {
            InitializeComponent();

            //using (StreamReader stream = new StreamReader(path))
            //{
                this.fullOutputBox.Lines = File.ReadAllLines(path);

            //}
        }
    }
}
