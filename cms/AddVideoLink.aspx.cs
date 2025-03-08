using System;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_pages_UploadMarqueeVideos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void btnSubmit_Edit(object sender, EventArgs e)
    {
        Response.Redirect("ManageVideoLinks.aspx");
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string VideoText = txtVideoText.Text.Trim();
        string VideoURL = txtVideoURL.Text.Trim();

        // Remove "https://youtu.be/" from the VideoURL if it exists
        string urlToRemove = "https://youtu.be/";
        string urlToAdd = "https://www.youtube.com/embed/";
        if (VideoURL.StartsWith(urlToRemove))
        {
            VideoURL = VideoURL.Replace(urlToRemove, urlToAdd);
        }

        if (!string.IsNullOrEmpty(VideoText) && !string.IsNullOrEmpty(VideoURL))
        {
            string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO Links (Type, LinkText, LinkURL, UploadDate) VALUES (@Type, @LinkText, @LinkURL, @UploadDate)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Type", "Video");
                    cmd.Parameters.AddWithValue("@LinkText", VideoText);
                    cmd.Parameters.AddWithValue("@LinkURL", VideoURL);
                    cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        lblMessage.Text = "Video Link uploaded successfully!";
                        lblMessage.ForeColor = System.Drawing.Color.Green;

                        // Clear form fields
                        txtVideoText.Text = string.Empty;
                        txtVideoURL.Text = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        else
        {
            lblMessage.Text = "Please enter both Video Text and Video Link URL.";
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }
    }
}
