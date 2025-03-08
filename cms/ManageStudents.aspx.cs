using System;
using System.Web.Security;
using System.Web.UI;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

public partial class ManageStudents : System.Web.UI.Page
{
    private string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

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
            LoadSessionDropdown();
            MultiView1.ActiveViewIndex = -1;
        }
        ClearMessage();
    }

    private void LoadSessionDropdown()
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("--- Select Session ---", string.Empty));

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT DISTINCT Session FROM Student ORDER BY Session";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ddlSession.Items.Add(new ListItem(reader["Session"].ToString(), reader["Session"].ToString()));
                }
            }
        }
    }

    private string GetConnectionString()
    {
        return ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
    }

    private void ShowMessage(string message, bool isError)
    {
        lblMessage.Text = message;
        lblMessage.ForeColor = isError ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }

    private void ClearMessage()
    {
        lblMessage.Text = string.Empty;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ClearMessage();
        if (string.IsNullOrWhiteSpace(txtsearch.Text))
        {
            ShowMessage("Enter search criteria!", true);
            ClearGrid();
            return;
        }

        string searchCriteria = txtsearch.Text.Trim() + "%";
        string query = GetSearchQuery();

        if (string.IsNullOrEmpty(query))
        {
            ShowMessage("Please select a search option.", true);
            return;
        }

        using (SqlConnection connection = new SqlConnection(GetConnectionString()))
        using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
        {
            adapter.SelectCommand.Parameters.AddWithValue("@SearchParam", searchCriteria);
            DataTable studentTable = new DataTable();

            try
            {
                connection.Open();
                adapter.Fill(studentTable);
                BindGridData(studentTable);
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, true);
                ClearGrid();
            }
        }
    }

    private string GetSearchQuery()
    {
        if (rdRoll.Checked)
            return "SELECT StudentID, Session, RollNo, RegNo, RegYear, FirstName, MidName, LastName FROM Student WHERE RollNo LIKE @SearchParam";
        if (rdName.Checked)
            return "SELECT StudentID, Session, RollNo, RegNo, RegYear, FirstName, MidName, LastName FROM Student WHERE FirstName LIKE @SearchParam";

        return string.Empty;
    }

    private void BindGridData(DataTable studentTable)
    {
        GridView1.DataSource = studentTable;
        GridView1.DataBind();
        if (studentTable.Rows.Count > 0)
        {
            MultiView1.ActiveViewIndex = 0;
            ShowMessage(studentTable.Rows.Count + " Student(s) Found.", false);
        }
        else
        {
            MultiView1.ActiveViewIndex = -1;
            ShowMessage("No students found.", true);
        }
    }

    private void ClearGrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        MultiView1.ActiveViewIndex = -1;
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "View")
        {
            ClearMessage();
            int studentId;
            if (int.TryParse(e.CommandArgument.ToString(), out studentId))
            {
                ShowStudentDetails(studentId);
            }
            else
            {
                ShowMessage("Invalid Student ID.", true);
            }
        }

        if (e.CommandName == "Remove")
        {
            int studentId;
            if (int.TryParse(e.CommandArgument.ToString(), out studentId))
            {
                RemoveStudent(studentId);
            }
            else
            {
                ShowMessage("Invalid Student ID.", true);
            }
        }
    }

    private void ShowStudentDetails(int studentId)
    {
        using (SqlConnection connection = new SqlConnection(GetConnectionString()))
        using (SqlCommand command = new SqlCommand("SELECT * FROM Student WHERE StudentID = @StudentID", connection))
        {
            command.Parameters.AddWithValue("@StudentID", studentId);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PopulateStudentDetails(reader);
                    MultiView1.ActiveViewIndex = 1;
                }
                else
                {
                    ShowMessage("Student details not found.", true);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, true);
            }
        }
    }

    private void PopulateStudentDetails(SqlDataReader reader)
    {
        hfPersonID.Value = reader["StudentID"].ToString();
        txtFirstName.Text = reader["FirstName"].ToString();
        txtMidName.Text = reader["MidName"].ToString();
        txtLastName.Text = reader["LastName"].ToString();
        txtPhone.Text = reader["Phone"].ToString();
        txtEmail.Text = reader["Email"].ToString();
        txtQualification.Text = reader["Qualification"].ToString();
        txtOccupation.Text = reader["Occupation"].ToString();
        txtCompany.Text = reader["Company"].ToString();
        txtAchievement.Text = reader["Achievement"].ToString();
        rblAlumnus.SelectedValue = reader["Alumnus"].ToString();
        rblAchiever.SelectedValue = reader["Achiever"].ToString();
        hfCurrentFilePath.Value = reader["FilePath"].ToString();

        currentImage.Src = ResolveUrl("~/" + reader["FilePath"].ToString());
        currentImage.Style["display"] = "block";
    }

    private void RemoveStudent(int studentId)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT FilePath FROM Student WHERE StudentID = @StudentID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string filePath = reader["FilePath"].ToString();
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        string fullPath = Server.MapPath("~/" + filePath);
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                    }
                }
                reader.Close();
            }

            query = "DELETE FROM Student WHERE StudentID = @StudentID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@StudentID", studentId);
                cmd.ExecuteNonQuery();
            }
        }
        ShowMessage("Student removed successfully.", false);
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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        ClearMessage();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlSession.SelectedIndex = 0;
        rdRoll.Checked = true;
        txtsearch.Text = string.Empty;
        ClearGrid();
        ClearMessage();
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        txtFirstName.Enabled = true;
        txtMidName.Enabled = true;
        txtLastName.Enabled = true;
        txtPhone.Enabled = true;
        txtEmail.Enabled = true; txtQualification.Enabled = true;
        txtOccupation.Enabled = true;
        txtCompany.Enabled = true;
        txtAchievement.Enabled = true;
        rblAchiever.Enabled = true;
        rblAlumnus.Enabled = true;
        fileUpload.Enabled = true;
        btnUpdate.Enabled = true;
        btnEdit.Enabled = false;
        txtFirstName.Focus();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string name = txtFirstName.Text + txtLastName.Text;
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
            string query = "UPDATE Student SET FirstName = @FirstName, MidName = @MidName, LastName = @LastName, Phone = @Phone, Email = @Email, Qualification = @Qualification, Occupation = @Occupation, Company = @Company, Achievement = @Achievement, Achiever = @Achiever, Alumnus = @Alumnus, FilePath = @FilePath WHERE StudentID = @StudentID";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = txtFirstName.Text.Trim();
                cmd.Parameters.Add("@MidName", SqlDbType.NVarChar).Value = txtMidName.Text.Trim();
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = txtLastName.Text.Trim();
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = txtPhone.Text.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text.Trim();
                cmd.Parameters.Add("@Qualification", SqlDbType.NVarChar).Value = txtQualification.Text.Trim();
                cmd.Parameters.Add("@Occupation", SqlDbType.NVarChar).Value = txtOccupation.Text.Trim();
                cmd.Parameters.Add("@Company", SqlDbType.NVarChar).Value = txtCompany.Text.Trim();
                cmd.Parameters.Add("@Achievement", SqlDbType.NVarChar).Value = txtAchievement.Text.Trim();
                cmd.Parameters.Add("@Alumnus", SqlDbType.NVarChar).Value = rblAlumnus.SelectedValue.ToString();
                cmd.Parameters.Add("@Achiever", SqlDbType.NVarChar).Value = rblAchiever.SelectedValue.ToString();
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

    private void ClearForm()
    {
        txtFirstName.Enabled = false;
        txtMidName.Enabled = false;
        txtLastName.Enabled = false;
        txtPhone.Enabled = false;
        txtEmail.Enabled = false;
        txtQualification.Enabled = false;
        txtOccupation.Enabled = false;
        txtCompany.Enabled = false;
        txtAchievement.Enabled = false;
        rblAlumnus.Enabled = false;
        rblAchiever.Enabled = false;
        fileUpload.Enabled = false;
        btnUpdate.Enabled = false;
        btnEdit.Enabled = true;
        fileUpload.Attributes.Clear();
        hfCurrentFilePath.Value = string.Empty;
        currentImage.Src = string.Empty;
        currentImage.Style["display"] = "none";
        MultiView1.ActiveViewIndex = 0;
        ClearMessage();
    }
}