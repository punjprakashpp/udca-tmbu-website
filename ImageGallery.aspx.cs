using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class ImageGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGallery();
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Title"]))
        {
            string title = Request.QueryString["Title"];
            Response.Clear();
            Response.Write(GetModalContent(title));
            Response.End();
        }
    }

    private void BindGallery()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
                WITH GalleryCTE AS (
                    SELECT 
                        Title, 
                        FilePath,
                        UploadDate,
                        ROW_NUMBER() OVER(PARTITION BY Title ORDER BY FilePath) AS RowNum
                    FROM 
                        Image 
                    WHERE 
                        Type = 'Gallery'
                )
                SELECT 
                    Title,
                    FilePath
                FROM 
                    GalleryCTE
                WHERE 
                    RowNum = 1
                ORDER BY UploadDate DESC";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                lblMessage.Text = "No records found.";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Text = ""; // Hide the message if records are found
            }

            ThumbnailRepeater.DataSource = dt;
            ThumbnailRepeater.DataBind();
        }
    }

    private string GetModalContent(string title)
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT FilePath FROM Image WHERE Title = @Title AND Type = 'Gallery'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Title", title);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string modalContent = "";
            foreach (DataRow row in dt.Rows)
            {
                modalContent += "<img src='" + row["FilePath"].ToString() + "' alt='" + title + "' onclick=\"displayImage('" + row["FilePath"].ToString() + "')\">";
            }
            return modalContent;
        }
    }
}