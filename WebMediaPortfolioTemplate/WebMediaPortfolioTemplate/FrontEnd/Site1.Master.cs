using WebMediaPortfolioTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMediaPortfolioTemplate.FrontEnd
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        UserAccount _currentUser;

        protected void Page_Load(object sender, EventArgs e)
        {
            //_currentUser = Session["CurrentUser"] as UserAccount;
            ////Login Check
            //if (!this.Page.User.Identity.IsAuthenticated || _currentUser == null)
            //{
            //    FormsAuthentication.RedirectToLoginPage();
            //    return;
            //}

            //if (_currentUser.Id == "1")
            //    lnkConfig.Visible = true;
        }
    }
}