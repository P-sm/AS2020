using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace DronTaxiWindowsForms
{
    public partial class Form2 : Form
    {
        public Form2(string url)
        {
            //Create a new instance in code or add via the designer
            //Set the ChromiumWebBrowser.Address property to your Url if you use the designer.
            var browser = new ChromiumWebBrowser(url);
            this.Controls.Add(browser);
            InitializeComponent();
        }
    }
}
