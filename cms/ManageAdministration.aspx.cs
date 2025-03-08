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

public partial class ManageAdministration : System.Web.UI.Page
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
            PopulateTypeDropdown();
        }
    }

    private void PopulateTypeDropdown()
    {
        ddlType.Items.Clear();
        ddlType.Items.Add(new ListItem("--- Select a Person ---", string.Empty));

        string query = "SELECT DISTINCT Position, Align FROM Member WHERE Type = 'Admin' ORDER BY Align";
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string position = reader["Position"].ToString();
                    ddlType.Items.Add(new ListItem(position, position));
                }
            }
            catch (Exception ex)
            {
                NotificationHelper.ShowNotification(this, "Error loading persons: " + ex.Message, "error", "Error");
            }
        }
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPerson();
    }

    private void LoadPerson()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        string query = "SELECT * FROM Member WHERE Type = @Type AND Position = @Position";

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@Type", SqlDbType.NVarChar).Value = "Admin";
                cmd.Parameters.Add("@Position", SqlDbType.NVarChar).Value = ddlType.SelectedItem.ToString();

                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        hfPersonID.Value = rdr["MemberID"].ToString();
                        txtName.Text = rdr["Name"].ToString();
                        txtPhone.Text = rdr["Phone"].ToString();
                        txtEmail.Text = rdr["Email"].ToString();
                        hfCurrentFilePath.Value = rdr["FilePath"].ToString();

                        currentImage.Src = ResolveUrl("~/" + rdr["FilePath"].ToString());
                        currentImage.Style["display"] = "block";
                    }
                    else
                    {
                        ClearForm();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(this, "Error loading person: " + ex.Message, "error", "Error");
            lblMessage.Text = "Error loading person: " + ex.Message;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim();
        string filePath = hfCurrentFilePath.Value ?? "Image/default/default.jpg"; // Default image path

        try
        {
            if (fileUpload.HasFile)
            {
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                {
                    string fileName = name.Replace(" ", "_") + fileExtension;
                    string folderPath = Server.MapPath("~/Uploads/person/");

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

                        filePath = "Uploads/person/" + fileName;

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

            NotificationHelper.ShowNotification(this, "Person saved successfully!", "success", "Success");
            ClearForm();
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(this, "Error saving person: " + ex.Message, "error", "Error");
            lblMessage.Text = "Error saving person: " + ex.Message;
        }
    }

    private void SavePerson(string name, string filePath)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE Member SET Name = @Name, Phone = @Phone, Email = @Email, FilePath = @FilePath WHERE MemberID = @MemberID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = txtPhone.Text;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                cmd.Parameters.Add("@FilePath", SqlDbType.NVarChar).Value = filePath;

                if (!string.IsNullOrEmpty(hfPersonID.Value))
                {
                    cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = int.Parse(hfPersonID.Value);
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
        hfPersonID.Value = string.Empty;
        ddlType.SelectedIndex = 0;
        txtName.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtEmail.Text = string.Empty;
        fileUpload.Attributes.Clear();
        hfCurrentFilePath.Value = string.Empty;
        currentImage.Src = string.Empty;
        currentImage.Style["display"] = "none";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Member WHERE MemberID = @MemberID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    if (!string.IsNullOrEmpty(hfPersonID.Value))
                    {
                        cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = int.Parse(hfPersonID.Value);
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                DeletePreviousFile(hfCurrentFilePath.Value);
                NotificationHelper.ShowNotification(this, "Person deleted successfully!", "success", "Success");
                ClearForm();
            }
        }
        catch (Exception ex)
        {
            NotificationHelper.ShowNotification(this, "Error deleting person: " + ex.Message, "error", "Error");
            lblMessage.Text = "Error deleting person: " + ex.Message;
        }
    }
}
