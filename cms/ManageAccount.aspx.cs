using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Configuration;

public partial class cms_ManageAccount : System.Web.UI.Page
{
    private string loggedInUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is authenticated
        if (Request.IsAuthenticated)
        {
            FormsIdentity identity = User.Identity as FormsIdentity;
            if (identity != null && User.Identity == identity)
            {
                FormsAuthenticationTicket ticket = identity.Ticket;
                string userRole = ticket.UserData;
                loggedInUser = ticket.Name; 

                if (string.IsNullOrEmpty(userRole))
                {
                    // Redirect to Login and display message after setting it
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "User role data is missing. Please contact support.";
                    Response.Redirect("~/Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }
        }
        else
        {
            // Redirect to login if user is not authenticated
            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        if (ValidateCurrentPassword(loggedInUser, txtCurrentPassword.Text))
        {
            CheckPasswordPanel.Visible = false;
            ManageAccountPanel.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Password verified. You may edit your information.";
            BindUser(loggedInUser);
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "The current password entered is incorrect.";
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        // Clear the current password input field
        txtCurrentPassword.Text = "";
        lblMessage.Text = "";
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        txtFirstName.ReadOnly = false;
        txtMidName.ReadOnly = false;
        txtLastName.ReadOnly = false;
        txtUserName.ReadOnly = false;
        btnChange.Visible = false;
        btnSubmit.Visible = true;
        txtFirstName.Focus();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string userName = loggedInUser;
        string newUserName = txtUserName.Text.Trim();
        string firstName = txtFirstName.Text.Trim();
        string midName = txtMidName.Text.Trim();
        string lastName = txtLastName.Text.Trim();
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE [User] SET UserName = @NewUserName, FirstName = @FirstName, MidName = @MidName, LastName = @LastName WHERE UserName = @UserName";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NewUserName", newUserName);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@MidName", midName);
                cmd.Parameters.AddWithValue("@LastName", lastName);
                cmd.Parameters.AddWithValue("@UserName", userName);

                conn.Open();
                cmd.ExecuteNonQuery();

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Account updated successfully.";
                
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "An error occurred while updating the account: " + ex.Message;
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        // Clear all input fields and hide the "ChangePassword" table
        txtCurrentPassword.Text = "";
        lblMessage.Text = "";
        txtFirstName.Text = "";
        txtMidName.Text = "";
        txtLastName.Text = "";
        txtUserName.Text = "";
        txtFirstName.ReadOnly = true;
        txtMidName.ReadOnly = true;
        txtLastName.ReadOnly = true;
        txtUserName.ReadOnly = true;
        btnChange.Visible = true;
        btnSubmit.Visible = false;
        ManageAccountPanel.Visible = false;  // Hide the ChangePassword table
        CheckPasswordPanel.Visible = true;        // Show the Check Password button again
    }

    private void BindUser(string userName)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                string query = "SELECT FirstName, MidName, LastName, Role FROM [User] WHERE UserName = @UserName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserName", userName);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtMidName.Text = reader["MidName"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtUserName.Text = userName;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error fetching user data: " + ex.Message;
            }
        }
    }
    private bool ValidateCurrentPassword(string userName, string password)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string sql = "SELECT PasswordHash, PasswordSalt FROM [User] WHERE UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserName", userName);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string storedHash = reader["PasswordHash"].ToString();
                string storedSalt = reader["PasswordSalt"].ToString();
                string passwordHash = GenerateHash(password, storedSalt);

                // Debugging: Check the generated hash
                lblMessage.ForeColor = System.Drawing.Color.Blue;
                lblMessage.Text = "Current Password Matched";

                return storedHash == passwordHash;
            }
            else
            {
                return false;
            }
        }
    }

    private string GenerateHash(string input, string salt)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(input + salt);
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}