using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using WebMediaPortfolioTemplate.Models;
using WebMediaPortfolioTemplate.Models;
using System.Data.SqlClient;
using System.Reflection;
using WebMediaPortfolioTemplate.Classes;
using System.ComponentModel;

//This is the administrator page for add/update/delete all categories and contents for the front end website
namespace WebMediaPortfolioTemplate
{
    public partial class AdminHome : System.Web.UI.Page
    {
        //Variable that will contain all the information about the logged in user
        UserAccount _currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get the current logged in user
            _currentUser = Session["CurrentUser"] as UserAccount;
            if (!Page.IsPostBack)
            {
                //Login validation check
                if (!this.Page.User.Identity.IsAuthenticated || _currentUser == null)
                {
                    FormsAuthentication.RedirectToLoginPage();
                    return;
                }
            }
        }
    }
}