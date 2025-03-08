using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class pages_VideoGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindVideoLinks();
    }

    private void BindVideoLinks()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT LinkText, LinkURL FROM Links WHERE Type = 'Video'";
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

            linksRepeater.DataSource = dt;
            linksRepeater.DataBind();
        }
    }
}