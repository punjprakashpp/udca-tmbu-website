using System;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI.WebControls;

public partial class ManageFaculty : System.Web.UI.Page
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
            PopulateFacultyDropdown();
            if (!string.IsNullOrEmpty(ddlFaculties.SelectedValue))
            {
                PopulateFacultyDetails(Convert.ToInt32(ddlFaculties.SelectedValue));
            }
        }
    }

    private void PopulateFacultyDropdown()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT MemberID, Name FROM Member WHERE Type = 'Faculty' OR Type = 'Guest'";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                ddlFaculties.DataSource = reader;
                ddlFaculties.DataTextField = "Name";
                ddlFaculties.DataValueField = "MemberID";
                ddlFaculties.DataBind();
                ddlFaculties.Items.Insert(0, new ListItem("--Select Faculty--", ""));
                reader.Close();
            }
        }
    }

    private void PopulateFacultyDetails(int facultyId)
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT * FROM Member WHERE MemberID = @FacultyId";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FacultyId", facultyId);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ddlType.SelectedValue = reader["Type"].ToString();
                    ddlStatus.SelectedValue = reader["Status"].ToString();
                    txtName.Text = reader["Name"].ToString();
                    txtQualification.Text = reader["Qualification"].ToString();
                    txtPosition.Text = reader["Position"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    currentImage.Src = ResolveUrl("~/" + reader["FilePath"].ToString());
                    currentImage.Style["display"] = "block";
                }
                reader.Close();
            }
        }
    }

    protected void ddlFaculties_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlFaculties.SelectedValue))
        {
            PopulateFacultyDetails(Convert.ToInt32(ddlFaculties.SelectedValue));
        }
        else
        {
            ClearForm();
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlFaculties.SelectedValue))
        {
            int facultyId = Convert.ToInt32(ddlFaculties.SelectedValue);
            string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
            string FilePath = "";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                string selectFilePathQuery = "SELECT FilePath FROM Member WHERE MemberID = @FacultyId";
                using (SqlCommand selectFilePathCmd = new SqlCommand(selectFilePathQuery, con))
                {
                    selectFilePathCmd.Parameters.AddWithValue("@FacultyId", facultyId);
                    FilePath = selectFilePathCmd.ExecuteScalar() as string;
                }
            }

            bool newFileUploaded = fileUpload.HasFile;
            string oldFilePath = FilePath; // Store the current image path
            string type = ddlType.SelectedValue;
            string status = ddlStatus.SelectedValue;
            if (newFileUploaded)
            {
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                    string folderPath = Server.MapPath("~/Uploads/faculty/");
                    string fullPath = Path.Combine(folderPath, fileName);

                    try
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        // Save the new file
                        string base64String = imagePreviewBase64.Value;
                        base64String = base64String.Replace("data:image/png;base64,", "").Replace("data:image/jpeg;base64,", "");
                        byte[] imageBytes = Convert.FromBase64String(base64String);
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            using (Bitmap bmp = new Bitmap(ms))
                            {
                                bmp.Save(fullPath, ImageFormat.Png);
                            }
                        }
                        FilePath = "Uploads/faculty/" + fileName;

                        // Delete the old file
                        if (!string.IsNullOrEmpty(oldFilePath))
                        {
                            string filePath = Server.MapPath("~/" + oldFilePath);
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error saving new image file: " + ex.Message;
                        return;
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid file type. Only .jpg, .jpeg, .png files are allowed.";
                    return;
                }
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "UPDATE Member SET Type = @Type, Status = @Status, Name = @Name, Qualification = @Qualification, Position = @Position, Phone = @Phone, Email = @Email, FilePath = @FilePath WHERE MemberID = @FacultyId";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Type", type);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text);
                        cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@FilePath", FilePath);
                        cmd.Parameters.AddWithValue("@FacultyId", ddlFaculties.SelectedValue);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        lblMessage.Text = "Faculty details updated successfully!";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error updating faculty details: " + ex.Message;
            }
        }
        else
        {
            lblMessage.Text = "Please select a faculty.";
        }

        ClearForm();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlFaculties.SelectedValue))
        {
            int facultyId = Convert.ToInt32(ddlFaculties.SelectedValue);
            string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string FilePath = "";
                    string selectFilePathQuery = "SELECT FilePath FROM Member WHERE MemberID = @FacultyId";
                    using (SqlCommand selectFilePathCmd = new SqlCommand(selectFilePathQuery, conn))
                    {
                        selectFilePathCmd.Parameters.AddWithValue("@FacultyId", facultyId);
                        FilePath = selectFilePathCmd.ExecuteScalar() as string;
                    }

                    string deleteFacultyQuery = "DELETE FROM Member WHERE MemberID = @FacultyId";
                    using (SqlCommand deleteFacultyCmd = new SqlCommand(deleteFacultyQuery, conn))
                    {
                        deleteFacultyCmd.Parameters.AddWithValue("@FacultyId", facultyId);
                        int rowsAffected = deleteFacultyCmd.ExecuteNonQuery();
                        if (rowsAffected > 0 && !string.IsNullOrEmpty(FilePath))
                        {
                            string filePath = Server.MapPath("~/" + FilePath);
                            if (File.Exists(filePath))
                            {
                                File.Delete(filePath);
                            }

                            lblMessage.Text = "Faculty deleted successfully!";
                            PopulateFacultyDropdown();
                            ClearForm();
                        }
                        else
                        {
                            lblMessage.Text = "Failed to delete faculty.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error deleting faculty: " + ex.Message;
            }
        }
        else
        {
            lblMessage.Text = "Please select a faculty.";
        }
    }

    private void ClearForm()
    {
        ddlType.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;
        txtName.Text = string.Empty;
        fileUpload.Attributes.Clear();
        txtQualification.Text = string.Empty;
        txtPosition.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        currentImage.Style["display"] = "none";
    }
}
