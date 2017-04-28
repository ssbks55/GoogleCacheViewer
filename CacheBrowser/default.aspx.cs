using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CacheBrowser
{
    public partial class Home : System.Web.UI.Page
    {

        #region Properties
        public List<String> blockedIPAddresses
        {
            get
            {
                if (ViewState["blockedIPAddresses"] != null)
                {
                    return (ViewState["blockedIPAddresses"]) as List<String>;
                }
                return new List<String>();
            }
            set
            {
                ViewState["blockedIPAddresses"] = value;
            }
        } 
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblIP.Text = Request.UserHostAddress;
                lblHostName.Text = Request.UserHostName;

                blockedIPAddresses = System.IO.File.ReadAllLines("\\CacheBrowser\\Resources\\BlockedIPAddresses.txt").ToList();
                if (blockedIPAddresses.Contains(Request.UserHostAddress))
                {
                    lblError.Text = "Your IP is blocked. Contact Administrator.";
                    lblError.Visible = true;
                    txtUrl.Enabled = false;
                    btnSubmit.Enabled = false;
                }
            }
        } 
        #endregion

        #region Control Events

        /// <summary>
        /// Search button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = Request.UserHostAddress;
                string hostname = Request.UserHostName;

                WriteToFile("------------------------------------------Koogle Log ---" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "-------------------------------------- ");
                WriteToFile("Search Query: " + txtUrl.Text.Trim());
                WriteToFile(String.Format("IP: {0} HostName: {1}", ip, hostname));
                if (!blockedIPAddresses.Contains(Request.UserHostAddress))
                {

                    lblError.Visible = false;
                    iframeViewer.Src = string.Empty;
                    iframeViewer.Visible = false;
                    string url = "http://webcache.googleusercontent.com/search?q=cache:" + txtUrl.Text.Trim().ToLower();
                    string path = "~/Dummy.html";
                    File.WriteAllText(Server.MapPath(path), String.Empty);
                    var webRequest = WebRequest.Create(url);
                    using (var response = webRequest.GetResponse())
                    using (var content = response.GetResponseStream())
                    using (var reader = new StreamReader(content))
                    {
                        String strContent = reader.ReadToEnd();
                       // String[] asaa = Regex.Split(strContent, "<!DOCTYPE html>"); // strContent.Split("<!DOCTYPE");

                        HtmlDocument resultat = new HtmlDocument();
                        resultat.LoadHtml(strContent);
                        resultat.GetElementbyId("google-cache-hdr").RemoveAll();
                        using (StreamWriter _testData = new StreamWriter(Server.MapPath(path), true))
                        {
                            _testData.WriteLine(resultat.DocumentNode.OuterHtml); // Write the file.
                        }
                    }
                    iframeViewer.Visible = true;
                    iframeViewer.Src = "Dummy.html";
                }

            }
            catch (Exception)
            {
                iframeViewer.Visible = true;
                iframeViewer.Src = "Error.html";
            }

        }

        #endregion

        #region Methods

        private bool IsUserActivityLegal()
        {
            //todo...
            return true;
        }

        /// <summary>
        /// Create Log Files
        /// </summary>
        /// <param name="text"></param>
        private void WriteToFile(string text)
        {
            string path = "\\CacheBrowser\\Resources\\KoogleUserSearchHistory.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(text);
                writer.Close();
            }
        } 
        #endregion
    }
}