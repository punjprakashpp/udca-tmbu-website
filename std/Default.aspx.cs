using System;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;

public partial class std_Default : System.Web.UI.Page
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
            LoadStudentData();
        }
    }

    private void LoadStudentData()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT * FROM Student WHERE StudentID = @StudentID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@StudentID", StudentID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblName.Text = reader["FirstName"].ToString() + " " + reader["MidName"].ToString() + " " + reader["LastName"].ToString();
                    lblSession.Text = reader["Session"].ToString();
                    lblGender.Text = reader["Gender"].ToString();
                    lblDOB.Text = Convert.ToDateTime(reader["DOB"]).ToString("dd MMM yyyy");
                    lblOccupation.Text = reader["Occupation"].ToString();
                    lblCompany.Text = reader["Company"].ToString();
                    lblAchievement.Text = reader["Achievement"].ToString();
                    lblPhone.Text = reader["Phone"].ToString();
                    lblEmail.Text = reader["Email"].ToString();

                    imgProfile.ImageUrl = string.IsNullOrEmpty(reader["FilePath"].ToString()) ? "~/Image/default/default.jpg" : "~/" + reader["FilePath"].ToString();

                    lnkFacebook.NavigateUrl = "https://www.facebook.com/"+ reader["Facebook"].ToString();
                    lnkInstagram.NavigateUrl = "https://www.instagram.com/" + reader["Instagram"].ToString();
                    lnkTwitter.NavigateUrl = "https://x.com/"+ reader["Twitter"].ToString();
                    lnkGitHub.NavigateUrl = "https://github.com/" + reader["GitHub"].ToString();
                    lnkLinkedIn.NavigateUrl = "https://www.linkedin.com/in/"+reader["LinkedIn"].ToString();
                }
                else
                {
                    NotificationHelper.ShowNotification(this, "Student not found.", "error", "Error");
                    lblMessage.Text = "Student not found.";
                }
            }
        }
    }
}
