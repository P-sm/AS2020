using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DronTaxiWindowsForms
{
    public partial class Form1 : Form
    {
        /*public Form1(IConfigurationRoot configuration)
        {
            this._configuration = configuration;
            InitializeComponent();
        }*/

        
        public IConfigurationRoot _configuration;



        public Form1(IConfigurationRoot configuration)
        {
            this._configuration = configuration;
            InitializeForm();
            webBrowser1.Navigate(this._configuration.GetSection("Configuration")["url"]);
        }


        // Exits the application.
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private WebBrowser webBrowser1;   
        private void InitializeForm()
        {
            this.ClientSize = new System.Drawing.Size(1600, 1200);
            webBrowser1 = new WebBrowser();            
            webBrowser1.Dock = DockStyle.Fill;
            Controls.AddRange(new Control[] {
            webBrowser1 });
        }

    }

}

