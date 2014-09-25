using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace WebSpider.Core
{
    public class Spider
    {
        #region [Variables/Constants]
        public const string siteUrl = "http://adiglobal.us";

        private Cache Cache = new Cache();
        private Browser browser = new Browser();
        #endregion
        

        #region [Constructor]
        public Spider() { }
        public Spider(Browser browser)
        {
            this.browser = browser;
        }
        #endregion

        #region [Login]
        public bool Login()
        {
            browser.Url = "https://adiglobal.us/Pages/WebRegistration.aspx";
            HtmlAgilityPack.HtmlDocument document = browser.GetWebRequest();
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
            return true;
        }
        #endregion

        #region [Parse caregory/Sub category]
        public List<Categories> ParseCatagory(string url)
        {
            List<Categories> categoriesList = new List<Categories>();
            #region [URL Scraping]
            //var getHtmlWeb = new HtmlWeb();
            //var doc = getHtmlWeb.Load(url);
            //var doc = Cache.GetUrl(url);
            browser.Url = url;
            var doc = browser.GetWebRequest();
            #endregion


            HtmlNode node = doc.DocumentNode.SelectSingleNode("//ul[@class='subCategory']");
            var hoteleWebsiteDoc = (from element in node.Descendants("a")
                                    where element.ParentNode.Name.Equals("li") &&
                                    element.Attributes.Contains("href")
                                    select new
                                    {
                                        URL = element.Attributes["href"].Value,
                                        Desc = element.InnerText
                                    }).ToList();

            foreach (var obj in hoteleWebsiteDoc)
            {
                Categories oCategories = new Categories();
                oCategories.categoriesUrl = "http://adiglobal.us" + obj.URL.Replace("amp;", "");
                oCategories.categoriesName = RemoveHtmlCharacters(obj.Desc);
                oCategories.SubCatagoryList = ParseSubCatagory(oCategories.categoriesUrl);
                categoriesList.Add(oCategories);
            }
            return categoriesList;
        }

        private List<SubCatagory> ParseSubCatagory(string url)
        {
            List<SubCatagory> subCatagoryList = new List<SubCatagory>();

            #region [URL Scraping]
            //var getHtmlWeb1 = new HtmlWeb();
            //var doc1 = getHtmlWeb1.Load(url);
            //var doc1 = Cache.GetUrl(url);
            browser.Url = url;
            browser.Url = url;
            var doc1 = browser.GetWebRequest();
            #endregion

            HtmlNode node = doc1.DocumentNode.SelectSingleNode("//div[@class='categories']");
            node = node.SelectSingleNode("//ul[@class='subCategory toplvls']");

            var hoteleWebsiteDoc = (from element in node.Descendants("a")
                                    where element.ParentNode.Name.Equals("li") &&
                                    element.Attributes.Contains("href")
                                    select new
                                    {
                                        URL = element.Attributes["href"].Value,
                                        Desc = element.InnerText
                                    }).ToList();

            foreach (var obj in hoteleWebsiteDoc)
            {
                SubCatagory oSubCatagory = new SubCatagory();
                oSubCatagory.subCategoriesUrl = "http://adiglobal.us" + obj.URL.Replace("amp;", ""); ;
                oSubCatagory.subCategoriesName = RemoveHtmlCharacters(obj.Desc);
                oSubCatagory.subSubCatagoryList = ParseSubSubCatagory(oSubCatagory.subCategoriesUrl);
                subCatagoryList.Add(oSubCatagory);
            }

            return subCatagoryList;
        }

        private List<SubSubCatagory> ParseSubSubCatagory(string url)
        {
            List<SubSubCatagory> subSubCatagoryList = new List<SubSubCatagory>();

            #region [URL Scraping]
            //var getHtmlWeb = new HtmlWeb();
            //var doc = getHtmlWeb.Load(url);
            //var doc = Cache.GetUrl(url);
            browser.Url = url;
            var doc = browser.GetWebRequest();
            #endregion


            HtmlNode node = doc.DocumentNode.SelectSingleNode("//ul[@class='subCategory']");
            var hoteleWebsiteDoc = (from element in node.Descendants("a")
                                    where element.ParentNode.Name.Equals("li") &&
                                    element.Attributes.Contains("href")
                                    select new
                                    {
                                        URL = element.Attributes["href"].Value,
                                        Desc = element.InnerText
                                    }).ToList();

            foreach (var obj in hoteleWebsiteDoc)
            {
                SubSubCatagory oSubSubCatagory = new SubSubCatagory();
                oSubSubCatagory.subSubCategoriesUrl = "http://adiglobal.us" + obj.URL.Replace("amp;", ""); ;
                oSubSubCatagory.subSubCategoriesName = RemoveHtmlCharacters(obj.Desc);
                subSubCatagoryList.Add(oSubSubCatagory);
            }

            return subSubCatagoryList;
        }

        
        #endregion

        #region [Utility Methods]
        private String RemoveHtmlCharacters(String text)
        {
            text = text.Replace("&amp;", "&");
            return text;
        }
        #endregion


        #region [Parse Product]
        public List<Product> ParseProducts(String url)
        {
            List<Product> Products = new List<Product>();

            var doc = Cache.GetUrl(url);
            HtmlNodeCollection productCollection = doc.DocumentNode.SelectNodes("//li[@class='productView']");
            HtmlNodeCollection pagesCollection = doc.DocumentNode.SelectNodes("//div[@class='paging']");

            if (!ReferenceEquals(productCollection, null))
            {
                //HtmlNodeCollection productHtmlNode;
                Product product;
                foreach (HtmlNode productNode in productCollection)
                {
                    product = new Product();

                    product.SmallImage = productNode.Descendants("div")
                        .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("product-image"))
                        .FirstOrDefault().Descendants("img").FirstOrDefault().Attributes["src"].Value;

                    var productPrice = productNode.Descendants("div")
                        .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("price"));

                    product.Name = productNode.Descendants("li")
                        .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("product-title"))
                        .FirstOrDefault().Descendants("a").FirstOrDefault().Attributes["title"].Value;

                    product.Url = "http://adiglobal.us" + productNode.Descendants("li")
                        .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("product-title"))
                        .FirstOrDefault().Descendants("a").FirstOrDefault().Attributes["href"].Value;

                    product.VendorName = productNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("vendorName")).FirstOrDefault().InnerHtml;
                    product.VendorModel = productNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("vendorNbr")).FirstOrDefault().InnerHtml.Replace("Model #: ", "");
                    product.PartNumber = productNode.Descendants("li").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("partNbr")).FirstOrDefault().InnerHtml.Replace("ADI #: ", "");

                    GetProductDetails(ref product);

                    Products.Add(product);
                }
            }
            return Products;
        }


        private void GetProductDetails(ref Product product)
        {
            var productDoc = Cache.GetUrl(product.Url);

            product.BigImage = productDoc.DocumentNode.SelectSingleNode("//div[@class='product-img-big']").Descendants("img").FirstOrDefault().Attributes["src"].Value;
            var productDetails = productDoc.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("tab-content overview")).FirstOrDefault();
            var productSpecs = productDoc.DocumentNode.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("tab-content specs")).FirstOrDefault();

            if (!ReferenceEquals(productDetails, null))
            {
                var mainFeatures = productDetails.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("spec-sectionfea")).FirstOrDefault();
                var marketingInfo1 = productDetails.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("spec-sectionmktg")).FirstOrDefault();
                var marketingInfo2 = productDetails.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("spec-sectionmktg")).LastOrDefault();
            }
            if (!ReferenceEquals(productSpecs, null))
            {
                var productSpecsSection = productDetails.Descendants("div").Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("spec-section")).FirstOrDefault();
            }
        }


        #region [Product Specifications]
        public void GetProductSpecification(String ProductID, String ProductName)
        {
            WebClient webclient = new WebClient();
            Uri uristring = null;
            uristring = new Uri("http://adiglobal.us/_vti_bin/requests.asmx/ProductSpecifications");
            webclient.Headers.Add("Content-Type", "application/json; charset=utf-8");
            webclient.Headers["ContentType"] = "application/json";
            string JsonStringParams = String.Format("{{\'productID\':\'{0}\',\'productDefinitionName\':\'{1}\'}}", ProductID, ProductName);
            webclient.UploadStringCompleted += wc_ParseProductSpecs;
            //String responce = webclient.UploadString("http://adiglobal.us/_vti_bin/requests.asmx/ProductSpecifications", JsonStringParams);
            //Post data like this
            webclient.UploadStringAsync(uristring, "POST", JsonStringParams);
        }
        private void wc_ParseProductSpecs(object sender, UploadStringCompletedEventArgs e)
        {
            try
            {

                if (e.Result != null)
                {
                    string responce = e.Result.ToString();
                    //To Do Your functionality
                    DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(responce);
                }
            }
            catch
            {
            }
        }
        #endregion
        #endregion
        
        
    }
}
