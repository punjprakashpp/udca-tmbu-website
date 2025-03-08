using System;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class DeleteInternalQuestions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is authenticated
        if (Request.IsAuthenticated)
        {
            FormsIdentity identity = Page.User.Identity as FormsIdentity;
            if (identity != null)
            {
                FormsAuthenticationTicket ticket = identity.Ticket;
                string userRole = ticket.UserData;

                if (string.IsNullOrEmpty(userRole))
                {
                    // Redirect to Login if user role is missing
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                // Allow access only if the user is an Admin
                if (userRole == "Admin")
                {
                    // User is an Admin; proceed with page logic
                }
                else
                {
                    // Show unauthorized access notification and redirect
                    NotificationHelper.ShowNotification(this, "You are not authorized to access this page!", "warning", "warning");
                    Response.Redirect("~/cms/", false); // Redirect to an Dashboard page
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                // Redirect to Login if identity is null
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        else
        {
            // Redirect to Login if the user is not authenticated
            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

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
                    Type = 'Internal'";

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
