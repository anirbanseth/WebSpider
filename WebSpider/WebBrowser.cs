using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebSpider
{
    public partial class WebBrowser : Form
    {
        public WebBrowser()
        {
            InitializeComponent();
        }

        private void WebBrowser_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region [Login Test]
            
           
            oIE.Navigate("https://adiglobal.us/Pages/WebRegistration.aspx");
            oIE.AllowNavigation = true;
            while (oIE.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }

            if (oIE.ReadyState == WebBrowserReadyState.Complete)
            {
                if (oIE.Document.GetElementById("ctl00_PlaceHolderMain_ctl00_ctlLoginView_MainLoginView_MainLogin_UserName").GetAttribute("Value") == "")
                {
                    oIE.Document.GetElementById("ctl00_PlaceHolderMain_ctl00_ctlLoginView_MainLoginView_MainLogin_UserName").SetAttribute("Value", "PHIL0691");
                    oIE.Document.GetElementById("ctl00_PlaceHolderMain_ctl00_ctlLoginView_MainLoginView_MainLogin_Password").SetAttribute("Value", "ABC123");
                    oIE.Document.GetElementById("ctl00_PlaceHolderMain_ctl00_ctlLoginView_MainLoginView_MainLogin_LoginButton").InvokeMember("Click");


                }

            }
            #endregion
        }
    }
}
