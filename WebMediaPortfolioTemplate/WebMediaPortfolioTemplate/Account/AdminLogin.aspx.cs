using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using WebMediaPortfolioTemplate.Classes;
using System.Threading;
using WebMediaPortfolioTemplate.Models;

namespace WebMediaPortfolioTemplate
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        //Simple user validation
        protected void ValidateUser(object sender, EventArgs e)
        {
            //Call valiadte user stored procedure from dbmanager
            string userId = DBManager.ValidateUser(Login1.UserName, Login1.Password);
            switch (userId)
            {
                case "-1":
                    Login1.FailureText = "Username and/or password is incorrect.";
                    break;
                case "-2":
                    Login1.FailureText = "Account has not been activated.";
                    break;
                default:
                    //If user is validated
                    if (SetUserSession(userId))
                    {
                        //Set authentication cookie
                        FormsAuthentication.SetAuthCookie(Login1.UserName, Login1.RememberMeSet);
                        Response.Redirect("/AdminHome.aspx");
                    }
                    else
                        Login1.FailureText = "An unexpected error has occurred, please try again.";
                    break;
            }
        }

        //Set user session
        private bool SetUserSession(string userId)
        {
            //Connect to sql database
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.Parameters.Clear();

            Comm.CommandText = "Select * from [COM_USER] WHERE [USER_ID]=@Id";
            Comm.Parameters.AddWithValue("Id", userId);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            //Basic user account information
            UserAccount acc = null;
            if (dt.Rows.Count > 0)
            {
                acc = new UserAccount
                {
                    Id = dt.Rows[0]["USER_ID"].ToString(),
                    Name = dt.Rows[0]["UserName"].ToString(),
                    UserName = dt.Rows[0]["UserName"].ToString(),
                    Email = dt.Rows[0]["Email"].ToString()
                };
            }
            else
            {
                return false;
            }
            //Save in Session
            Session["CurrentUser"] = acc;

            return true;
        }

    }
}