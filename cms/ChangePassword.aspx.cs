using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Configuration;

public partial class Account_ChangePassword : System.Web.UI.Page
{

    protected void btnCheck_Click(object sender, EventArgs e)
    {
        // Validate the current password and display the ChangePassword table
        if (ValidateCurrentPassword(HttpContext.Current.User.Identity.Name, txtCurrentPassword.Text))
        {
            // Hide the "Check Password" button and show the "ChangePassword" table
            CheckPasswordPanel.Visible = false;
            ChangePasswordPanel.Visible = true;
        }
        else
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Current password is incorrect.";
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        // Clear the current password input field
        txtCurrentPassword.Text = "";
        lblMessage.Text = "";
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        // Clear all input fields and hide the "ChangePassword" table
        txtCurrentPassword.Text = "";
        txtNewPassword.Text = "";
        txtPasswordCheck.Text = "";
        lblMessage.Text = "";
        ChangePasswordPanel.Visible = false;  // Hide the ChangePassword table
        CheckPasswordPanel.Visible = true;        // Show the Check Password button again
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        // Ensure new passwords match
        if (txtNewPassword.Text != txtPasswordCheck.Text)
        {
            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "Passwords do not match.";
            return;
        }

        // Perform the password change if the page is valid
        if (Page.IsValid)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string oldPassword = txtCurrentPassword.Text;
            string newPassword = txtNewPassword.Text;

                string newSalt = CreateSalt();
                string newHash = GenerateHash(newPassword, newSalt);

                if (UpdatePassword(userName, newHash, newSalt))
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Password changed successfully!";
                }
                else
                {
                    lblMessage.Text = "An error occurred while updating the password.";
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

    private bool UpdatePassword(string userName, string newHash, string newSalt)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string sql = "UPDATE [User] SET PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt WHERE UserName = @UserName";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@PasswordHash", newHash);
            cmd.Parameters.AddWithValue("@PasswordSalt", newSalt);
            cmd.Parameters.AddWithValue("@UserName", userName);

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();

            return rowsAffected > 0;
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
}