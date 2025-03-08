using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Drawing;

public partial class DownloadIQPapers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulateSessionDropDown();
            populateSemesterDropdown();
        }
    }

    private void populateSemesterDropdown()
    {
        ddlSemester.Items.Add(new ListItem("--- Select-Semester ---", string.Empty));
        ddlSemester.Items.Add(new ListItem("Semester 1", "Semester-1"));
        ddlSemester.Items.Add(new ListItem("Semester 2", "Semester-2"));
        ddlSemester.Items.Add(new ListItem("Semester 3", "Semester-3"));
        ddlSemester.Items.Add(new ListItem("Semester 4", "Semester-4"));
    }

    private void PopulateSessionDropDown()
    {
        ddlSession.Items.Add(new ListItem("--- Select-Session ---", string.Empty));
        int currentYear = DateTime.Now.Year;
        for (int year = 2020; year <= currentYear; year++)
        {
            ddlSession.Items.Add(new ListItem(year.ToString() + " - " + (year + 2).ToString(), year.ToString() + "-" + (year + 2).ToString()));
        }
    }

    protected void btnShowFiles_Click(object sender, EventArgs e)
    {
        string semester = ddlSemester.SelectedValue;
        string session = ddlSession.SelectedValue;

        string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT DocsID, Title FROM Docs WHERE Type = @Type";

            if (ddlSemester.SelectedValue != string.Empty)
            {
                query += " AND Semester = @Semester";
            }

            if (ddlSession.Enabled & (ddlSession.SelectedValue != string.Empty))
            {
                query += " AND Session = @Session";
            }

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Type", "Internal");
                cmd.Parameters.AddWithValue("@Semester", semester);
                if (ddlSession.Enabled)
                {
                    cmd.Parameters.AddWithValue("@Session", session);
                }

                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    lblMessage.Text = "No records found.";
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = ""; // Hide the message if records are found
                }

                GridViewFiles.DataSource = dt;
                GridViewFiles.DataBind();
                conn.Close();
            }
        }
    }

    protected void GridViewFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string connectionString = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FilePath FROM Docs WHERE DocsID = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    string filePath = cmd.ExecuteScalar().ToString();
                    conn.Close();

                    string fullPath = Server.MapPath("~/" + filePath);
                    if (File.Exists(fullPath))
                    {
                        try
                        {
                            Response.Clear();
                            Response.ContentType = "application/octet-stream";
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(fullPath));
                            Response.TransmitFile(fullPath);
                            Response.End();
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Error in file download: " + ex.Message;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        // Handle file not found scenario
                        lblMessage.Text = "File not found!";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMessage.Text = string.Empty;
        ddlSemester.SelectedIndex = 0;
        ddlSession.SelectedIndex = 0;
        GridViewFiles.DataSource = null;
    }
}