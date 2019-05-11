using WebMediaPortfolioTemplate.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMediaPortfolioTemplate.FrontEnd
{
    public partial class Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (CurrentUser == null)
            //{
            //    FormsAuthentication.RedirectToLoginPage();
            //    return;
            //}

            if (Request.QueryString["Id"] == null) Response.Redirect("/Home.aspx");

            ViewState["Id"] = Request.QueryString["Id"].ToString();

            //Get policy description 
            string itemDesc = DBManager.GetCodingDesc(ViewState["Id"].ToString());
            if (itemDesc == "")
            {
                Helper.DisplayAlert(this, "Please make sure that item exists and has a description");
                Response.Redirect("/Coding.aspx");
            }

            bindGrid(ViewState["Id"].ToString());

            //string pageTitle = "Item Details";
            lblTitle.Text = itemDesc;
            //lblPageTitle_sub.Text = pageTitle;
            //lblPageDesc.Text = "Add and view information for " + pageTitle.ToLower();
            //lblPageDesc_sub.Text = "Please choose a " + pageTitle.ToLower() + " to view, edit or delete information.";


        }

        private void bindGrid(string id)
        {


            SqlConnection Conn = new SqlConnection(AppConstants.connStr);
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            //== TODO: Change table name
            Comm.CommandText = @"SELECT * FROM COM_CONTENT WHERE CODING_ID=@Id";
            Comm.Parameters.AddWithValue("@Id", id);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            lstItems.DataSource = dt;
            lstItems.DataBind();
        }

    }
}