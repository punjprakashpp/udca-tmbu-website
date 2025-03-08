using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;

public partial class Admin_pages_EditDeleteMarqueeLinks : System.Web.UI.Page
{
    private int PageSize
    {
        get { return int.Parse(ddlRecordsPerPage.SelectedValue); }
    }

    private int CurrentPage
    {
        get { return ViewState["CurrentPage"] != null ? (int)ViewState["CurrentPage"] : 0; }
        set { ViewState["CurrentPage"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    private void BindGridView()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT * FROM " +
                           "(SELECT ROW_NUMBER() OVER (ORDER BY LinkID) AS RowNum, LinkID, LinkText, LinkURL " +
                           "FROM Links WHERE Type = 'Video'";
            string whereClause = "";
            if (!string.IsNullOrEmpty(txtSearchVideoLabel.Text))
            {
                whereClause = " AND LinkText LIKE @SearchText";
            }

            query += whereClause + ") AS RowConstrainedResult WHERE RowNum >= @StartRow AND RowNum < @EndRow ORDER BY RowNum";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (!string.IsNullOrEmpty(txtSearchVideoLabel.Text))
                {
                    cmd.Parameters.AddWithValue("@SearchText", "%" + txtSearchVideoLabel.Text + "%");
                }

                int startRow = CurrentPage * PageSize + 1;
                int endRow = startRow + PageSize;

                cmd.Parameters.AddWithValue("@StartRow", startRow);
                cmd.Parameters.AddWithValue("@EndRow", endRow);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        CurrentPage = 0;
        BindGridView();
    }

    protected void ddlRecordsPerPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        CurrentPage = 0;
        BindGridView();
    }

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        if (CurrentPage > 0)
        {
            CurrentPage--;
            BindGridView();
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        CurrentPage++;
        BindGridView();
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        BindGridView();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = GridView1.Rows[e.RowIndex];
        int VideoID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

        TextBox txtVideoText = (TextBox)row.FindControl("txtVideoText");
        TextBox txtVideoURL = (TextBox)row.FindControl("txtVideoURL");

        string VideoText = txtVideoText.Text;
        string VideoURL = txtVideoURL.Text;

        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "UPDATE Links SET LinkText=@LinkText, LinkURL=@LinkURL WHERE LinkID=@LinkID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LinkText", VideoText);
                cmd.Parameters.AddWithValue("@LinkURL", VideoURL);
                cmd.Parameters.AddWithValue("@LinkID", VideoID);

                conn.Open();
                cmd.ExecuteNonQuery();
                GridView1.EditIndex = -1;
                BindGridView();
            }
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        BindGridView();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int VideoID = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "DELETE FROM Links WHERE LinkID=@LinkID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LinkID", VideoID);
                conn.Open();
                cmd.ExecuteNonQuery();
                BindGridView();
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Optionally, add any custom logic for data bound rows here.
    }
}
