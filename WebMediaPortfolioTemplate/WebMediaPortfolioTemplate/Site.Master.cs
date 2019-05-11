using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMediaPortfolioTemplate.Models;
using WebMediaPortfolioTemplate.Classes;

//Admin master page
namespace WebMediaPortfolioTemplate
{
    public partial class Site : System.Web.UI.MasterPage
    {
        UserAccount _currentUser;

        public string RequestAlert
        {
            get
            {
                if (Session["RequestAlert"] != null)
                    return Session["RequestAlert"].ToString();
                else
                    return "";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!Page.IsPostBack)
            {
                Session["Reset"] = true;
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SessionAlert", "SessionExpireAlert(" + timeout + ");", true);

                _currentUser = Session["CurrentUser"] as UserAccount;
                //Login Check
                if (!this.Page.User.Identity.IsAuthenticated || _currentUser == null)
                {
                    FormsAuthentication.RedirectToLoginPage();
                    return;
                }

            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("/Account/AdminLogin.aspx");
        }
    }
}