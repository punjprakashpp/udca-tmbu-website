using System;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public partial class AdminPersons : System.Web.UI.Page
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
            ClearForm();
        }
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CheckPersonExists())
        {
            NotificationHelper.ShowNotification(this, "Person already exists! Use the manage option to update.", "info", "Info");
            return;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim();
        string filePath = hfCurrentFilePath.Value ?? "Image/default/default.jpg"; // Default image path

        try
        {
            if (CheckPersonExists())
            {
                NotificationHelper.ShowNotification(this, "Person already exists! Use the manage option to update.", "info", "Info");
                ClearForm();
                return;
            }

            if (fileUpload.HasFile)
            {
                // Validate file type
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    string fileName = name + fileExtension;
                    string folderPath = Server.MapPath("~/Uploads/person/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fullPath = Path.Combine(folderPath, fileName);

                    // Save the cropped image if provided
                    string base64String = imagePreviewBase64.Value;
                    if (!string.IsNullOrEmpty(base64String))
                    {
                        base64String = base64String.Replace("data:image/png;base64,", "").Replace("data:image/jpeg;base64,", "");
                        byte[] imageBytes = Convert.FromBase64String(base64String);

                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        using (Bitmap bmp = new Bitmap(ms))
                        {
                            bmp.Save(fullPath, ImageFormat.Png);
                        }

                        filePath = "Uploads/person/" + fileName;

                        // Delete the old file if a new one is uploaded
                        DeletePreviousFile(hfCurrentFilePath.Value);
                    }
                }
                else
                {
                    NotificationHelper.ShowNotification(this, "Invalid file type. Only .jpg, .jpeg, and .png are allowed.", "error", "Error");
                    return;
                }
            }

            SavePerson(name, filePath);
            NotificationHelper.ShowNotification(this, "Person added successfully!", "success", "Success");
            ClearForm();
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(this, "Error adding person: " + ex.Message, "error", "Error");
            lblMessage.Text = "Error adding person: " + ex.Message;
        }
    }

    private bool CheckPersonExists()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        string query = "SELECT COUNT(*) FROM Member WHERE Type = @Type AND Align = @Align";

        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            cmd.Parameters.AddWithValue("@Type", "Admin");
            cmd.Parameters.AddWithValue("@Align", ddlType.SelectedIndex);

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return count > 0;
        }
    }

    private void SavePerson(string name, string filePath)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Member (Type, Align, Name, Position, Phone, Email, FilePath, UploadDate) " +
                           "VALUES (@Type, @Align, @Name, @Position, @Phone, @Email, @FilePath, @UploadDate)";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Type", "Admin");
                cmd.Parameters.AddWithValue("@Align", ddlType.SelectedIndex);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Position", ddlType.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@FilePath", filePath);
                cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    private void DeletePreviousFile(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath) && filePath != "Image/default/default.jpg")
        {
            string fullPath = Server.MapPath("~") + filePath;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    private void ClearForm()
    {
        txtName.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        ddlType.SelectedIndex = 0;
        imagePreviewBase64.Value = string.Empty;
        hfCurrentFilePath.Value = string.Empty;
    }
}
