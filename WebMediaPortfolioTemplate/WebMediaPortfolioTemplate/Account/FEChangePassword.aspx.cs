using WebMediaPortfolioTemplate.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMediaPortfolioTemplate.FrontEnd
{
    public partial class FEChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }

        protected void OnChangingPassword(object sender, LoginCancelEventArgs e)
        {
            if (!ChangePassword1.CurrentPassword.Equals(ChangePassword1.NewPassword, StringComparison.CurrentCultureIgnoreCase))
            {
                //Validate password

                int rowsAffected = 0;
                string query = "UPDATE [COM_USER] SET [Password] = @NewPassword WHERE [Username] = @Username AND [Password] = @CurrentPassword";
                string constr = AppConstants.connStr;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.Parameters.AddWithValue("@Username", this.Page.User.Identity.Name);
                            cmd.Parameters.AddWithValue("@CurrentPassword", Encryption.GetPass(this.Page.User.Identity.Name, ChangePassword1.CurrentPassword));
                            cmd.Parameters.AddWithValue("@NewPassword", Encryption.Hash(ChangePassword1.NewPassword));
                            cmd.Connection = con;
                            con.Open();
                            rowsAffected = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    if (rowsAffected > 0)
                    {
                        lblMessage.ForeColor = Color.Green;
                        lblMessage.Text = "Password has been changed successfully.";
                    }
                    else
                    {
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Text = "Password does not match with our database records.";
                    }
                }
            }
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Old Password and New Password must not be equal.";
            }

            e.Cancel = true;
        }
    }
}