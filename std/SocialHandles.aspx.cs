using System;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class std_SocialHandles : System.Web.UI.Page
{
    private string StudentID = string.Empty;
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

                if (string.IsNullOrEmpty(userRole) || userRole == "Admin" || userRole == "User")
                {
                    // Redirect to Login if user role is missing
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
                else
                {
                    StudentID = userRole;
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
            LoadStudent();
        }
    }

    private void LoadStudent()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        string query = "SELECT StudentID, LinkedIn, GitHub, Facebook, Instagram, Twitter FROM Student WHERE StudentID = @StudentID";

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", StudentID);

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        hfPersonID.Value = rdr["StudentID"].ToString();
                        txtLinkedIn.Text = rdr["LinkedIn"].ToString();
                        txtGitHub.Text = rdr["GitHub"].ToString();
                        txtFacebook.Text = rdr["Facebook"].ToString();
                        txtInstagram.Text = rdr["Instagram"].ToString();
                        txtTwitter.Text = rdr["Twitter"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(this, "Error loading Details: " + ex.Message, "error", "Error");
            lblMessage.Text = "Error loading Details: " + ex.Message;
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        txtLinkedIn.Enabled = true;
        txtGitHub.Enabled = true;
        txtFacebook.Enabled = true;
        txtInstagram.Enabled = true;
        txtTwitter.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        btnEdit.Enabled = false;
        txtLinkedIn.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE Student SET LinkedIn = @LinkedIn, GitHub = @GitHub, Facebook = @Facebook, Instagram = @Instagram, Twitter = @Twitter WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@LinkedIn", SqlDbType.NVarChar).Value = txtLinkedIn.Text.Trim();
                cmd.Parameters.Add("@GitHub", SqlDbType.NVarChar).Value = txtGitHub.Text.Trim();
                cmd.Parameters.Add("@Facebook", SqlDbType.NVarChar).Value = txtFacebook.Text.Trim();
                cmd.Parameters.Add("@Instagram", SqlDbType.NVarChar).Value = txtInstagram.Text.Trim();
                cmd.Parameters.Add("@Twitter", SqlDbType.NVarChar).Value = txtTwitter.Text.Trim();

                if (!string.IsNullOrEmpty(hfPersonID.Value))
                {
                    cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = int.Parse(hfPersonID.Value);
                }

                con.Open();
                cmd.ExecuteNonQuery();
                ClearForm();
                NotificationHelper.ShowNotification(this, "Details saved successfully!", "success", "Success");
            }
        }
    }

    private void ClearForm()
    {
        txtLinkedIn.Enabled = false;
        txtGitHub.Enabled = false;
        txtFacebook.Enabled = false;
        txtInstagram.Enabled = false;
        txtTwitter.Enabled = false;
        btnSave.Enabled = false;
        btnCancel.Enabled = false;
        btnEdit.Enabled = true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
}