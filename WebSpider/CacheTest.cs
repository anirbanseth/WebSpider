using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSpider.Core;

namespace WebSpider
{
    public partial class CacheTest : Form
    {
        Browser browser;

        public CacheTest()
        {
            InitializeComponent();
            browser = new Browser();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            String Url = txtUrl.Text;
            browser.Url = Url;
            Log(browser.GetWebRequest().ToString());
        }

        private void Log(String text)
        {
            txtLog.Text = txtLog.Text + "\n\r" + text;
        }
    }
}
