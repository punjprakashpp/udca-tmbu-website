using System;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class cms_AddHighlightLink : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateDropDownList();
        }
    }

    protected void Alert(string message)
    {
        string script = string.Format("alert('{0}');", message.Replace("'", "\\'")); // Escape single quotes

        // Register the JavaScript alert
        ClientScript.RegisterStartupScript(this.GetType(), "AlertMessage", script, true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string linkText = txtLinkText.Text.Trim();
        string linkURL = txtLinkURL.Text.Trim();

        if (!string.IsNullOrEmpty(linkText) && !string.IsNullOrEmpty(linkURL))
        {
            string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Check for duplicates
                string checkQuery = "SELECT COUNT(*) FROM Links WHERE LinkText = @LinkText AND LinkURL = @LinkURL";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@LinkText", linkText);
                    checkCmd.Parameters.AddWithValue("@LinkURL", linkURL);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Alert("This link already exists.");
                        return;
                    }
                }

                // Insert new link if it doesn't exist
                string query = "INSERT INTO Links (Type, LinkText, LinkURL, UploadDate) VALUES (@Type, @LinkText, @LinkURL, @UploadDate)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Type", "Link");
                    cmd.Parameters.AddWithValue("@LinkText", linkText);
                    cmd.Parameters.AddWithValue("@LinkURL", linkURL);
                    cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        Alert("Link uploaded successfully!");

                        // Clear form fields and reset state
                        txtLinkText.Text = string.Empty;
                        txtLinkURL.Text = string.Empty;
                        chkboxExt.Checked = false;
                        txtLinkURL.Enabled = false;
                        ddlLinkPicker.Enabled = true;
                        ddlLinkPicker.SelectedIndex = 0;

                        // Refresh the dropdown list
                        PopulateDropDownList();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
        else
        {
            Alert("Please enter both Link Text and Link URL.");
        }
    }

    protected void btnManage_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageHighlightLinks.aspx");

    }

    private void PopulateDropDownList()
    {
        ddlLinkPicker.Items.Clear(); // Clear existing items

        string[] filePaths = Directory.GetFiles(Server.MapPath("~/"), "*.aspx", SearchOption.TopDirectoryOnly);
        ddlLinkPicker.Items.Add(new ListItem("--- Select Link ---", string.Empty));
        // Add more predefined links as needed

        foreach (string filePath in filePaths)
        {
            string relativePath = filePath.Replace(Server.MapPath("~/"), "").Replace("\\", "/");
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            ddlLinkPicker.Items.Add(new ListItem(fileName, relativePath));
        }
    }

    protected void chkboxExt_CheckedChanged(object sender, EventArgs e)
    {
        if (chkboxExt.Checked)
        {
            // Enable the external link URL textbox, disable the local link dropdown
            txtLinkURL.Enabled = true;
            ddlLinkPicker.Enabled = false;

            // Clear any selected value in the dropdown to avoid confusion
            ddlLinkPicker.ClearSelection();

            // Set focus to the URL textbox
            txtLinkURL.Focus();
        }
        else
        {
            // Disable the external link URL textbox and enable the local link dropdown
            txtLinkURL.Enabled = false;
            ddlLinkPicker.Enabled = true;

            // Clear the URL textbox since it's not relevant for local links
            txtLinkURL.Text = string.Empty;

            // Set focus to the dropdown list
            ddlLinkPicker.Focus();
        }
    }

    protected void ddlLinkPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLinkPicker.SelectedValue != string.Empty)
        {
            txtLinkURL.Text = ddlLinkPicker.SelectedValue;
        }
    }
}