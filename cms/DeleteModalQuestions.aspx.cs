using System;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class DeleteModalQuestions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void gvNotice_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvNotice.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    private void BindGridView()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
                SELECT 
                    DocsID,
                    Title, 
                    Semester,
                    Session,
                    FilePath,
                    ROW_NUMBER() OVER (ORDER BY UploadDate DESC) AS RowNum
                FROM 
                    Docs
                WHERE
                    Type = 'Modal'";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        sda.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            lblMessage.Text = "No records found.";
                            lblMessage.ForeColor = Color.Red;
                            lblMessage.Visible = true;
                        }
                        else
                        {
                            lblMessage.Visible = false;
                        }

                        gvNotice.DataSource = dt;
                        gvNotice.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "An error occurred while retrieving data: " + ex.Message;
                        lblMessage.ForeColor = Color.Red;
                        lblMessage.Visible = true;
                    }
                }
            }
        }
    }

    protected void gvNotice_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int noticeID = Convert.ToInt32(gvNotice.DataKeys[e.RowIndex].Values[0]);
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        try
        {
            string filePath = null;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT FilePath FROM Docs WHERE DocsID = @DocsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DocsID", noticeID);
                    conn.Open();
                    filePath = cmd.ExecuteScalar() as string;
                }
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                string fullPath = Server.MapPath("~/" + filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "DELETE FROM Docs WHERE DocsID = @DocsID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DocsID", noticeID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            lblMessage.Text = "Assignment deleted successfully.";
            lblMessage.ForeColor = Color.Green;
            lblMessage.Visible = true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error deleting assignment: " + ex.Message;
            lblMessage.ForeColor = Color.Red;
            lblMessage.Visible = true;
        }

        BindGridView();
    }
}
