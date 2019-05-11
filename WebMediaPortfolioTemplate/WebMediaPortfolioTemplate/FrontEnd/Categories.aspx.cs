using WebMediaPortfolioTemplate.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebMediaPortfolioTemplate.FrontEnd
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] == null) Response.Redirect("/Home.aspx");

            ViewState["Id"] = Request.QueryString["Id"].ToString();

            //Get policy description 
            string itemDesc = DBManager.GetCodingTypeDesc(ViewState["Id"].ToString());
            if (itemDesc == "")
            {
                Helper.DisplayAlert(this, "Please make sure that item exists and has a description");
                Response.Redirect("/Coding.aspx");
            }

            bindGrid(ViewState["Id"].ToString());

            lblTitle.Text = itemDesc;
            //foreach (RepeaterItem r in lstItems.Items)
            //{
            //    ListView lst = (ListView)r.FindControl("lstMenuSub");
            //    Label lbl = (Label)r.FindControl("lblId");
            //}
        }

        private void bindGrid(string codingType)
        {

            SqlConnection Conn = new SqlConnection(AppConstants.connStr);
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.CommandText = @"SELECT dbo.COM_CODING.CODING_ID, dbo.COM_CODING.CodingType_ID,
dbo.COM_CODING.CODING_DESCRIPTION,dbo.COM_CODING.URL, dbo.COM_CODING.VALID_FROM, dbo.COM_CODING.VALID_TO,
dbo.COM_CODING.CREATE_BY, dbo.COM_CODING.CREATE_DATE, dbo.COM_CODING.UPDATE_BY, 
dbo.COM_CODING.UPDATE_DATE FROM dbo.COM_CODING  WHERE CodingType_ID=@CodingType";
            Comm.Parameters.AddWithValue("@CodingType", codingType);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            lstItems.DataSource = dt;
            lstItems.DataBind();
        }

    }
}