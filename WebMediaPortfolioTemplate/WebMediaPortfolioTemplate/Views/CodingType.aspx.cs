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

//Coding type consists of a user defined input linked to Coding table
namespace WebMediaPortfolioTemplate.Views
{
    public partial class CodingType : BaseWebForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Check if user is admin
                if (_currentUser.Id != "1")
                {
                    btnAdd.Visible = false;
                    btnSave.Visible = false;
                }

                //Fill page contents
                string pageTitle = "Main Catalog";
                lblPageTitle.Text = "Setup - " + pageTitle;
                lblPageTitle_sub.Text = pageTitle;
                lblPageDesc.Text = "Add and view information for " + pageTitle.ToLower();
                lblPageDesc_sub.Text = "Please choose a " + pageTitle.ToLower() + " to view, edit or delete information.";

                //Bind data grid
                bindGrid();
            }
        }

        private void bindGrid()
        {
            SqlConnection Conn = new SqlConnection(AppConstants.connStr);
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.CommandText = @"SELECT * FROM COM_CODINGTYPE ORDER BY CODINGTYPE_ID";

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
                Comm.CommandText = "Delete from COM_CODINGTYPE WHERE CODINGTYPE_ID=@CODINGTYPE_ID";
                Comm.Parameters.AddWithValue("@CODINGTYPE_ID", e.CommandArgument);
                Conn.Open();
                Comm.ExecuteNonQuery();
                Conn.Close();

                Comm.Parameters.Clear();
                Comm.CommandText = "Delete from COM_CODING WHERE CODINGTYPE_ID=@CODINGTYPE_ID";
                Comm.Parameters.AddWithValue("@CODINGTYPE_ID", e.CommandArgument);
                Conn.Open();
                Comm.ExecuteNonQuery();
                Conn.Close();

                bindGrid();
            }

        }

        //Insert/update
        protected void btnSave_Command(object sender, CommandEventArgs e)
        {
            if (hdRecId.Value == "")
            {
                using (SqlConnection Conn = new SqlConnection(AppConstants.connStr))
                {
                    using (SqlCommand Comm = new SqlCommand())
                    {
                        Comm.CommandType = CommandType.StoredProcedure;
                        Comm.CommandText = "SP_COM_CODINGTYPE_INSERT";
                        Comm.Parameters.AddWithValue("@DESCRIPTION", txtDesc.Text);

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
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
                SqlCommand Comm = new SqlCommand("", Conn);

                Comm.CommandType = CommandType.StoredProcedure;
                Comm.CommandText = "SP_COM_CODINGTYPE_UPDATE";
                Comm.Parameters.AddWithValue("@CODINGTYPE_ID", hdRecId.Value);
                Comm.Parameters.AddWithValue("@DESCRIPTION", txtDesc.Text);

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
                    Comm.CommandText = "UPDATE COM_CODINGTYPE SET URL=@URL WHERE CODINGTYPE_ID=@CODINGTYPE_ID";
                    Comm.Parameters.AddWithValue("@CODINGTYPE_ID", hdRecId.Value);
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

            bindGrid();
        }

        [WebMethod]
        public static CodingType_Model GetRecord(string id)
        {
            //Get the coding type data from sq; voew 
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.CommandText = @"SELECT * FROM VW_COM_CODINGTYPE
                                WHERE CODINGTYPE_ID=@Id";
            Comm.Parameters.AddWithValue("@Id", id);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            CodingType_Model record = new CodingType_Model()
            {
                CODINGTYPE_ID = dt.Rows[0]["CODINGTYPE_ID"].ToString(),
                DESCRIPTION = dt.Rows[0]["DESCRIPTION"].ToString(),
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


    }
}