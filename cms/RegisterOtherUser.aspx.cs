using System;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;

public partial class Register : Page
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
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        if (txtPassword.Text != txtPasswordCheck.Text)
        {
            lblMessage.Text = "Passwords do not match.";
            lblMessage.ForeColor = Color.Red;
            return;
        }

        string firstName = txtFirstName.Text.Trim();
        string midName = txtMidName.Text.Trim();
        string lastName = txtLastName.Text.Trim();
        string userName = txtUserName.Text.Trim();
        string role = drpRole.SelectedValue;
        string password = txtPassword.Text;

        if (!IsPasswordStrong(password))
        {
            lblMessage.Text = "Password must be at least 8 characters long and contain uppercase, lowercase, number, and special character.";
            lblMessage.ForeColor = Color.Red;
            return;
        }

        if (IsUserExist(userName))
        {
            lblMessage.Text = "User with the same username already exists.";
            lblMessage.ForeColor = Color.Red;
            return;
        }

        // Hash and salt the password
        string passwordSalt = CreateSalt();
        string passwordHash = GenerateHash(password, passwordSalt);

        // Insert user details into the database
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = @"INSERT INTO [dbo].[User] 
                            (FirstName, MidName, LastName, UserName, Role, PasswordHash, PasswordSalt, CreatedDate) 
                            VALUES 
                            (@FirstName, @MidName, @LastName, @UserName, @Role, @PasswordHash, @PasswordSalt, @CreatedDate)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@MidName", string.IsNullOrEmpty(midName) ? (object)DBNull.Value : midName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@PasswordSalt", passwordSalt);
                command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        lblMessage.Text = "Registration successful.";
                        lblMessage.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblMessage.Text = "Registration failed. Please try again.";
                        lblMessage.ForeColor = Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred during registration. Please try again later.";
                    lblMessage.ForeColor = Color.Red;
                    LogError(ex); // Log the exception to a file
                }
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        // Clear all fields
        txtFirstName.Text = "";
        txtMidName.Text = "";
        txtLastName.Text = "";
        txtUserName.Text = "";
        txtPassword.Text = "";
        txtPasswordCheck.Text = "";
        drpRole.SelectedIndex = 0;
        lblMessage.Text = "";
    }

    private bool IsPasswordStrong(string password)
    {
        if (password.Length < 8) return false; // Minimum length
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]")) return false; // At least one uppercase letter
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]")) return false; // At least one lowercase letter
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]")) return false; // At least one digit
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[\W_]")) return false; // At least one special character

        return true;
    }

    private bool IsUserExist(string userName)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string sql = "SELECT COUNT(*) FROM [User] WHERE UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@UserName", userName);

            conn.Open();
            int userCount = (int)cmd.ExecuteScalar();

            return userCount > 0;
        }
    }

    private string CreateSalt()
    {
        byte[] saltBytes = new byte[16];
        using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
        {
            provider.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
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

    private void LogError(Exception ex)
    {
        string filePath = Server.MapPath("~/log/ErrorLog.txt");
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine("Date: " + DateTime.Now.ToString());
            writer.WriteLine("Message: " + ex.Message);
            writer.WriteLine("StackTrace: " + ex.StackTrace);
            writer.WriteLine("-----------------------------------------------------");
        }
    }
}