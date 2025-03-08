using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Data;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    private string userName;
    private string role;
    private string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            NotificationHelper.ShowNotification(this, "This page is only for Alumni & CMS Users!", "warning", "warning");
        }
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            userName = txtUserName.Text.Trim(); // Use the class-level userName variable
            string password = txtPassword.Text;

            try
            {
                if (AuthenticateUser(userName, password, out role))
                {
                    NotificationHelper.ShowNotification(this, "Login successful!", "success", "success");

                    // Create the authentication ticket with the role
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,                      // Ticket version
                        userName,               // Username
                        DateTime.Now,           // Issue date
                        DateTime.Now.AddMinutes(15), // Expiration date (default)
                        chkboxRem.Checked,      // Persistent (true if checked)
                        role                    // User role (stored as user data)
                    );

                    // Encrypt the ticket
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    // Set the cookie expiration based on whether "Remember Me" is checked
                    if (chkboxRem.Checked)
                    {
                        authCookie.Expires = DateTime.Now.AddDays(30); // Cookie expires in 30 days
                    }

                    Response.Cookies.Add(authCookie);

                    // Redirect to the dashboard
                    Response.Redirect("~/cms/");
                }
                else
                {
                    NotificationHelper.ShowNotification(this, "Invalid username or password!", "error", "error");
                }
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowNotification(this, "An error occurred during login. Please try again later.", "error", "error");
                LogError(ex);  // Log the error for troubleshooting
            }
        }
    }

    private bool AuthenticateUser(string userName, string password, out string role)
    {
        role = null;
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string sql = "SELECT PasswordHash, PasswordSalt, Role FROM [User] WHERE UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@UserName", userName);

            conn.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    string storedHash = reader["PasswordHash"].ToString();
                    string storedSalt = reader["PasswordSalt"].ToString();
                    role = reader["Role"].ToString();
                    string passwordHash = GenerateHash(password, storedSalt);

                    return storedHash == passwordHash;
                }
            }
        }
        return false;
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
        try
        {
            // Log errors to a file or database as needed
            string logFilePath = Server.MapPath("~/log/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                writer.WriteLine("Date: " + DateTime.Now.ToString());
                writer.WriteLine("Message: " + ex.Message);
                writer.WriteLine("StackTrace: " + ex.StackTrace);
                writer.WriteLine("-----------------------------------------------------");
            }
        }
        catch
        {
            // Handle any errors that occurred during logging
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        // Clear the input fields
        txtUserName.Text = "";
        txtPassword.Text = "";
        lblMessage.Text = "Login to School Website";
        chkboxRem.Checked = false;
        txtUserName.Focus();
    }

    protected void rblLoginType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblLoginType.SelectedValue == "Alumni")
        {
            LoadSessions();
            AlumniPanel.Visible = true;
            AlumniPanel.Enabled = true;
            CMSPanel.Visible = false;
            CMSPanel.Enabled = false;
        }
        else if (rblLoginType.SelectedValue == "CMS")
        {
            AlumniPanel.Visible = false;
            AlumniPanel.Enabled = false;
            CMSPanel.Visible = true;
            CMSPanel.Enabled = true;
        }
    }

    private void LoadSessions()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT [Session] FROM [Student]";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            ddlSession.DataSource = dt;
            ddlSession.DataTextField = "Session";
            ddlSession.DataValueField = "Session";
            ddlSession.DataBind();
            ddlSession.Items.Insert(0, new ListItem("--- Select Session ---", ""));
        }
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                if (AuthenticateStudent())
                {
                    // Create the authentication ticket with the role
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,                      // Ticket version
                        userName,               // Username
                        DateTime.Now,           // Issue date
                        DateTime.Now.AddMinutes(10), // Expiration date (default)
                        true,                       // Persistent (true if checked)
                        role                        // User role (stored as user data)
                    );

                    // Encrypt the ticket
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                    Response.Cookies.Add(authCookie);

                    // Redirect to the dashboard
                    Response.Redirect("~/std/");
                }
                else
                {
                    NotificationHelper.ShowNotification(this, "Invalid username or password!", "error", "error");
                }
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowNotification(this, "An error occurred during login. Please try again later.", "error", "error");
                LogError(ex);  // Log the error for troubleshooting
            }
        }
    }

    private bool AuthenticateStudent()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT StudentID, FirstName FROM [Student] WHERE [Session] = @Session AND [RollNo] = @RollNo AND [RegNo] = @RegNo AND [RegYear] = @RegYear AND [DOB] = @DOB";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Session", ddlSession.SelectedValue);
            cmd.Parameters.AddWithValue("@RollNo", txtRollNo.Text);
            cmd.Parameters.AddWithValue("@RegNo", txtRegistrationNo.Text);
            cmd.Parameters.AddWithValue("@RegYear", txtRegistrationYear.Text);
            cmd.Parameters.AddWithValue("@DOB", DateTime.ParseExact(txtDOB.Text, "dd-MM-yyyy", null));

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                if (reader.Read())
                {
                    role = reader["StudentID"].ToString();
                    userName = reader["FirstName"].ToString();
                    NotificationHelper.ShowNotification(this, "Verification Successful.", "success", "success");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                NotificationHelper.ShowNotification(this, "Verification failed. Please check your details.", "error", "error");
                return false;
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        txtRollNo.Text = txtRegistrationNo.Text = txtRegistrationYear.Text = txtDOB.Text = string.Empty;
    }
}
