using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Drawing;

public partial class OurAlumni : System.Web.UI.Page
{
    private int currentPage;
    private int recordsPerPage;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["CurrentPage"] = 0;
            ViewState["RecordsPerPage"] = 10;
            LoadFacultyDetails();
            PopulateSessionDropdown();
        }
        else
        {
            currentPage = (int)ViewState["CurrentPage"];
            recordsPerPage = (int)ViewState["RecordsPerPage"];
        }
    }

    private void PopulateSessionDropdown()
    {
        ddlSession.Items.Clear();
        ddlSession.Items.Add(new ListItem("--- Select Session ---", string.Empty));

        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        string query = "SELECT DISTINCT Session FROM Student ORDER BY Session";

        using (SqlConnection conn = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string session = reader["Session"].ToString();
                    ddlSession.Items.Add(new ListItem(session, session));
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text= "Error loading sessions: " + ex.Message;
                lblmsg.ForeColor = Color.Red;
            }
        }
    }

    private void LoadFacultyDetails()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"SELECT FirstName, MidName, LastName, Session, Qualification, Achievement, Occupation, 
                                    Company, Phone, Email, FilePath, LinkedIn, Facebook, Instagram, Twitter
                            FROM Student
                            WHERE (@Session IS NULL OR Session = @Session) 
                            AND Alumnus = 'yes'
                            ORDER BY Session";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Session", string.IsNullOrEmpty(ddlSession.SelectedValue) ? (object)DBNull.Value : ddlSession.SelectedItem.ToString());

            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                DataTable dt = new DataTable();
                dt.Load(reader);

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

                PagedDataSource pds = new PagedDataSource();
                pds.DataSource = dt.DefaultView;
                pds.AllowPaging = true;
                pds.PageSize = recordsPerPage;
                pds.CurrentPageIndex = currentPage;

                ViewState["TotalPages"] = pds.PageCount;

                btnPrevious.Enabled = !pds.IsFirstPage;
                btnNext.Enabled = !pds.IsLastPage;

                facultyRepeater.DataSource = pds;
                facultyRepeater.DataBind();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ViewState["CurrentPage"] = 0;
        LoadFacultyDetails();
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        currentPage = (int)ViewState["CurrentPage"] - 1;
        ViewState["CurrentPage"] = currentPage;
        LoadFacultyDetails();
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        currentPage = (int)ViewState["CurrentPage"] + 1;
        ViewState["CurrentPage"] = currentPage;
        LoadFacultyDetails();
    }

    protected void ddlRecordsPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["RecordsPerPage"] = int.Parse(ddlRecordsPerPage.SelectedValue);
        LoadFacultyDetails();
    }
}