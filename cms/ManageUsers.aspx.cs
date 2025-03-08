using System;
using System.Data;
using System.Drawing;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Manage : Page
{
    private string loggedInUser;

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
            BindUserGrid();
            lblMessage.Text = string.Empty;
        }
    }

    // Method to bind user data to GridView
    private void BindUserGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                string query = "SELECT UserId, UserName, FirstName, MidName, LastName, Role FROM [User]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                gridUsers.DataSource = dataTable;
                gridUsers.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error fetching user data: " + ex.Message, Color.Red);
            }
        }
    }

    // RowDeleting event to handle user deletion
    protected void gridUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string userId = gridUsers.DataKeys[e.RowIndex].Values["UserId"].ToString();
        string userName = gridUsers.DataKeys[e.RowIndex].Values["UserName"].ToString();

        // Check if trying to delete the logged-in user
        if (userName == loggedInUser)
        {
            DisplayMessage("You cannot delete the currently logged-in admin.", Color.Red);
            return;
        }

        // Proceed with deletion logic
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                string query = "DELETE FROM [User] WHERE UserId = @UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", userId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    DisplayMessage("User " + userName + " removed successfully.", Color.Green);
                    BindUserGrid(); // Refresh the GridView after deletion
                }
                else
                {
                    DisplayMessage("Failed to remove user " + userName + ". Please try again.", Color.Red);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error removing user: " + ex.Message, Color.Red);
            }
        }
    }

    // Helper method to display messages
    private void DisplayMessage(string message, Color color)
    {
        lblMessage.Text = message;
        lblMessage.ForeColor = color;
    }
}
