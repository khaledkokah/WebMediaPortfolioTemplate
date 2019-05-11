using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//This is the default page for front end
namespace WebMediaPortfolioTemplate
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Get category types (COM_CODINGTYPE table)
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbConn"].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.CommandText = "Select * from COM_CODINGTYPE";
            da.SelectCommand = Comm;
            da.Fill(dt);
            lstItems.DataSource = dt;
            lstItems.DataBind();
        }
    }
}