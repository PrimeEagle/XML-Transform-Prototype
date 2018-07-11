using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Net;

namespace WebApplication6 {
	public class WebForm1 : System.Web.UI.Page {
		protected System.Web.UI.WebControls.TextBox TextBox1;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Button Button2;
		protected System.Web.UI.WebControls.Button Button1;
	
		private void Page_Load(object sender, System.EventArgs e) {
		}
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Button2.Click += new System.EventHandler(this.Button2_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Button1_Click(object sender, System.EventArgs e) {
			try{
				Label1.Text = "";
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(TextBox1.Text);

				XslTransform transForm = new XslTransform();
				XmlElement root = xmlDoc.DocumentElement;
				XPathNavigator nav = root.CreateNavigator();

				XmlUrlResolver resolver = new XmlUrlResolver();
//
//				//Doesn't work with integrated security (therefore, CredentialCache.DefaultCredentials
//				//won't work). We have to use "Basic" authentication.
//
//				CredentialCache myCache = new CredentialCache();
//				NetworkCredential myCred = Utility.GetSvcAcctCredentials();
//				myCache.Add(xsltUri, "Basic", myCred);
				resolver.Credentials = CredentialCache.DefaultCredentials;

				transForm.Load(Server.MapPath("DocumentList_Localize.xslt"));
				//transForm.Load(xsltUri.AbsoluteUri.ToString());

				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				System.IO.TextWriter tw = (System.IO.TextWriter) new System.IO.StringWriter(sb);
				System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

				transForm.Transform(nav, null, hw, resolver);

				TextBox1.Text = sb.ToString();
			} catch(Exception exc) {
				Label1.Text = exc.Message;
			}
		}

		private void Button2_Click(object sender, System.EventArgs e) {
			try{
				Label1.Text = "";
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(TextBox1.Text);
			} catch(Exception exc) {
				Label1.Text = exc.Message;
			}
		}
	}
}
