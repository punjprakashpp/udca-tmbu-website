using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class cms_DeleteGalleryImages : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    private void BindGridView(string searchQuery = "")
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT ImageID, Title, '~/' + FilePath AS FilePath FROM Image WHERE Type = 'Gallery' ORDER BY UploadDate DESC";
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += " AND Title LIKE @SearchQuery";
            }

            using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
            {
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    da.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                }

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

                gvGallery.DataSource = dt;
                gvGallery.DataBind();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView(txtSearch.Text.Trim());
    }

    protected void gvGallery_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvGallery.PageIndex = e.NewPageIndex;
        BindGridView(txtSearch.Text.Trim());
    }

    protected void gvGallery_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int id = Convert.ToInt32(gvGallery.DataKeys[e.RowIndex].Value);
        string FilePath = null;

        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT FilePath AS FilePath FROM Image WHERE ImageID = @ImageID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ImageID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    FilePath = reader["FilePath"].ToString();
                }
                reader.Close();
            }

            query = "DELETE FROM Image WHERE ImageID = @ImageID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ImageID", id);
                cmd.ExecuteNonQuery();
            }
        }

        if (!string.IsNullOrEmpty(FilePath))
        {
            string fullPath = Server.MapPath("~/" + FilePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        lblMessage.Text = "Gallery image deleted successfully!";
        lblMessage.ForeColor = Color.Green;
        BindGridView(txtSearch.Text.Trim());
    }
}