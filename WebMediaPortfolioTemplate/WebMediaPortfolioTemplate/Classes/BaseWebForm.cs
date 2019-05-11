using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMediaPortfolioTemplate.Classes;
using WebMediaPortfolioTemplate.Models;

//Base class for all pages that contains user validation and account information
namespace WebMediaPortfolioTemplate.Classes
{
    public class BaseWebForm : Page
    {
        public UserAccount _currentUser;
        public UserAccount CurrentUser
        {
            get
            {
                return _currentUser;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //Check current session user
            if (Session["CurrentUser"] == null)
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }
            else
            {
                _currentUser = Session["CurrentUser"] as UserAccount;
            }
            
            //If null redirect to login page
            if (_currentUser == null)
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

        }

        //Will be utilized when we need to clear the session
        [WebMethod]
        public static void ClearSession(string name)
        {
            HttpContext.Current.Session[name] = "";
        }
    }
}