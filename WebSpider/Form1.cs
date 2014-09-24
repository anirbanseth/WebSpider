using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using WebSpider.Core;
using System.Collections;
using System.Net.Sockets;
using System.IO;
using System.Collections.Specialized;
using System.Web;
using WebSpider.WebSpiderDbTableAdapters;

namespace WebSpider
{
    public partial class Form1 : Form
    {
        // unique Uri's queue
        private Queue queueURLS = new Queue();
        Browser browser = new Browser();

        public Form1()
        {
            InitializeComponent();
        }

        // List of a user defined list of restricted hosts extensions to avoid blocking by these hosts
        private string[] strExcludeHosts;
        private string[] ExcludeHosts
        {
            get { return strExcludeHosts; }
            set { strExcludeHosts = value; }
        }
        // download folder
        private string strDownloadfolder;
        private string Downloadfolder
        {
            get { return strDownloadfolder; }
            set
            {
                strDownloadfolder = value;
                strDownloadfolder = strDownloadfolder.TrimEnd('\\');
            }
        }
        private void buttonGo_Click(object sender, EventArgs e)
        {
            

            #region [Load Data]
            //MyUri uri = new MyUri("http://adiglobal.us/Manufacturer%20Logos/Standard/08273.jpg");
            //this.EnqueueUri(uri, false);
            //uri = DequeueUri();
            //MyWebRequest request = null;
            //// create web request
            //request = MyWebRequest.Create(uri, request, true);
            //// set request timeout
            //request.Timeout = 20 * 1000;
            //// retrieve response from web request
            //MyWebResponse response = request.GetResponse();

            ////// construct the full path folder
            ////string BasePath = this.Downloadfolder + "\\" + uri.Host + Path.GetDirectoryName(uri.AbsolutePath);
            ////// check if the folder not found
            ////if (Directory.Exists(BasePath) == false)
            ////    // create the folder
            ////    Directory.CreateDirectory(BasePath);
            //// construct the full path name of the file
            //////string PathName = this.Downloadfolder + "\\" + uri.Host ;
            //string PathName = "D:" + "\\" + uri.Host + "\\" + "ab.jpg";
            //// open the output file
            //FileStream streamOut = File.Open(PathName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            //BinaryWriter writer = new BinaryWriter(streamOut);
            //// receive response buffer
            //string strResponse = "";
            //byte[] RecvBuffer = new byte[10240];
            //int nBytes, nTotalBytes = 0;
            //while ((nBytes = response.socket.Receive(RecvBuffer, 0, 10240, SocketFlags.None)) > 0)
            //{
            //    // increment total received bytes
            //    nTotalBytes += nBytes;
            //    writer.Write(RecvBuffer, 0, nBytes);
            //    strResponse += Encoding.ASCII.GetString(RecvBuffer, 0, nBytes);
            //    // check if connection Keep-Alive to can break the loop if response completed
            //    break;
            //}
            //// close output stream
            //writer.Close();
            //streamOut.Close();
            #endregion

            #region [Web Client Login]
            //string URLAuth = "https://adiglobal.us/Pages/WebRegistration.aspx";           
            //WebClient webClient = new WebClient();           
            //webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");            
            //NameValueCollection formData = new NameValueCollection();
            //formData["MSO_PageHashCode"] = "2691463195435";
            //formData["__SPSCEditMenu"] = "true";
            //formData["MSOTlPn_View"] = "0";
            //formData["MSOTlPn_ShowSettings"] = "false";
            //formData["MSOTlPn_Button"] = "none";
            //formData["MSOSPWebPartManager_DisplayModeName"] = "Browse";
           
            //formData["__EVENTTARGET"] = "ctl00$PlaceHolderMain$ctl00$ctlLoginView$MainLoginView$MainLogin$LoginButton";
            //formData["__EVENTARGUMENT"] = "";
            //formData["__VIEWSTATE"] = "/wEPDwUBMA9kFgJmD2QWBAIBDxYCHhBkYXRhLWlzLXBvc3RiYWNrBQExFgQCBA9kFgJmD2QWAgIBDxYCHhNQcmV2aW91c0NvbnRyb2xNb2RlCymIAU1pY3Jvc29mdC5TaGFyZVBvaW50LldlYkNvbnRyb2xzLlNQQ29udHJvbE1vZGUsIE1pY3Jvc29mdC5TaGFyZVBvaW50LCBWZXJzaW9uPTEyLjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTcxZTliY2UxMTFlOTQyOWMBZAIID2QWAmYPZBYCZg9kFgRmDxUEAjE1ATIUaHR0cHM6Ly9hZGlnbG9iYWwudXMUaHR0cHM6Ly9hZGlnbG9iYWwudXNkAgIPZBYCZg9kFgRmDxUWGGh0dHBzOi8vY2RuLmFkaWdsb2JhbC51cwgyMDE0MDkxMxhodHRwczovL2Nkbi5hZGlnbG9iYWwudXMIMjAxNDA5MTMYaHR0cHM6Ly9jZG4uYWRpZ2xvYmFsLnVzCDIwMTQwOTEzGGh0dHBzOi8vY2RuLmFkaWdsb2JhbC51cwgyMDE0MDkxMxhodHRwczovL2Nkbi5hZGlnbG9iYWwudXMIMjAxNDA5MTMYaHR0cHM6Ly9jZG4uYWRpZ2xvYmFsLnVzCDIwMTQwOTEzGGh0dHBzOi8vY2RuLmFkaWdsb2JhbC51cwgyMDE0MDkxMxhodHRwczovL2Nkbi5hZGlnbG9iYWwudXMIMjAxNDA5MTMYaHR0cHM6Ly9jZG4uYWRpZ2xvYmFsLnVzCDIwMTQwOTEzGGh0dHBzOi8vY2RuLmFkaWdsb2JhbC51cwgyMDE0MDkxMxhodHRwczovL2Nkbi5hZGlnbG9iYWwudXMIMjAxNDA5MTNkAgIPFQIYaHR0cHM6Ly9jZG4uYWRpZ2xvYmFsLnVzCDIwMTQwOTEzZAIDD2QWCgICD2QWAgIBD2QWAmYPZBYCAgEPZBYCAgEPZBYGAgEPZBYCAgEPFgIeB1Zpc2libGVoFgJmD2QWBAICD2QWAgIDDxYCHwJoZAIDDw8WAh4JQWNjZXNzS2V5BQEvZGQCAw9kFgICAQ9kFgICAg8PFgIfAmcWAh4Fc3R5bGUFDmRpc3BsYXk6YmxvY2s7ZAIFD2QWAgIBDw8WAh8CaGQWBAIBDw8WAh8CaGRkAgMPDxYCHwJoZBYCAgEPDxYCHwJnZBYEAgEPDxYCHwJoZBYcAgEPDxYCHwJoZGQCAw8WAh8CaGQCBQ8PFgIfAmhkZAIHDxYCHwJoZAIJDw8WAh8CaGRkAgsPDxYCHwJoZGQCDQ8PFgIfAmhkZAIPDw8WBB4HRW5hYmxlZGgfAmhkZAIRDw8WAh8CaGRkAhMPDxYEHwVoHwJoZGQCFQ8PFgIfAmhkZAIXDxYCHwJoZAIZDxYCHwJoZAIbDw8WAh8CZ2RkAgMPDxYCHwJnZBYGAgEPDxYCHwJnZGQCAw8PFgIfAmdkZAIFDw8WAh8CZ2RkAgQPZBYCAgEPZBYIAgMPZBYEZg8WAh4EVGV4dAVSZG9jdW1lbnQuZm9ybXNbMF0uYWN0aW9uID0gZG9jdW1lbnQuZm9ybXNbMF0uYWN0aW9uLnJlcGxhY2UoJ2h0dHA6Ly8nLCdodHRwczovLycpO2QCAg9kFgJmD2QWAgIBD2QWAmYPZBYCAgUPZBYCAhUPEA8WAh4HQ2hlY2tlZGhkZGRkAgsPZBYCZg9kFgJmDzwrAAkAZAITDxYCHgtfIUl0ZW1Db3VudAILFhYCAQ9kFgJmDxUCBDYwMDAGQWNjZXNzZAICD2QWAmYPFQIENTAwMA1BdWRpbyAmIFZpZGVvZAIDD2QWAmYPFQIENTgwMA5DZW50cmFsIFZhY3V1bWQCBA9kFgJmDxUCBDIwMDAERmlyZWQCBQ9kFgJmDxUCBDEwMDAJSW50cnVzaW9uZAIGD2QWAmYPFQIEODAwMA9OZXR3b3JrIFN5c3RlbXNkAgcPZBYCZg8VAgQxNzAwBVBvd2VyZAIID2QWAmYPFQIEOTAwMAlUZWxlcGhvbnlkAgkPZBYCZg8VAgQ3MDAwEFRvb2xzICYgSGFyZHdhcmVkAgoPZBYCZg8VAgQzMDAwElZpZGVvIFN1cnZlaWxsYW5jZWQCCw9kFgJmDxUCBDQwMDAMV2lyZSAmIENhYmxlZAIbD2QWAmYPZBYCZg9kFgICAQ8WAh8GBQ9XZWxjb21lIHRvIEFESSFkAgYPZBYCAgEPZBYCAgQPZBYCZg9kFgQCAw8PFgIeC05hdmlnYXRlVXJsBUovQ29tcGFueS9QYWdlcy9Na3RnX1Nob3BQcm9kdWN0cy5hc3B4P2NhdD1BREkgVVMmY2F0ZWdvcnk9MDAwMCZwYXJlbnQ9MDAwMGRkAgUPFgIfCAILFhYCAQ9kFgICAQ8PFgYfCQVEL0NvbXBhbnkvUGFnZXMvTWt0Z19BY2Nlc3MuYXNweD9jYXQ9QURJIFVTJmNhdGVnb3J5PTYwMDAmcGFyZW50PTAwMDAfBgUGQWNjZXNzHgdUb29sVGlwBQZBY2Nlc3NkZAICD2QWAgIBDw8WBh8JBUovQ29tcGFueS9QYWdlcy9Na3RnX0F1ZGlvVmlkZW8uYXNweD9jYXQ9QURJJTIwVVMmY2F0ZWdvcnk9NTAwMCZwYXJlbnQ9MDAwMB8GBQ1BdWRpbyAmIFZpZGVvHwoFDUF1ZGlvICYgVmlkZW9kZAIDD2QWAgIBDw8WBh8JBUsvQ29tcGFueS9QYWdlcy9Na3RnX0NlbnRyYWxWYWN1dW0uYXNweD9jYXQ9QURJIFVTJmNhdGVnb3J5PTU4MDAmcGFyZW50PTAwMDAfBgUOQ2VudHJhbCBWYWN1dW0fCgUOQ2VudHJhbCBWYWN1dW1kZAIED2QWAgIBDw8WBh8JBUIvQ29tcGFueS9QYWdlcy9Na3RnX0ZpcmUuYXNweD9jYXQ9QURJIFVTJmNhdGVnb3J5PTIwMDAmcGFyZW50PTAwMDAfBgUERmlyZR8KBQRGaXJlZGQCBQ9kFgICAQ8PFgYfCQVHL0NvbXBhbnkvUGFnZXMvTWt0Z19JbnRydXNpb24uYXNweD9jYXQ9QURJIFVTJmNhdGVnb3J5PTEwMDAmcGFyZW50PTAwMDAfBgUJSW50cnVzaW9uHwoFCUludHJ1c2lvbmRkAgYPZBYCAgEPDxYGHwkFTi9Db21wYW55L1BhZ2VzL01rdGdfTmV0d29ya1N5c3RlbXMuYXNweD9jYXQ9QURJJTIwVVMmY2F0ZWdvcnk9ODAwMCZwYXJlbnQ9MDAwMB8GBQ9OZXR3b3JrIFN5c3RlbXMfCgUPTmV0d29yayBTeXN0ZW1zZGQCBw9kFgICAQ8PFgYfCQVDL0NvbXBhbnkvUGFnZXMvTWt0Z19Qb3dlci5hc3B4P2NhdD1BREkgVVMmY2F0ZWdvcnk9MTcwMCZwYXJlbnQ9MDAwMB8GBQVQb3dlch8KBQVQb3dlcmRkAggPZBYCAgEPDxYGHwkFSS9Db21wYW55L1BhZ2VzL01rdGdfVGVsZXBob255LmFzcHg/Y2F0PUFESSUyMFVTJmNhdGVnb3J5PTkwMDAmcGFyZW50PTAwMDAfBgUJVGVsZXBob255HwoFCVRlbGVwaG9ueWRkAgkPZBYCAgEPDxYGHwkFSy9Db21wYW55L1BhZ2VzL01rdGdfVG9vbHNIYXJkd2FyZS5hc3B4P2NhdD1BREkgVVMmY2F0ZWdvcnk9NzAwMCZwYXJlbnQ9MDAwMB8GBRBUb29scyAmIEhhcmR3YXJlHwoFEFRvb2xzICYgSGFyZHdhcmVkZAIKD2QWAgIBDw8WBh8JBU8vQ29tcGFueS9QYWdlcy9Na3RnX1ZpZGVvU3VydmVpbGxhbmNlLmFzcHg/Y2F0PUFESSBVUyZjYXRlZ29yeT0zMDAwJnBhcmVudD0wMDAwHwYFElZpZGVvIFN1cnZlaWxsYW5jZR8KBRJWaWRlbyBTdXJ2ZWlsbGFuY2VkZAILD2QWAgIBDw8WBh8JBUcvQ29tcGFueS9QYWdlcy9Na3RnX1dpcmVDYWJsZS5hc3B4P2NhdD1BREkgVVMmY2F0ZWdvcnk9NDAwMCZwYXJlbnQ9MDAwMB8GBQxXaXJlICYgQ2FibGUfCgUMV2lyZSAmIENhYmxlZGQCCg9kFgICAQ9kFgICAw88KwAFAQAPFgIeD1NpdGVNYXBQcm92aWRlcgUVQURJWG1sU2l0ZU1hcFByb3ZpZGVyZGQCDA9kFgICAw9kFgICAg9kFgYCBg9kFgQCAQ8WAh8GBVJkb2N1bWVudC5mb3Jtc1swXS5hY3Rpb24gPSBkb2N1bWVudC5mb3Jtc1swXS5hY3Rpb24ucmVwbGFjZSgnaHR0cDovLycsJ2h0dHBzOi8vJyk7ZAIDD2QWAmYPZBYCAgEPZBYCZg9kFgICAQ9kFgICDw8QDxYCHwdoZGRkZAIID2QWKgIDDw8WAh8GBS4qIFBsZWFzZSBlbnRlciBpbmZvcm1hdGlvbiBpbiByZXF1aXJlZCBmaWVsZHMuFgIfBAUOZGlzcGxheTpibG9jaztkAgUPDxYEHgxFcnJvck1lc3NhZ2UFJCogUGxlYXNlIGVudGVyIGEgdmFsaWQgZW1haWwgYWRkcmVzcx8GBSQqIFBsZWFzZSBlbnRlciBhIHZhbGlkIGVtYWlsIGFkZHJlc3NkZAILDw8WBB8MBRtBREkgQWNjb3VudCBOdW1iZXIgUmVxdWlyZWQfBgUbQURJIEFjY291bnQgTnVtYmVyIFJlcXVpcmVkZGQCDQ8PFgQfDAUkUGxlYXNlIGVudGVyIGEgdmFsaWQgY3VzdG9tZXIgbnVtYmVyHwYFJFBsZWFzZSBlbnRlciBhIHZhbGlkIGN1c3RvbWVyIG51bWJlcmRkAg8PZBYCAgUPDxYEHwwFG1BsZWFzZSBlbnRlciBhIHZhbGlkIHN1ZmZpeB8GBRtQbGVhc2UgZW50ZXIgYSB2YWxpZCBzdWZmaXhkZAIVDw8WBB8MBQ5FbWFpbCBSZXF1aXJlZB8GBQ5FbWFpbCBSZXF1aXJlZGRkAhsPDxYEHwwFE0ZpcnN0IE5hbWUgUmVxdWlyZWQfBgUTRmlyc3QgTmFtZSBSZXF1aXJlZGRkAiEPDxYEHwwFEkxhc3QgTmFtZSBSZXF1aXJlZB8GBRJMYXN0IE5hbWUgUmVxdWlyZWRkZAIlDxAPFgQeFEFwcGVuZERhdGFCb3VuZEl0ZW1zZx4LXyFEYXRhQm91bmRnZA8WBmYCAQICAgMCBAIFFgYQBRFQbGVhc2Ugc2VsZWN0IG9uZWVnEAUTUHJlc2lkZW50L093bmVyL0NFTwUTUHJlc2lkZW50L093bmVyL0NFT2cQBQVTYWxlcwUFU2FsZXNnEAUKUHVyY2hhc2luZwUKUHVyY2hhc2luZ2cQBQpBY2NvdW50aW5nBQpBY2NvdW50aW5nZxAFBU90aGVyBQVPdGhlcmdkZAInDw8WBB8MBRJKb2IgVGl0bGUgUmVxdWlyZWQfBgUSSm9iIFRpdGxlIFJlcXVpcmVkZGQCKQ9kFgQCAw8QDxYEHw1nHw5nZA8WBmYCAQICAgMCBAIFFgYQBRFQbGVhc2UgU2VsZWN0IE9uZWVnEAUNQWR2ZXJ0aXNlbWVudAUNQWR2ZXJ0aXNlbWVudGcQBQhBREkgRXhwbwUIQURJIEV4cG9nEAUPQURJIFNhbGVzcGVyc29uBQ9BREkgU2FsZXNwZXJzb25nEAUIV2ViIFNpdGUFCFdlYiBTaXRlZxAFBU90aGVyBQVPdGhlcmcWAWZkAgUPDxYEHwwFFFJlZmVycmVkIEJ5IFJlcXVpcmVkHwYFFFJlZmVycmVkIEJ5IFJlcXVpcmVkZGQCNw8PFgIfBmVkZAJND2QWAgIFDw8WBB8MBRBDb21wYW55IFJlcXVpcmVkHwYFEENvbXBhbnkgUmVxdWlyZWRkZAJPD2QWAgIFDw8WBB8MBRBBZGRyZXNzIFJlcXVpcmVkHwYFEEFkZHJlc3MgUmVxdWlyZWRkZAJRD2QWAgIFDw8WBB8MBQ1DaXR5IFJlcXVpcmVkHwYFDUNpdHkgUmVxdWlyZWRkZAJTD2QWBAIDDxAPFgQfDWcfDmdkEBU0EVBsZWFzZSBzZWxlY3Qgb25lB0FsYWJhbWEGQWxhc2thB0FyaXpvbmEIQXJrYW5zYXMKQ2FsaWZvcm5pYQhDb2xvcmFkbwtDb25uZWN0aWN1dAhEZWxhd2FyZQhEaXN0cmljdAdGbG9yaWRhB0dlb3JnaWEGSGF3YWlpBUlkYWhvCElsbGlub2lzB0luZGlhbmEESW93YQZLYW5zYXMIS2VudHVja3kJTG91aXNpYW5hBU1haW5lCU1hcnlsYW5kIAlNYXNzYWNodXMITWljaGlnYW4JTWlubmVzb3RhCk1pc3Npc3NpcGkJTWlzc291cmkgCU1vbnRhbmEgIAlOZWJyYXNrYSAGTmV2YWRhDU5ldyBIYW1wc2hpcmUKTmV3IEplcnNleQpOZXcgTWV4aWNvCE5ldyBZb3JrDk5vcnRoIENhcm9saW5hDE5vcnRoIERha290YQRPaGlvCE9rbGFob21hBk9yZWdvbgxQZW5uc3lsdmFuaWEMUmhvZGUgSXNsYW5kDlNvdXRoIENhcm9saW5hDFNvdXRoIERha290YQlUZW5uZXNzZWUFVGV4YXMEVXRhaAdWZXJtb250CFZpcmdpbmlhCldhc2hpbmd0b24MV2VzdCBWaXJnbmlhCVdpc2NvbnNpbgdXeW9taW5nFTQAAkFMAkFLAkFaAkFSAkNBAkNPAkNUAkRFAkRDAkZMAkdBAkhJAklEAklMAklOAklBAktTAktZAkxBAk1FAk1EAk1BAk1JAk1OAk1TAk1PAk1UAk5FAk5WAk5IAk5KAk5NAk5ZAk5DAk5EAk9IAk9LAk9SAlBBAlJJAlNDAlNEAlROAlRYAlVUAlZUAlZBAldBAldWAldJAldZFCsDNGdnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dkZAILDw8WBB8MBQ5TdGF0ZSBSZXF1aXJlZB8GBQ5TdGF0ZSBSZXF1aXJlZGRkAlUPZBYEAgUPDxYEHwwFFFBvc3RhbCBDb2RlIFJlcXVpcmVkHwYFFFBvc3RhbCBDb2RlIFJlcXVpcmVkZGQCBw8PFgQfDAUaUGxlYXNlIGVudGVyIGEgcG9zdGFsIGNvZGUfBgUaUGxlYXNlIGVudGVyIGEgcG9zdGFsIGNvZGVkZAJXD2QWBAIDDxAPFgIfDmdkEBUDDVVuaXRlZCBTdGF0ZXMLUHVlcnRvIFJpY28GQ2FuYWRhFQMCVVMCUFICQ0EUKwMDZ2dnFgFmZAIFDw8WBB8MBRBDb3VudHJ5IFJlcXVpcmVkHwYFEENvdW50cnkgUmVxdWlyZWRkZAJZD2QWBAIFDw8WBB8MBQ5QaG9uZSBSZXF1aXJlZB8GBQ5QaG9uZSBSZXF1aXJlZGRkAgcPDxYEHwwFKlBsZWFzZSBlbnRlciBhIHZhbGlkIGJ1c2luZXNzIHBob25lIG51bWJlch8GBSpQbGVhc2UgZW50ZXIgYSB2YWxpZCBidXNpbmVzcyBwaG9uZSBudW1iZXJkZAJbD2QWAgIFDw8WBB8MBSFQbGVhc2UgZW50ZXIgYSB2YWxpZCBwaG9uZSBudW1iZXIfBgUhUGxlYXNlIGVudGVyIGEgdmFsaWQgcGhvbmUgbnVtYmVyZGQCaw8PFgQfDAUiQ29uZmlkZW50aWFsaXR5IEFncmVlbWVudCBSZXF1aXJlZB8GBSJDb25maWRlbnRpYWxpdHkgQWdyZWVtZW50IFJlcXVpcmVkZGQCCg8PFgIfAmhkZBgBBR5fX0NvbnRyb2xzUmVxdWlyZVBvc3RCYWNrS2V5X18WBAVbY3RsMDAkUGxhY2VIb2xkZXJIZWFkZXIkaGVhZGVyQ29udGVudF9jbnRybCRBRElMb2dpblZpZXckTWFpbkxvZ2luVmlldyRNYWluTG9naW4kUmVtZW1iZXJNZQVLY3RsMDAkUGxhY2VIb2xkZXJNYWluJGN0bDAwJGN0bExvZ2luVmlldyRNYWluTG9naW5WaWV3JE1haW5Mb2dpbiRSZW1lbWJlck1lBSljdGwwMCRQbGFjZUhvbGRlck1haW4kY3RsMDAkcHJvbW90aW9uc19jYgUoY3RsMDAkUGxhY2VIb2xkZXJNYWluJGN0bDAwJGFncmVlbWVudF9jYgi7z7Zwchits/ZBAxyxeZWiLo1N";
            //formData["__EVENTVALIDATION"] = "/wEWWwL2nJWCBQKt4JnpDQKwgZvMBwKpw/mtCQLVuuTLDwKx66bzBwKDw5CdAQKtn4zDBQKrpLDACwLd8JPUCwKzvOreCQKEmM6pDQLYuLX9DwK3j5reCgKJ1tTKAwK2+tn7DALK0N7WAwLwgpyFAQLX9oLKDgLMpdObCAKW/s73BALBheygBAKmsv+4BQKj64PGDQKooIT+BwLB2qDwDQLAlvy6DgLG7L6bBAKJ86OABgLgvJv6CwK9kISSDQLi/77iAQLi/7riAQLi/4bjAQLi/+biAQLg/6LiAQLg/6riAQLg/97iAQLh/5LiAQLh/5riAQLn/77iAQLk/6LiAQLV/8LiAQLa/57iAQLa/77iAQLa/7biAQLa/6LiAQLY/9riAQLY/4LjAQLZ/6LiAQLe/5LiAQLe/57iAQLe/6LiAQLe/8LiAQLe/7biAQLe/9riAQLe/6riAQLe/97iAQLf/5LiAQLf/9biAQLf/87iAQLf/8biAQLf/7LiAQLf/4LjAQLf/5riAQLf/57iAQLc/87iAQLc/7riAQLc/+biAQLN/6LiAQLT/8LiAQLQ/5riAQLQ/57iAQLR/7biAQLR/47jAQLW/97iAQLX/97iAQLX/6LiAQLU/6LiAQLU/9biAQLU/8LiAQLU/4LjAQLbkIbGBQK064aOCALC7q3hDQKGwOr9AgLq8OXoBQKaueyoCQLmj6vTBwLkovrPAwLarrP9BoHBXyNl6lY33BkKH2Vuh9DiZvW7";
            //formData["ctl00$PlaceHolderMain$ctl00$ctlLoginView$MainLoginView$MainLogin$UserName"] = "PHIL0691";
            //formData["ctl00$PlaceHolderMain$ctl00$ctlLoginView$MainLoginView$MainLogin$Password"] = "ABC123";
            //formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$MainLoginView$MainLogin$UserName"] = "PHIL0691";
            //formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$MainLoginView$MainLogin$Password"] = "ABC123";
            //formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$SplashLoginView$MainLogin$UserName"] = "";
            //formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$SplashLoginView$MainLogin$Password"] = "";
            //formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$hidden_url_location"] = "";
            //formData["ctl00$xyHF"] = "";
            //formData["MSOSPWebPartManager_StartWebPartEditingName"] = "";
            //formData["MSOSPWebPartManager_OldDisplayModeName"] = "Browse";
            // formData["MSOWebPartPage_Shared"] = "";	
            // formData["MSOLayout_LayoutChanges"] = "";
            // formData["MSOLayout_InDesignMode"] = "";
            // formData["MSOAuthoringConsole_FormContext"] = "";
            // formData["MSOAC_EditDuringWorkflow"] = "";
            //formData["MSOGallery_SelectedLibrary"] = "";	
            //formData["MSOGallery_FilterString"] = "";
            //formData["MSOWebPartPage_PostbackSource"] = "";
            //formData["MSOTlPn_SelectedWpId"] = "";
            //byte[] responseBytes = webClient.UploadValues(URLAuth, "POST", formData);
            //string resultAuthTicket = Encoding.UTF8.GetString(responseBytes);
            //webClient.Dispose();
            #endregion

            #region [Login]
            browser.Url = "https://adiglobal.us/Pages/WebRegistration.aspx";
            HtmlAgilityPack.HtmlDocument document =  browser.GetWebRequest();
            NameValueCollection formData = browser.GetFormData(document);
            formData["MSO_PageHashCode"] = "2691463195435";
            formData["__SPSCEditMenu"] = "true";
            formData["MSOTlPn_View"] = "0";
            formData["MSOTlPn_ShowSettings"] = "false";
            formData["MSOTlPn_Button"] = "none";
            formData["MSOSPWebPartManager_DisplayModeName"] = "Browse";

            formData["__EVENTTARGET"] = "ctl00$PlaceHolderMain$ctl00$ctlLoginView$MainLoginView$MainLogin$LoginButton";
            formData["__EVENTARGUMENT"] = "";
            formData["__VIEWSTATE"] = "/wEPDwUBMA9kFgJmD2QWBAIBDxYCHhBkYXRhLWlzLXBvc3RiYWNrBQExFgQCBA9kFgJmD2QWAgIBDxYCHhNQcmV2aW91c0NvbnRyb2xNb2RlCymIAU1pY3Jvc29mdC5TaGFyZVBvaW50LldlYkNvbnRyb2xzLlNQQ29udHJvbE1vZGUsIE1pY3Jvc29mdC5TaGFyZVBvaW50LCBWZXJzaW9uPTEyLjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPTcxZTliY2UxMTFlOTQyOWMBZAIID2QWAmYPZBYCZg9kFgRmDxUEAjE1ATIUaHR0cHM6Ly9hZGlnbG9iYWwudXMUaHR0cHM6Ly9hZGlnbG9iYWwudXNkAgIPZBYCZg9kFgRmDxUWGGh0dHBzOi8vY2RuLmFkaWdsb2JhbC51cwgyMDE0MDkxMxhodHRwczovL2Nkbi5hZGlnbG9iYWwudXMIMjAxNDA5MTMYaHR0cHM6Ly9jZG4uYWRpZ2xvYmFsLnVzCDIwMTQwOTEzGGh0dHBzOi8vY2RuLmFkaWdsb2JhbC51cwgyMDE0MDkxMxhodHRwczovL2Nkbi5hZGlnbG9iYWwudXMIMjAxNDA5MTMYaHR0cHM6Ly9jZG4uYWRpZ2xvYmFsLnVzCDIwMTQwOTEzGGh0dHBzOi8vY2RuLmFkaWdsb2JhbC51cwgyMDE0MDkxMxhodHRwczovL2Nkbi5hZGlnbG9iYWwudXMIMjAxNDA5MTMYaHR0cHM6Ly9jZG4uYWRpZ2xvYmFsLnVzCDIwMTQwOTEzGGh0dHBzOi8vY2RuLmFkaWdsb2JhbC51cwgyMDE0MDkxMxhodHRwczovL2Nkbi5hZGlnbG9iYWwudXMIMjAxNDA5MTNkAgIPFQIYaHR0cHM6Ly9jZG4uYWRpZ2xvYmFsLnVzCDIwMTQwOTEzZAIDD2QWCgICD2QWAgIBD2QWAmYPZBYCAgEPZBYCAgEPZBYGAgEPZBYCAgEPFgIeB1Zpc2libGVoFgJmD2QWBAICD2QWAgIDDxYCHwJoZAIDDw8WAh4JQWNjZXNzS2V5BQEvZGQCAw9kFgICAQ9kFgICAg8PFgIfAmcWAh4Fc3R5bGUFDmRpc3BsYXk6YmxvY2s7ZAIFD2QWAgIBDw8WAh8CaGQWBAIBDw8WAh8CaGRkAgMPDxYCHwJoZBYCAgEPDxYCHwJnZBYEAgEPDxYCHwJoZBYcAgEPDxYCHwJoZGQCAw8WAh8CaGQCBQ8PFgIfAmhkZAIHDxYCHwJoZAIJDw8WAh8CaGRkAgsPDxYCHwJoZGQCDQ8PFgIfAmhkZAIPDw8WBB4HRW5hYmxlZGgfAmhkZAIRDw8WAh8CaGRkAhMPDxYEHwVoHwJoZGQCFQ8PFgIfAmhkZAIXDxYCHwJoZAIZDxYCHwJoZAIbDw8WAh8CZ2RkAgMPDxYCHwJnZBYGAgEPDxYCHwJnZGQCAw8PFgIfAmdkZAIFDw8WAh8CZ2RkAgQPZBYCAgEPZBYIAgMPZBYEZg8WAh4EVGV4dAVSZG9jdW1lbnQuZm9ybXNbMF0uYWN0aW9uID0gZG9jdW1lbnQuZm9ybXNbMF0uYWN0aW9uLnJlcGxhY2UoJ2h0dHA6Ly8nLCdodHRwczovLycpO2QCAg9kFgJmD2QWAgIBD2QWAmYPZBYCAgUPZBYCAhUPEA8WAh4HQ2hlY2tlZGhkZGRkAgsPZBYCZg9kFgJmDzwrAAkAZAITDxYCHgtfIUl0ZW1Db3VudAILFhYCAQ9kFgJmDxUCBDYwMDAGQWNjZXNzZAICD2QWAmYPFQIENTAwMA1BdWRpbyAmIFZpZGVvZAIDD2QWAmYPFQIENTgwMA5DZW50cmFsIFZhY3V1bWQCBA9kFgJmDxUCBDIwMDAERmlyZWQCBQ9kFgJmDxUCBDEwMDAJSW50cnVzaW9uZAIGD2QWAmYPFQIEODAwMA9OZXR3b3JrIFN5c3RlbXNkAgcPZBYCZg8VAgQxNzAwBVBvd2VyZAIID2QWAmYPFQIEOTAwMAlUZWxlcGhvbnlkAgkPZBYCZg8VAgQ3MDAwEFRvb2xzICYgSGFyZHdhcmVkAgoPZBYCZg8VAgQzMDAwElZpZGVvIFN1cnZlaWxsYW5jZWQCCw9kFgJmDxUCBDQwMDAMV2lyZSAmIENhYmxlZAIbD2QWAmYPZBYCZg9kFgICAQ8WAh8GBQ9XZWxjb21lIHRvIEFESSFkAgYPZBYCAgEPZBYCAgQPZBYCZg9kFgQCAw8PFgIeC05hdmlnYXRlVXJsBUovQ29tcGFueS9QYWdlcy9Na3RnX1Nob3BQcm9kdWN0cy5hc3B4P2NhdD1BREkgVVMmY2F0ZWdvcnk9MDAwMCZwYXJlbnQ9MDAwMGRkAgUPFgIfCAILFhYCAQ9kFgICAQ8PFgYfCQVEL0NvbXBhbnkvUGFnZXMvTWt0Z19BY2Nlc3MuYXNweD9jYXQ9QURJIFVTJmNhdGVnb3J5PTYwMDAmcGFyZW50PTAwMDAfBgUGQWNjZXNzHgdUb29sVGlwBQZBY2Nlc3NkZAICD2QWAgIBDw8WBh8JBUovQ29tcGFueS9QYWdlcy9Na3RnX0F1ZGlvVmlkZW8uYXNweD9jYXQ9QURJJTIwVVMmY2F0ZWdvcnk9NTAwMCZwYXJlbnQ9MDAwMB8GBQ1BdWRpbyAmIFZpZGVvHwoFDUF1ZGlvICYgVmlkZW9kZAIDD2QWAgIBDw8WBh8JBUsvQ29tcGFueS9QYWdlcy9Na3RnX0NlbnRyYWxWYWN1dW0uYXNweD9jYXQ9QURJIFVTJmNhdGVnb3J5PTU4MDAmcGFyZW50PTAwMDAfBgUOQ2VudHJhbCBWYWN1dW0fCgUOQ2VudHJhbCBWYWN1dW1kZAIED2QWAgIBDw8WBh8JBUIvQ29tcGFueS9QYWdlcy9Na3RnX0ZpcmUuYXNweD9jYXQ9QURJIFVTJmNhdGVnb3J5PTIwMDAmcGFyZW50PTAwMDAfBgUERmlyZR8KBQRGaXJlZGQCBQ9kFgICAQ8PFgYfCQVHL0NvbXBhbnkvUGFnZXMvTWt0Z19JbnRydXNpb24uYXNweD9jYXQ9QURJIFVTJmNhdGVnb3J5PTEwMDAmcGFyZW50PTAwMDAfBgUJSW50cnVzaW9uHwoFCUludHJ1c2lvbmRkAgYPZBYCAgEPDxYGHwkFTi9Db21wYW55L1BhZ2VzL01rdGdfTmV0d29ya1N5c3RlbXMuYXNweD9jYXQ9QURJJTIwVVMmY2F0ZWdvcnk9ODAwMCZwYXJlbnQ9MDAwMB8GBQ9OZXR3b3JrIFN5c3RlbXMfCgUPTmV0d29yayBTeXN0ZW1zZGQCBw9kFgICAQ8PFgYfCQVDL0NvbXBhbnkvUGFnZXMvTWt0Z19Qb3dlci5hc3B4P2NhdD1BREkgVVMmY2F0ZWdvcnk9MTcwMCZwYXJlbnQ9MDAwMB8GBQVQb3dlch8KBQVQb3dlcmRkAggPZBYCAgEPDxYGHwkFSS9Db21wYW55L1BhZ2VzL01rdGdfVGVsZXBob255LmFzcHg/Y2F0PUFESSUyMFVTJmNhdGVnb3J5PTkwMDAmcGFyZW50PTAwMDAfBgUJVGVsZXBob255HwoFCVRlbGVwaG9ueWRkAgkPZBYCAgEPDxYGHwkFSy9Db21wYW55L1BhZ2VzL01rdGdfVG9vbHNIYXJkd2FyZS5hc3B4P2NhdD1BREkgVVMmY2F0ZWdvcnk9NzAwMCZwYXJlbnQ9MDAwMB8GBRBUb29scyAmIEhhcmR3YXJlHwoFEFRvb2xzICYgSGFyZHdhcmVkZAIKD2QWAgIBDw8WBh8JBU8vQ29tcGFueS9QYWdlcy9Na3RnX1ZpZGVvU3VydmVpbGxhbmNlLmFzcHg/Y2F0PUFESSBVUyZjYXRlZ29yeT0zMDAwJnBhcmVudD0wMDAwHwYFElZpZGVvIFN1cnZlaWxsYW5jZR8KBRJWaWRlbyBTdXJ2ZWlsbGFuY2VkZAILD2QWAgIBDw8WBh8JBUcvQ29tcGFueS9QYWdlcy9Na3RnX1dpcmVDYWJsZS5hc3B4P2NhdD1BREkgVVMmY2F0ZWdvcnk9NDAwMCZwYXJlbnQ9MDAwMB8GBQxXaXJlICYgQ2FibGUfCgUMV2lyZSAmIENhYmxlZGQCCg9kFgICAQ9kFgICAw88KwAFAQAPFgIeD1NpdGVNYXBQcm92aWRlcgUVQURJWG1sU2l0ZU1hcFByb3ZpZGVyZGQCDA9kFgICAw9kFgICAg9kFgYCBg9kFgQCAQ8WAh8GBVJkb2N1bWVudC5mb3Jtc1swXS5hY3Rpb24gPSBkb2N1bWVudC5mb3Jtc1swXS5hY3Rpb24ucmVwbGFjZSgnaHR0cDovLycsJ2h0dHBzOi8vJyk7ZAIDD2QWAmYPZBYCAgEPZBYCZg9kFgICAQ9kFgICDw8QDxYCHwdoZGRkZAIID2QWKgIDDw8WAh8GBS4qIFBsZWFzZSBlbnRlciBpbmZvcm1hdGlvbiBpbiByZXF1aXJlZCBmaWVsZHMuFgIfBAUOZGlzcGxheTpibG9jaztkAgUPDxYEHgxFcnJvck1lc3NhZ2UFJCogUGxlYXNlIGVudGVyIGEgdmFsaWQgZW1haWwgYWRkcmVzcx8GBSQqIFBsZWFzZSBlbnRlciBhIHZhbGlkIGVtYWlsIGFkZHJlc3NkZAILDw8WBB8MBRtBREkgQWNjb3VudCBOdW1iZXIgUmVxdWlyZWQfBgUbQURJIEFjY291bnQgTnVtYmVyIFJlcXVpcmVkZGQCDQ8PFgQfDAUkUGxlYXNlIGVudGVyIGEgdmFsaWQgY3VzdG9tZXIgbnVtYmVyHwYFJFBsZWFzZSBlbnRlciBhIHZhbGlkIGN1c3RvbWVyIG51bWJlcmRkAg8PZBYCAgUPDxYEHwwFG1BsZWFzZSBlbnRlciBhIHZhbGlkIHN1ZmZpeB8GBRtQbGVhc2UgZW50ZXIgYSB2YWxpZCBzdWZmaXhkZAIVDw8WBB8MBQ5FbWFpbCBSZXF1aXJlZB8GBQ5FbWFpbCBSZXF1aXJlZGRkAhsPDxYEHwwFE0ZpcnN0IE5hbWUgUmVxdWlyZWQfBgUTRmlyc3QgTmFtZSBSZXF1aXJlZGRkAiEPDxYEHwwFEkxhc3QgTmFtZSBSZXF1aXJlZB8GBRJMYXN0IE5hbWUgUmVxdWlyZWRkZAIlDxAPFgQeFEFwcGVuZERhdGFCb3VuZEl0ZW1zZx4LXyFEYXRhQm91bmRnZA8WBmYCAQICAgMCBAIFFgYQBRFQbGVhc2Ugc2VsZWN0IG9uZWVnEAUTUHJlc2lkZW50L093bmVyL0NFTwUTUHJlc2lkZW50L093bmVyL0NFT2cQBQVTYWxlcwUFU2FsZXNnEAUKUHVyY2hhc2luZwUKUHVyY2hhc2luZ2cQBQpBY2NvdW50aW5nBQpBY2NvdW50aW5nZxAFBU90aGVyBQVPdGhlcmdkZAInDw8WBB8MBRJKb2IgVGl0bGUgUmVxdWlyZWQfBgUSSm9iIFRpdGxlIFJlcXVpcmVkZGQCKQ9kFgQCAw8QDxYEHw1nHw5nZA8WBmYCAQICAgMCBAIFFgYQBRFQbGVhc2UgU2VsZWN0IE9uZWVnEAUNQWR2ZXJ0aXNlbWVudAUNQWR2ZXJ0aXNlbWVudGcQBQhBREkgRXhwbwUIQURJIEV4cG9nEAUPQURJIFNhbGVzcGVyc29uBQ9BREkgU2FsZXNwZXJzb25nEAUIV2ViIFNpdGUFCFdlYiBTaXRlZxAFBU90aGVyBQVPdGhlcmcWAWZkAgUPDxYEHwwFFFJlZmVycmVkIEJ5IFJlcXVpcmVkHwYFFFJlZmVycmVkIEJ5IFJlcXVpcmVkZGQCNw8PFgIfBmVkZAJND2QWAgIFDw8WBB8MBRBDb21wYW55IFJlcXVpcmVkHwYFEENvbXBhbnkgUmVxdWlyZWRkZAJPD2QWAgIFDw8WBB8MBRBBZGRyZXNzIFJlcXVpcmVkHwYFEEFkZHJlc3MgUmVxdWlyZWRkZAJRD2QWAgIFDw8WBB8MBQ1DaXR5IFJlcXVpcmVkHwYFDUNpdHkgUmVxdWlyZWRkZAJTD2QWBAIDDxAPFgQfDWcfDmdkEBU0EVBsZWFzZSBzZWxlY3Qgb25lB0FsYWJhbWEGQWxhc2thB0FyaXpvbmEIQXJrYW5zYXMKQ2FsaWZvcm5pYQhDb2xvcmFkbwtDb25uZWN0aWN1dAhEZWxhd2FyZQhEaXN0cmljdAdGbG9yaWRhB0dlb3JnaWEGSGF3YWlpBUlkYWhvCElsbGlub2lzB0luZGlhbmEESW93YQZLYW5zYXMIS2VudHVja3kJTG91aXNpYW5hBU1haW5lCU1hcnlsYW5kIAlNYXNzYWNodXMITWljaGlnYW4JTWlubmVzb3RhCk1pc3Npc3NpcGkJTWlzc291cmkgCU1vbnRhbmEgIAlOZWJyYXNrYSAGTmV2YWRhDU5ldyBIYW1wc2hpcmUKTmV3IEplcnNleQpOZXcgTWV4aWNvCE5ldyBZb3JrDk5vcnRoIENhcm9saW5hDE5vcnRoIERha290YQRPaGlvCE9rbGFob21hBk9yZWdvbgxQZW5uc3lsdmFuaWEMUmhvZGUgSXNsYW5kDlNvdXRoIENhcm9saW5hDFNvdXRoIERha290YQlUZW5uZXNzZWUFVGV4YXMEVXRhaAdWZXJtb250CFZpcmdpbmlhCldhc2hpbmd0b24MV2VzdCBWaXJnbmlhCVdpc2NvbnNpbgdXeW9taW5nFTQAAkFMAkFLAkFaAkFSAkNBAkNPAkNUAkRFAkRDAkZMAkdBAkhJAklEAklMAklOAklBAktTAktZAkxBAk1FAk1EAk1BAk1JAk1OAk1TAk1PAk1UAk5FAk5WAk5IAk5KAk5NAk5ZAk5DAk5EAk9IAk9LAk9SAlBBAlJJAlNDAlNEAlROAlRYAlVUAlZUAlZBAldBAldWAldJAldZFCsDNGdnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dnZ2dkZAILDw8WBB8MBQ5TdGF0ZSBSZXF1aXJlZB8GBQ5TdGF0ZSBSZXF1aXJlZGRkAlUPZBYEAgUPDxYEHwwFFFBvc3RhbCBDb2RlIFJlcXVpcmVkHwYFFFBvc3RhbCBDb2RlIFJlcXVpcmVkZGQCBw8PFgQfDAUaUGxlYXNlIGVudGVyIGEgcG9zdGFsIGNvZGUfBgUaUGxlYXNlIGVudGVyIGEgcG9zdGFsIGNvZGVkZAJXD2QWBAIDDxAPFgIfDmdkEBUDDVVuaXRlZCBTdGF0ZXMLUHVlcnRvIFJpY28GQ2FuYWRhFQMCVVMCUFICQ0EUKwMDZ2dnFgFmZAIFDw8WBB8MBRBDb3VudHJ5IFJlcXVpcmVkHwYFEENvdW50cnkgUmVxdWlyZWRkZAJZD2QWBAIFDw8WBB8MBQ5QaG9uZSBSZXF1aXJlZB8GBQ5QaG9uZSBSZXF1aXJlZGRkAgcPDxYEHwwFKlBsZWFzZSBlbnRlciBhIHZhbGlkIGJ1c2luZXNzIHBob25lIG51bWJlch8GBSpQbGVhc2UgZW50ZXIgYSB2YWxpZCBidXNpbmVzcyBwaG9uZSBudW1iZXJkZAJbD2QWAgIFDw8WBB8MBSFQbGVhc2UgZW50ZXIgYSB2YWxpZCBwaG9uZSBudW1iZXIfBgUhUGxlYXNlIGVudGVyIGEgdmFsaWQgcGhvbmUgbnVtYmVyZGQCaw8PFgQfDAUiQ29uZmlkZW50aWFsaXR5IEFncmVlbWVudCBSZXF1aXJlZB8GBSJDb25maWRlbnRpYWxpdHkgQWdyZWVtZW50IFJlcXVpcmVkZGQCCg8PFgIfAmhkZBgBBR5fX0NvbnRyb2xzUmVxdWlyZVBvc3RCYWNrS2V5X18WBAVbY3RsMDAkUGxhY2VIb2xkZXJIZWFkZXIkaGVhZGVyQ29udGVudF9jbnRybCRBRElMb2dpblZpZXckTWFpbkxvZ2luVmlldyRNYWluTG9naW4kUmVtZW1iZXJNZQVLY3RsMDAkUGxhY2VIb2xkZXJNYWluJGN0bDAwJGN0bExvZ2luVmlldyRNYWluTG9naW5WaWV3JE1haW5Mb2dpbiRSZW1lbWJlck1lBSljdGwwMCRQbGFjZUhvbGRlck1haW4kY3RsMDAkcHJvbW90aW9uc19jYgUoY3RsMDAkUGxhY2VIb2xkZXJNYWluJGN0bDAwJGFncmVlbWVudF9jYgi7z7Zwchits/ZBAxyxeZWiLo1N";
            formData["__EVENTVALIDATION"] = "/wEWWwL2nJWCBQKt4JnpDQKwgZvMBwKpw/mtCQLVuuTLDwKx66bzBwKDw5CdAQKtn4zDBQKrpLDACwLd8JPUCwKzvOreCQKEmM6pDQLYuLX9DwK3j5reCgKJ1tTKAwK2+tn7DALK0N7WAwLwgpyFAQLX9oLKDgLMpdObCAKW/s73BALBheygBAKmsv+4BQKj64PGDQKooIT+BwLB2qDwDQLAlvy6DgLG7L6bBAKJ86OABgLgvJv6CwK9kISSDQLi/77iAQLi/7riAQLi/4bjAQLi/+biAQLg/6LiAQLg/6riAQLg/97iAQLh/5LiAQLh/5riAQLn/77iAQLk/6LiAQLV/8LiAQLa/57iAQLa/77iAQLa/7biAQLa/6LiAQLY/9riAQLY/4LjAQLZ/6LiAQLe/5LiAQLe/57iAQLe/6LiAQLe/8LiAQLe/7biAQLe/9riAQLe/6riAQLe/97iAQLf/5LiAQLf/9biAQLf/87iAQLf/8biAQLf/7LiAQLf/4LjAQLf/5riAQLf/57iAQLc/87iAQLc/7riAQLc/+biAQLN/6LiAQLT/8LiAQLQ/5riAQLQ/57iAQLR/7biAQLR/47jAQLW/97iAQLX/97iAQLX/6LiAQLU/6LiAQLU/9biAQLU/8LiAQLU/4LjAQLbkIbGBQK064aOCALC7q3hDQKGwOr9AgLq8OXoBQKaueyoCQLmj6vTBwLkovrPAwLarrP9BoHBXyNl6lY33BkKH2Vuh9DiZvW7";
            formData["ctl00$PlaceHolderMain$ctl00$ctlLoginView$MainLoginView$MainLogin$UserName"] = "PHIL0691";
            formData["ctl00$PlaceHolderMain$ctl00$ctlLoginView$MainLoginView$MainLogin$Password"] = "ABC123";
            formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$MainLoginView$MainLogin$UserName"] = "PHIL0691";
            formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$MainLoginView$MainLogin$Password"] = "ABC123";
            formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$SplashLoginView$MainLogin$UserName"] = "";
            formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$SplashLoginView$MainLogin$Password"] = "";
            formData["ctl00$PlaceHolderHeader$headerContent_cntrl$ADILoginView$hidden_url_location"] = "";
            formData["ctl00$xyHF"] = "";
            formData["MSOSPWebPartManager_StartWebPartEditingName"] = "";
            formData["MSOSPWebPartManager_OldDisplayModeName"] = "Browse";
            formData["MSOWebPartPage_Shared"] = "";
            formData["MSOLayout_LayoutChanges"] = "";
            formData["MSOLayout_InDesignMode"] = "";
            formData["MSOAuthoringConsole_FormContext"] = "";
            formData["MSOAC_EditDuringWorkflow"] = "";
            formData["MSOGallery_SelectedLibrary"] = "";
            formData["MSOGallery_FilterString"] = "";
            formData["MSOWebPartPage_PostbackSource"] = "";
            formData["MSOTlPn_SelectedWpId"] = "";
            HtmlAgilityPack.HtmlDocument doc = browser.PostRequest(null, formData);
            #endregion

            #region [Load Catagory]
            //string url = "https://adiglobal.us/Company/Pages/Mktg_ShopProducts.aspx?cat=ADI%20US&category=0000&parent=0000";
            //string url = "https://adiglobal.us:443/Company/Pages/Mktg_AccessAuth.aspx?cat=ADI%20US&category=6000&parent=0000";
            String url = "https://adiglobal.us:443/Pages/default.aspx";
            List<Categories> oCategories = new List<Categories>();
            Spider oSpider = new Spider(browser);
            oCategories = oSpider.ParseCatagory(url);
            AddToCatagory(oCategories);
            #endregion

            //new Crawler().GetProductSpecification("NC-PX26H", "Security Cards & Tokens_10394");
            //new Crawler().GetInventory();
        }

        

