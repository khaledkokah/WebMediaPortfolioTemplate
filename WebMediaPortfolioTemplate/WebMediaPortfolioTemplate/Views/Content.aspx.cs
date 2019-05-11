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

//Content class links the children items of each Coding table rows
namespace WebMediaPortfolioTemplate.Views
{
    public partial class Content : BaseWebForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Check if coding id is numeric
                if (Request.QueryString["Id"] == null) Response.Redirect("/Coding.aspx");

                //Save codingId in ViewState
                ViewState["Id"] = Request.QueryString["Id"].ToString();

                //Get coding description 
                string itemDesc = DBManager.GetCodingDesc(ViewState["Id"].ToString());
                if (itemDesc == "")
                {
                    //Invalid coding Id
                    Helper.DisplayAlert(this, "Please make sure that item exists and has a description");
                    Response.Redirect("/Coding.aspx");
                }

                //Fill page contents
                string pageTitle = "Item Details";
                lblPageTitle.Text = "Item Details for Item: " + itemDesc + "<br/>";
                lblPageTitle_sub.Text = pageTitle;
                lblPageDesc.Text = "Add and view information for " + pageTitle.ToLower();
                lblPageDesc_sub.Text = "Please choose a " + pageTitle.ToLower() + " to view, edit or delete information.";

                //Fill any related combo boxes
                // FillCombos();

