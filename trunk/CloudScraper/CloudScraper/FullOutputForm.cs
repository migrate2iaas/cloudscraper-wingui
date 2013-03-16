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

            using (StreamReader stream = new StreamReader(path))
            {
                while (!stream.EndOfStream)
                {
                    this.fullOutputBox.Items.Add(stream.ReadLine());
                }
            }
        }
    }
}