        #region [Add Catagory to Tree]
        private void AddToCatagory(List<Categories> oCategories)
        {
            foreach(Categories c in oCategories)
            {
                TreeNode level1 = new TreeNode();
                level1.Text = c.categoriesName;
                level1.Tag = c.categoriesUrl;
                foreach (SubCatagory s in c.SubCatagoryList)
                {
                    TreeNode level2 = new TreeNode();
                    level2.Text = s.subCategoriesName;
                    level2.Tag = s.subCategoriesUrl;
                    level1.Nodes.Add(level2);
                    foreach (SubSubCatagory ss in s.subSubCatagoryList)
                    {
                        TreeNode level3 = new TreeNode();
                        level3.Text = ss.subSubCategoriesName;
                        level3.Tag = ss.subSubCategoriesUrl;
                        level2.Nodes.Add(level3);
                    }
                }
                treeCatagory.Nodes.Add(level1);
            }
        }
        #endregion

        private void treeCatagory_AfterCheck(object sender, TreeViewEventArgs e)
        {
            CheckTreeViewNode(e.Node, e.Node.Checked);
            TreeViewNodeValue(e.Node, e.Node.Checked);
        }

        private void CheckTreeViewNode(TreeNode node, Boolean isChecked)
        {
            foreach (TreeNode item in node.Nodes)
            {
                item.Checked = isChecked;

                if (item.Nodes.Count > 0)
                {
                    this.CheckTreeViewNode(item, isChecked);
                }
            }
        }

