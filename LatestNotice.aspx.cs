using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

public partial class pages_Notice : System.Web.UI.Page
{
    private int pageSize = 10;

    // Property to persist currentPageIndex using ViewState
    private int CurrentPageIndex
    {
        get
        {
            return ViewState["CurrentPageIndex"] != null ? (int)ViewState["CurrentPageIndex"] : 0;
        }
        set
        {
            ViewState["CurrentPageIndex"] = value;
        }
    }

    private int TotalRecords
    {
        get
        {
            return ViewState["TotalRecords"] != null ? (int)ViewState["TotalRecords"] : 0;
        }
        set
        {
            ViewState["TotalRecords"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pageSize = int.Parse(ddlRecordsPerPage.SelectedValue);
            BindGridView();
        }
    }

    private void BindGridView(string searchQuery = "")
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string baseQuery = "SELECT DocsID, No, Title, Date, FilePath FROM Docs WHERE Type = 'Notice'";
            if (!string.IsNullOrEmpty(searchQuery))
            {
                baseQuery += " AND Title LIKE @SearchQuery";
            }

            string paginatedQuery =
                "WITH PaginatedData AS (" +
                "    SELECT ROW_NUMBER() OVER (ORDER BY Date DESC) AS RowNum, DocsID, No, Title, Date, FilePath " +
                "    FROM Docs WHERE Type = 'Notice' " +
                (string.IsNullOrEmpty(searchQuery) ? "" : "AND Title LIKE @SearchQuery ") +
                ") " +
                "SELECT DocsID, No, Title, Date, FilePath " +
                "FROM PaginatedData " +
                "WHERE RowNum BETWEEN @StartRow AND @EndRow";

            using (SqlCommand cmd = new SqlCommand(paginatedQuery, conn))
            {
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    cmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                }

                int startRow = (CurrentPageIndex * pageSize) + 1;
                int endRow = startRow + pageSize - 1;
                cmd.Parameters.AddWithValue("@StartRow", startRow);
                cmd.Parameters.AddWithValue("@EndRow", endRow);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

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

                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }

            // Count total records
            string countQuery = "SELECT COUNT(*) FROM Docs WHERE Type = 'Notice'";
            if (!string.IsNullOrEmpty(searchQuery))
            {
                countQuery += " AND Title LIKE @SearchQuery";
            }

            using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
            {
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    countCmd.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                }

                conn.Open();
                TotalRecords = (int)countCmd.ExecuteScalar();
                conn.Close();
            }
        }

        UpdatePaginationInfo();
    }

    private void UpdatePaginationInfo()
    {
        int totalPages = (int)Math.Ceiling((double)TotalRecords / pageSize);
        lblPageInfo.Text = "Page " + (CurrentPageIndex + 1) + " of " + totalPages;

        btnPrevPage.Enabled = CurrentPageIndex > 0;
        btnNextPage.Enabled = CurrentPageIndex < totalPages - 1;
    }

    protected void btnPrevPage_Click(object sender, EventArgs e)
    {
        if (CurrentPageIndex > 0)
        {
            CurrentPageIndex--;
            BindGridView(txtSearch.Text.Trim());
        }
    }

    protected void btnNextPage_Click(object sender, EventArgs e)
    {
        int totalPages = (int)Math.Ceiling((double)TotalRecords / pageSize);
        if (CurrentPageIndex < totalPages - 1)
        {
            CurrentPageIndex++;
            BindGridView(txtSearch.Text.Trim());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrentPageIndex = 0;
        BindGridView(txtSearch.Text.Trim());
    }

    protected void ddlRecordsPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        pageSize = int.Parse(ddlRecordsPerPage.SelectedValue);
        CurrentPageIndex = 0;
        BindGridView(txtSearch.Text.Trim());
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        lblMessage.Text = string.Empty;
        txtSearch.Text = string.Empty;
        BindGridView("");
    }
}

