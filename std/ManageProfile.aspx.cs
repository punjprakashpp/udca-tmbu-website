using System;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;

public partial class std_ManageProfile : System.Web.UI.Page
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
        string query = "SELECT StudentID, FirstName, MidName, LastName, Phone, Email, FilePath FROM Student WHERE StudentID = @StudentID";

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
                        txtFirstName.Text = rdr["FirstName"].ToString();
                        txtMidName.Text = rdr["MidName"].ToString();
                        txtLastName.Text = rdr["LastName"].ToString();
                        txtPhone.Text = rdr["Phone"].ToString();
                        txtEmail.Text = rdr["Email"].ToString();
                        hfCurrentFilePath.Value = rdr["FilePath"].ToString();

                        currentImage.Src = ResolveUrl("~/" + rdr["FilePath"].ToString());
                        currentImage.Style["display"] = "block";
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
        txtFirstName.Enabled = true;
        txtMidName.Enabled = true;
        txtLastName.Enabled = true;
        txtPhone.Enabled = true;
        txtEmail.Enabled = true;
        fileUpload.Enabled = true;
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        btnEdit.Enabled = false;
        txtFirstName.Focus();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string name = txtFirstName.Text+txtLastName.Text;
        string filePath = hfCurrentFilePath.Value ?? "Image/default/default.jpg"; // Default image path

        try
        {
            if (fileUpload.HasFile)
            {
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    string fileName = name.Replace(" ", "_") + fileExtension;
                    string folderPath = Server.MapPath("~/Uploads/student/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fullPath = Path.Combine(folderPath, fileName);

                    if (!string.IsNullOrEmpty(imagePreviewBase64.Value))
                    {
                        string base64String = imagePreviewBase64.Value.Replace("data:image/png;base64,", "").Replace("data:image/jpeg;base64,", "");
                        byte[] imageBytes = Convert.FromBase64String(base64String);

                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        using (Bitmap bmp = new Bitmap(ms))
                        {
                            bmp.Save(fullPath, ImageFormat.Png);
                        }

                        filePath = "Uploads/student/" + fileName;

                        DeletePreviousFile(hfCurrentFilePath.Value);
                    }
                }
                else
                {
                    NotificationHelper.ShowNotification(this, "Invalid file type. Only .jpg, .jpeg, and .png are allowed.", "info", "info");
                    return;
                }
            }

            SavePerson(name, filePath);
            ClearForm();
            NotificationHelper.ShowNotification(this, "Details saved successfully!", "success", "Success");
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(this, "Error saving Details: " + ex.Message, "error", "Error");
            lblMessage.Text = "Error saving Details: " + ex.Message;
        }
    }

    private void SavePerson(string name, string filePath)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE Student SET FirstName = @FirstName, MidName = @MidName, LastName = @LastName, Phone = @Phone, Email = @Email, FilePath = @FilePath WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = txtFirstName.Text.Trim();
                cmd.Parameters.Add("@MidName", SqlDbType.NVarChar).Value = txtMidName.Text.Trim();
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text.Trim();
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = txtPhone.Text.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();
                cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar).Value = filePath;

                if (!string.IsNullOrEmpty(hfPersonID.Value))
                {
                    cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = int.Parse(hfPersonID.Value);
                }

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    private void DeletePreviousFile(string filePath)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            string fullPath = Server.MapPath("~/" + filePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }

    private void ClearForm()
    {
        txtFirstName.Enabled = false;
        txtMidName.Enabled = false;
        txtLastName.Enabled = false;
        txtPhone.Enabled = false;
        txtEmail.Enabled = false;
        fileUpload.Enabled = false;
        btnSave.Enabled = false;
        btnCancel.Enabled = false;
        btnEdit.Enabled = true;
        fileUpload.Attributes.Clear();
        hfCurrentFilePath.Value = string.Empty;
        currentImage.Src = string.Empty;
        currentImage.Style["display"] = "none";
        LoadStudent();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearForm();
    }
}