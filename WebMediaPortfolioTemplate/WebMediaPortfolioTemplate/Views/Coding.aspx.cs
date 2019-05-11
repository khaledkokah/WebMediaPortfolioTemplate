using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMediaPortfolioTemplate.Classes;
using WebMediaPortfolioTemplate.Models;

//Coding refers to numbering of items in the items database
namespace WebMediaPortfolioTemplate.Views
{
    public partial class Coding : BaseWebForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Simple verification for admin (admin id should be 1)
                if (_currentUser.Id != "1")
                {
                    btnAdd.Visible = false;
                    btnSave.Visible = false;
                }

                //Page labels 
                string pageTitle = "Items Catalog";
                lblPageTitle.Text = "Setup - " + pageTitle;
                lblPageTitle_sub.Text = pageTitle;
                lblPageDesc.Text = "Add and view information for " + pageTitle.ToLower();
                lblPageDesc_sub.Text = "Please choose a " + pageTitle.ToLower() + " to view, edit or delete information.";

                btnAdd.Visible = false;

                //Bind Coding Type
                BindCodingType();
            }
        }

        //Get the list of coding typesssss
        private void BindCodingType()
        {
            SqlConnection Conn = new SqlConnection(AppConstants.connStr);
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.CommandText = "Select * FROM COM_CODINGTYPE";
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            ddlCodingType.DataSource = dt;
            ddlCodingType.DataValueField = "CodingType_ID";
            ddlCodingType.DataTextField = "Description";
            ddlCodingType.DataBind();
        }

        //Change lables and bind gridView based on codingType selection 
        protected void ddlCodingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCodingType.SelectedIndex != 0)
            {
                lblPageTitle_sub.Text = ddlCodingType.SelectedItem.Text;
                bindGrid(ddlCodingType.SelectedValue);
                btnAdd.Visible = true;
            }
            else
            {
                lblPageTitle_sub.Text = "";
                GridView1.DataSource = null;
                GridView1.DataBind();
                btnAdd.Visible = false;
            }
        }

        //Bind gridView with selected coding data
        private void bindGrid(string codingType)
        {
            SqlConnection Conn = new SqlConnection(AppConstants.connStr);
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.CommandText = @"SELECT dbo.COM_CODING.CODING_ID, dbo.COM_CODING.CodingType_ID,
                dbo.COM_CODING.CODING_DESCRIPTION,dbo.COM_CODING.URL, dbo.COM_CODING.VALID_FROM, dbo.COM_CODING.VALID_TO,
                dbo.COM_CODING.CREATE_BY, dbo.COM_CODING.CREATE_DATE, dbo.COM_CODING.UPDATE_BY, 
                dbo.COM_CODING.UPDATE_DATE, dbo.COM_USER.USERNAME as CreateByUser, COM_USER_1.USERNAME AS UpdateByUser
                FROM dbo.COM_CODING INNER JOIN
                dbo.COM_USER ON dbo.COM_CODING.CREATE_BY = dbo.COM_USER.USER_ID INNER JOIN
                dbo.COM_USER AS COM_USER_1 ON dbo.COM_CODING.UPDATE_BY = COM_USER_1.USER_ID
                WHERE CodingType_ID=@CodingType";
            Comm.Parameters.AddWithValue("@CodingType", codingType);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbConn"].ToString());
                SqlCommand Comm = new SqlCommand("", Conn);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                //Delete from all related tables 
                Comm.CommandText = "Delete from COM_CODING WHERE CODING_ID=@CODING_ID";
                Comm.Parameters.AddWithValue("@CODING_ID", e.CommandArgument);
                Conn.Open();
                Comm.ExecuteNonQuery();
                Conn.Close();

                Comm.Parameters.Clear();
                Comm.CommandText = "Delete from COM_CONTENT WHERE CODING_ID=@CODING_ID";
                Comm.Parameters.AddWithValue("@CODING_ID", e.CommandArgument);
                Conn.Open();
                Comm.ExecuteNonQuery();
                Conn.Close();

                //Rebind
                bindGrid(ddlCodingType.SelectedValue);
            }
        }


        //Save changes (insert/update)
        protected void btnSave_Command(object sender, CommandEventArgs e)
        {
            //In new entry
            if (hdRecId.Value == "")
            {
                using (SqlConnection Conn = new SqlConnection(AppConstants.connStr))
                {
                    using (SqlCommand Comm = new SqlCommand())
                    {
                        Comm.CommandType = CommandType.StoredProcedure;
                        Comm.CommandText = "SP_COM_CODING_INSERT";
                        Comm.Parameters.AddWithValue("@CODING_DESCRIPTION", txtDesc.Text);
                        Comm.Parameters.AddWithValue("@CODINGTYPE_ID", ddlCodingType.SelectedValue);

                        DBManager.AddStampFields(Comm, _currentUser, modalStamp, false);

                        //Insert file into attachments
                        if (fileUpload.HasFile)
                        {
                            //Get file name
                            var name = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
                            HttpPostedFile postedFile = fileUpload.PostedFile;
                            string fileName = Path.GetFileName(postedFile.FileName);

                            Comm.Parameters.AddWithValue("@URL", "../Upload/" + name + fileName);

                            //Resize image
                            string extension = Path.GetExtension(fileUpload.FileName);
                            if (extension.ToLower() == ".bmp" || extension.ToLower() == ".png" || extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg")
                            {
                                Stream strm = fileUpload.PostedFile.InputStream;
                                using (var image = System.Drawing.Image.FromStream(strm))
                                {
                                    int newWidth = 800; // New Width of Image in Pixel  
                                    int newHeight = 500; // New Height of Image in Pixel  
                                    var thumbImg = new Bitmap(newWidth, newHeight);
                                    var thumbGraph = Graphics.FromImage(thumbImg);
                                    thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                    thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                    thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                    thumbGraph.DrawImage(image, imgRectangle);
                                    // Save the file  
                                    string targetPath = HttpContext.Current.Server.MapPath("~/Upload/") + name + fileName;
                                    thumbImg.Save(targetPath, image.RawFormat);
                                }
                            }
                        }
                        else
                            Comm.Parameters.AddWithValue("@URL", DBNull.Value);

                        Comm.Connection = Conn;
                        Conn.Open();
                        Comm.ExecuteNonQuery();
                        Conn.Close();
                    }
                }
            }
            else
            {
                //Update 
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
                SqlCommand Comm = new SqlCommand("", Conn);

                Comm.CommandType = CommandType.StoredProcedure;
                Comm.CommandText = "SP_COM_CODING_UPDATE";
                Comm.Parameters.AddWithValue("@CODING_ID", hdRecId.Value);
                Comm.Parameters.AddWithValue("@CODING_DESCRIPTION", txtDesc.Text);

                DBManager.AddStampFields(Comm, _currentUser, modalStamp, true);

                Comm.Connection = Conn;
                Conn.Open();
                Comm.ExecuteNonQuery();
                Conn.Close();

                //Update file into attachments
                if (fileUpload.HasFile)
                {
                    //Get file name
                    var name = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
                    HttpPostedFile postedFile = fileUpload.PostedFile;
                    string fileName = Path.GetFileName(postedFile.FileName);
                    Comm.Parameters.Clear();
                    Comm.CommandType = CommandType.Text;
                    Comm.CommandText = "UPDATE COM_CODING SET URL=@URL WHERE CODING_ID=@CODING_ID";
                    Comm.Parameters.AddWithValue("@CODING_ID", hdRecId.Value);
                    Comm.Parameters.AddWithValue("@URL", "../Upload/" + name + fileName);

                    Comm.Connection = Conn;
                    Conn.Open();
                    Comm.ExecuteNonQuery();
                    Conn.Close();

                    //Resize image
                    string extension = Path.GetExtension(fileUpload.FileName);
                    if (extension.ToLower() == ".bmp" || extension.ToLower() == ".png" || extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg")
                    {
                        Stream strm = fileUpload.PostedFile.InputStream;
                        using (var image = System.Drawing.Image.FromStream(strm))
                        {
                            int newWidth = 800; // New Width of Image in Pixel  
                            int newHeight = 500; // New Height of Image in Pixel  
                            var thumbImg = new Bitmap(newWidth, newHeight);
                            var thumbGraph = Graphics.FromImage(thumbImg);
                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                            thumbGraph.DrawImage(image, imgRectangle);
                            // Save the file  
                            string targetPath = HttpContext.Current.Server.MapPath("~/Upload/") + name + fileName;
                            thumbImg.Save(targetPath, image.RawFormat);
                        }
                    }
                }
            }

            //Rebind
            bindGrid(ddlCodingType.SelectedValue);
        }

        //Web method to be called when pop up view 
        [WebMethod]
        public static Coding_Model GetRecord(string id)
        {
            //Get the companies 
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.CommandText = @"SELECT * FROM VW_COM_CODING
                                WHERE CODING_ID=@Id";
            Comm.Parameters.AddWithValue("@Id", id);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            //Fill model data
            Coding_Model record = new Coding_Model()
            {
                Coding_Id = dt.Rows[0]["CODING_ID"].ToString(),
                Coding_Description = dt.Rows[0]["CODING_DESCRIPTION"].ToString(),
                //== FIXED FIELDS ==//
                VALID_FROM = DBManager.ReadDate(dt.Rows[0]["VALID_FROM"].ToString(), "yyyy/MM/dd"),
                VALID_TO = DBManager.ReadDate(dt.Rows[0]["VALID_TO"].ToString(), "yyyy/MM/dd"),
                CREATE_BY = dt.Rows[0]["CreateByUser"].ToString(),
                CREATE_DATE = DBManager.ReadDate(dt.Rows[0]["CREATE_DATE"].ToString(), "yyyy/MM/dd"),
                UPDATE_BY = dt.Rows[0]["UpdateByUser"].ToString(),
                UPDATE_DATE = DBManager.ReadDate(dt.Rows[0]["UPDATE_DATE"].ToString(), "yyyy/MM/dd")
            };

            return record;
        }

        //Hide delete button from gridView if user is not admin
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (_currentUser.Id != "1")
            {
                e.Row.Cells[7].Visible = false;
            }
        }
    }
}