                //Bind main gridview/repeater/list
                if (ViewState["Id"] == null)
                    Response.Redirect("/Account/AdminLogin.aspx");
                bindGrid(ViewState["Id"].ToString());
            }
        }

        //Binding gridView
        private void bindGrid(string id)
        {
            SqlConnection Conn = new SqlConnection(AppConstants.connStr);
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                //Get content data based on coding id
                Comm.CommandText = @"SELECT * FROM COM_CONTENT WHERE CODING_ID=@Id AND CREATE_BY=@CurrUser";
                Comm.Parameters.AddWithValue("@Id", id);

                if (CurrentUser == null)
                {
                    Response.Redirect("/Account/AdminLogin.aspx");
                    return;
                }
                Comm.Parameters.AddWithValue("@CurrUser", CurrentUser.Id);
            }
            catch
            {
                Response.Redirect("/Account/AdminLogin.aspx");
                return;
            }

            Da.SelectCommand = Comm;
            Da.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        //Insert/update
        protected void btnSave_Command(object sender, CommandEventArgs e)
        {
            //New record
            if (hdRecId.Value == "")
            {
                using (SqlConnection Conn = new SqlConnection(AppConstants.connStr))
                {
                    using (SqlCommand Comm = new SqlCommand())
                    {
                        Comm.CommandType = CommandType.Text;
                        Comm.CommandText = @"INSERT INTO COM_CONTENT(CODING_ID,DESCRIPTION,URL,VIDEO_URL
                                    ,CREATE_BY,CREATE_DATE,UPDATE_BY,UPDATE_DATE)
                                    VALUES(@CODING_ID,@DESCRIPTION,@URL,@VIDEO_URL,@CREATE_BY,@CREATE_DATE,@UPDATE_BY,@UPDATE_DATE)";

                        Comm.Parameters.AddWithValue("@CODING_ID", ViewState["Id"].ToString());
                        Comm.Parameters.AddWithValue("@DESCRIPTION", txtDesc.Text);
                        Comm.Parameters.AddWithValue("@VIDEO_URL", txtVideoUrl.Text);

                        if (txtVideoUrl.Text != "")
                            Comm.Parameters.AddWithValue("@URL", txtVideoUrl.Text);
                        else
                        {
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
                                else
                                {
                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/Upload/") + name + fileName);
                                }
                            }
                            else
                            {
                                Comm.Parameters.AddWithValue("@URL", DBNull.Value);
                            }
                        }

                        //== FIXED STAMP FIELDS ==
                        DBManager.AddStampFields(Comm, _currentUser, modalStamp, false);

                        Comm.Connection = Conn;
                        Conn.Open();
                        Comm.ExecuteNonQuery();
                        Conn.Close();
                    }
                }
            }
            else
            {
                //Existing record
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
                SqlCommand Comm = new SqlCommand("", Conn);

                Comm.CommandText = "UPDATE COM_CONTENT SET DESCRIPTION=@DESCRIPTION,VIDEO_URL=@VIDEO_URL,UPDATE_BY=@UPDATE_BY,UPDATE_DATE=@UPDATE_DATE WHERE CONTENT_ID=@CONTENT_ID";

                Comm.Parameters.AddWithValue("CONTENT_ID", hdRecId.Value);

                Comm.Parameters.AddWithValue("@DESCRIPTION", txtDesc.Text);
                Comm.Parameters.AddWithValue("@VIDEO_URL", txtVideoUrl.Text);

                //== FIXED STAMP FIELDS ==
                DBManager.AddStampFields(Comm, _currentUser, modalStamp, true);
                Comm.Connection = Conn;
                Conn.Open();
                Comm.ExecuteNonQuery();
                Conn.Close();

                if (txtVideoUrl.Text != "")
                    Comm.Parameters.AddWithValue("@URL", txtVideoUrl.Text);
                else
                {
                    //Update file into attachments
                    if (fileUpload.HasFile)
                    {
                        //Get file name
                        var name = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
                        HttpPostedFile postedFile = fileUpload.PostedFile;
                        string fileName = Path.GetFileName(postedFile.FileName);
                        Comm.Parameters.Clear();
                        Comm.CommandText = "UPDATE COM_CONTENT SET URL=@URL WHERE CONTENT_ID=@CONTENT_ID";
                        Comm.Parameters.AddWithValue("@CONTENT_ID", hdRecId.Value);
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
                        else
                        {
                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/Upload/") + name + fileName);
                        }

                        Comm.Connection = Conn;
                        Conn.Open();
                        Comm.ExecuteNonQuery();
                        Conn.Close();
                    }
                }
            }

            //Rebind gridView
            bindGrid(ViewState["Id"].ToString());
        }

        //GetRecord will be called by ajax to get all records for the selected content
        [WebMethod]
        public static Content_Model GetRecord(string id)
        {
            SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[AppConstants.connName].ToString());
            SqlCommand Comm = new SqlCommand("", Conn);
            SqlDataAdapter Da = new SqlDataAdapter();
            DataTable dt = new DataTable();

            Comm.CommandText = @"SELECT dbo.COM_User.USERNAME AS CreateByUser, COM_USER_1.USERNAME AS UpdateByUser, dbo.COM_CONTENT.*
                        FROM  dbo.COM_User AS COM_USER_1 RIGHT OUTER JOIN
                        dbo.COM_CONTENT ON COM_USER_1.USER_ID = dbo.COM_CONTENT.UPDATE_BY LEFT OUTER JOIN
                        dbo.COM_User ON dbo.COM_CONTENT.CREATE_BY = dbo.COM_User.USER_ID where CONTENT_ID=@Id";
            Comm.Parameters.AddWithValue("@Id", id);
            Da.SelectCommand = Comm;
            Da.Fill(dt);

            Content_Model record = new Content_Model()
            {
                DESCRIPTION = dt.Rows[0]["DESCRIPTION"].ToString(),
                CONTENT_ID = dt.Rows[0]["CONTENT_ID"].ToString(),
                VIDEO_URL = dt.Rows[0]["VIDEO_URL"].ToString(),
                VALID_FROM = DBManager.ReadDate(dt.Rows[0]["VALID_FROM"].ToString(), "yyyy/MM/dd"),
                VALID_TO = DBManager.ReadDate(dt.Rows[0]["VALID_TO"].ToString(), "yyyy/MM/dd"),
                CREATE_BY = dt.Rows[0]["CreateByUser"].ToString(),
                CREATE_DATE = DBManager.ReadDate(dt.Rows[0]["CREATE_DATE"].ToString(), "yyyy/MM/dd"),
                UPDATE_BY = dt.Rows[0]["UpdateByUser"].ToString(),
                UPDATE_DATE = DBManager.ReadDate(dt.Rows[0]["UPDATE_DATE"].ToString(), "yyyy/MM/dd")
            };

            return record;
        }

        //Deletion of entry
        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                SqlConnection Conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbConn"].ToString());
                SqlCommand Comm = new SqlCommand("", Conn);
                SqlDataAdapter Da = new SqlDataAdapter();
                DataTable dt = new DataTable();

                //== TODO: You can validate this item is not used before deletion
                Comm.CommandText = "Delete from COM_CONTENT WHERE CONTENT_ID=@Id";
                Comm.Parameters.AddWithValue("@Id", e.CommandArgument);
                Conn.Open();
                Comm.ExecuteNonQuery();
                Conn.Close();

                //Rebind
                bindGrid(ViewState["Id"].ToString());
            }
        }
    }
}