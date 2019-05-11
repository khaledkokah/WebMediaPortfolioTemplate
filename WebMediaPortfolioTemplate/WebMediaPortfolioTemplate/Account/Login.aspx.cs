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
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void ValidateUser(object sender, EventArgs e)
        {
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
                    // Session["LoggedInUserID"] = DBManager.getUserId(Login1.UserName);
                    // var membershipUser = DBManager.GetUser(Login1.UserName);

                    //Get and set session data for the logged-in user
                    if (SetUserSession(userId))
                        FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
                    else
                        Login1.FailureText = "An unexpected error has occurred, please try again.";

                    break;
            }
        }

        //Output
        private bool SetUserSession(string userId)
        {
            //Get the dropdownlist
            // DropDownList ddlCompany = (DropDownList)(Login1.Controls[0].FindControl("ddlCompany"));

            //Save the original login user in case of onBehalf
            Session["OriginalLoginUser"] = userId;

            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.Parameters.Clear();

            Comm.CommandText = "Select * from [COM_USER] WHERE [USER_ID]=@Id";
            Comm.Parameters.AddWithValue("Id", userId);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            UserAccount acc = null;
            if (dt.Rows.Count > 0)
            {
                acc = new UserAccount
                {
                    Id = dt.Rows[0]["USER_ID"].ToString(),
                    Name = dt.Rows[0]["UserName"].ToString(),
                    UserName = dt.Rows[0]["UserName"].ToString(),
                    Email = dt.Rows[0]["Email"].ToString()
                    //CompanyId = compId,
                    //CompanyName = compName,
                    //DepartmentId = deptId,
                    //DepartmentName = deptName,
                    //JobId = jobId,
                    //JobName = jobName,
                    //PositionId = positionId,
                    //Level = jobLevel,
                    //isOnBehalf = isOnBehalf,
                    //JobType = jobType,
                    //IsCritical = Convert.ToBoolean(jobCritical),
                    //IsImportant = Convert.ToBoolean(jobImportant)
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