        private void TreeViewNodeValue(TreeNode node, Boolean isChecked)
        {
            if (node.Nodes.Count <= 0 && isChecked == true)
            {
                string[] row = { node.Text, node.Tag.ToString(), "Open" };
                ListViewItem lItem = new ListViewItem(row);
                if (DeplicateEntry(node.Text) == true)
                {
                    listViewThreads.Items.Add(lItem);
                }
            }
              
            else
            {
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode item in node.Nodes)
                    {

                        if (item.Nodes.Count <= 0 && isChecked == true)
                        {
                            string[] row = { item.Text, "Open", item.Tag.ToString() };
                            ListViewItem lItem = new ListViewItem(row);
                            if (DeplicateEntry(item.Text) == true)
                            {
                                listViewThreads.Items.Add(lItem);
                            }
                        }
                        else if (item.Nodes.Count <= 0 && isChecked == false)
                        {
                            RemoveEntry(item.Text);
                        }

                    }
                }
                else
                {
                    RemoveEntry(node.Text);
                }
            }

        }

        private bool DeplicateEntry(string lValue)
        {
            bool r = true;
              foreach (ListViewItem item in listViewThreads.Items)
                {
                    if (item.SubItems[0].Text == lValue)
                    {
                        r = false;
                    }
                }
            
            return r;
        }

        private void RemoveEntry(string lValue)
        {
           
            foreach (ListViewItem item in listViewThreads.Items)
            {
                if (item.SubItems[0].Text == lValue)
                {
                    listViewThreads.Items.Remove(item);
                }
            }

           
        }
        // push uri to the queue
        bool EnqueueUri(MyUri uri, bool bCheckRepetition)
        {
            // add the uri to the binary tree to check if it is duplicated or not
            if (bCheckRepetition == true)
                return false;

            Monitor.Enter(queueURLS);
            try
            {
                // add the uri to the queue
                queueURLS.Enqueue(uri);
            }
            catch (Exception)
            {
            }
            Monitor.Exit(queueURLS);

            return true;
        }
        // pop uri from the queue
        MyUri DequeueUri()
        {
            Monitor.Enter(queueURLS);
            MyUri uri = null;
            try
            {
                uri = (MyUri)queueURLS.Dequeue();
            }
            catch (Exception)
            {
            }
            Monitor.Exit(queueURLS);
            return uri;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            while ((from ListViewItem li in listViewThreads.Items where li.SubItems[2].Text.Equals("Open") select li).Count() > 0)
                ProcessQueue();
        }

        private void ProcessQueue()
        {
            ListViewItem item = (from ListViewItem li in listViewThreads.Items where li.SubItems[2].Text.Equals("Open") select li).FirstOrDefault();
            if (!ReferenceEquals(item, null))
            {
                Spider oSpider = new Spider();
                item.SubItems[2].Text = "Processing";
                //List<Product> products = oSpider.ParseProducts(item.SubItems[1].Text);
                //foreach (Product p in products)
                //    SaveProduct(p, true);
                new Cache().GetFormData(item.SubItems[1].Text);
                item.SubItems[2].Text = "Complete";
            }
        }

        private void SaveProduct(Product product, Boolean SaveImage = false)
        {
            String SmallImage = null;
            String BigImage = null;
            if (SaveImage)
            {
                String ImagePath = String.Format("{0}\\Images", Application.StartupPath);

                String VendorDir = GetValidDirName(product.VendorName);
                String ModelDir = GetValidDirName(product.VendorModel);

                if (!Directory.Exists(String.Format("{0}\\{1}", ImagePath, VendorDir)))
                    Directory.CreateDirectory(String.Format("{0}\\{1}", ImagePath, VendorDir));

                Crawler crawler = new Crawler();
                crawler.Url = product.SmallImage;
                SmallImage = String.Format("{0}_small{1}", ModelDir, GetFileExtension(product.SmallImage));
                crawler.Save(String.Format("{0}\\{1}\\{2}", ImagePath, VendorDir, SmallImage));

                crawler.Url = product.BigImage;
                BigImage = String.Format("{0}_large{1}", ModelDir, GetFileExtension(product.BigImage));
                //crawler.Save(String.Format("{0}\\{1}\\{2}", ImagePath, VendorDir, BigImage));
            }
            //new WebSpiderDb.Final_TableDataTable().
            new Final_TableTableAdapter().Insert(0, null, null, null, product.VendorName, "1111", product.PartNumber, 0.00m, SmallImage, BigImage, product.VendorName, "0", DateTime.Now.ToString()
                , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
        }

        private String GetFileExtension(string FileName)
        {
            try
            {
                return FileName.Substring(FileName.LastIndexOf('.'));
            }
            catch
            {
                return "";
            }
        }

        private String GetValidDirName(String DirName)
        {
            try 
            {
                //com1, com2, com3, com4, com5, com6, com7, com8, com9, lpt1, lpt2, lpt3, lpt4, lpt5, lpt6, lpt7, lpt8, lpt9, con, nul, and prn
                DirName = DirName.Replace(" ", "_");
                DirName = DirName.Replace("/", "_");
                DirName = DirName.Replace("?", "_");
                DirName = DirName.Replace("<", "_");
                DirName = DirName.Replace(">", "_");
                DirName = DirName.Replace("\\", "_");
                DirName = DirName.Replace(":", "_");
                DirName = DirName.Replace("*", "_");
                DirName = DirName.Replace("|", "_");
                DirName = DirName.Replace("\"", "_");

                return DirName;
            }
            catch{
                return "";
            }
        }
    }
    /// <summary>
    /// A result encapsulating the Url and the HtmlDocument
    /// </summary>
    public abstract class WebPage
    {
        public Uri Url { get; set; }

        /// <summary>
        /// Get every WebPage.Internal on a web site (or part of a web site) visiting all internal links just once
        /// plus every external page (or other Url) linked to the web site as a WebPage.External
        /// </summary>
        /// <remarks>
        /// Use .OfType WebPage.Internal to get just the internal ones if that's what you want
        /// </remarks>
        public static IEnumerable<WebPage> GetAllPagesUnder(Uri urlRoot)
        {
            var queue = new Queue<Uri>();
            var allSiteUrls = new HashSet<Uri>();

            queue.Enqueue(urlRoot);
            allSiteUrls.Add(urlRoot);

            while (queue.Count > 0)
            {
                Uri url = queue.Dequeue();

                HttpWebRequest oReq = (HttpWebRequest)WebRequest.Create(url);
                oReq.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";

                HttpWebResponse resp = (HttpWebResponse)oReq.GetResponse();

                WebPage result;

                if (resp.ContentType.StartsWith("text/html", StringComparison.InvariantCultureIgnoreCase))
                {
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    try
                    {
                        var resultStream = resp.GetResponseStream();
                        doc.Load(resultStream); // The HtmlAgilityPack
                        result = new Internal() { Url = url, HtmlDocument = doc };
                    }
                    catch (System.Net.WebException ex)
                    {
                        result = new WebPage.Error() { Url = url, Exception = ex };
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Add("Url", url);    // Annotate the exception with the Url
                        throw;
                    }

                    // Success, hand off the page
                    yield return new WebPage.Internal() { Url = url, HtmlDocument = doc };

                    // And and now queue up all the links on this page
                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes(@"//a[@href]"))
                    {
                        HtmlAttribute att = link.Attributes["href"];
                        if (att == null) continue;
                        string href = att.Value;
                        if (href.StartsWith("javascript", StringComparison.InvariantCultureIgnoreCase)) continue;      // ignore javascript on buttons using a tags

                        Uri urlNext = new Uri(href, UriKind.RelativeOrAbsolute);

                        // Make it absolute if it's relative
                        if (!urlNext.IsAbsoluteUri)
                        {
                            urlNext = new Uri(urlRoot, urlNext);
                        }

                        if (!allSiteUrls.Contains(urlNext))
                        {
                            allSiteUrls.Add(urlNext);               // keep track of every page we've handed off

                            if (urlRoot.IsBaseOf(urlNext))
                            {
                                queue.Enqueue(urlNext);
                            }
                            else
                            {
                                yield return new WebPage.External() { Url = urlNext };
                            }
                        }
                    }
                }
            }
        }

        ///// <summary>
        ///// In the future might provide all the images too??
        ///// </summary>
        //public class Image : WebPage
        //{
        //}

        /// <summary>
        /// Error loading page
        /// </summary>
        public class Error : WebPage
        {
            public int HttpResult { get; set; }
            public Exception Exception { get; set; }
        }

        /// <summary>
        /// External page - not followed
        /// </summary>
        /// <remarks>
        /// No body - go load it yourself
        /// </remarks>
        public class External : WebPage
        {
        }

        /// <summary>
        /// Internal page
        /// </summary>
        public class Internal : WebPage
        {
            /// <summary>
            /// For internal pages we load the document for you
            /// </summary>
            public virtual HtmlAgilityPack.HtmlDocument HtmlDocument { get; internal set; }
        }
    }


}
