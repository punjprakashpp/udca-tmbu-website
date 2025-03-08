using System;
using System.Data;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cms_ManageHighlightLinks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGridView();
    }

    protected void gvLinks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLinks.PageIndex = e.NewPageIndex;
        BindGridView();
    }

    private void BindGridView()
    {
        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
                SELECT LinkID, LinkText, LinkURL,
                    ROW_NUMBER() OVER (ORDER BY UploadDate) AS RowNum
                FROM Links
                WHERE Type = 'Link' 
                    AND (@LinkText IS NULL OR LinkText LIKE '%' + @LinkText + '%')";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LinkText", string.IsNullOrEmpty(txtSearch.Text) ? (object)DBNull.Value : txtSearch.Text);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        sda.Fill(dt);

                        lblMessage.Text = dt.Rows.Count == 0 ? "No records found." : string.Empty;
                        lblMessage.ForeColor = Color.Red;

                        gvLinks.DataSource = dt;
                        gvLinks.DataBind();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "An error occurred while retrieving data: " + ex.Message;
                        lblMessage.ForeColor = Color.Red;
                    }
                }
            }
        }
    }

    protected void gvLinks_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvLinks.EditIndex = e.NewEditIndex;
        BindGridView();
    }

    protected void gvLinks_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gvLinks.Rows[e.RowIndex];
        int linkID = Convert.ToInt32(gvLinks.DataKeys[e.RowIndex].Values[0]);

        TextBox txtLinkText = (TextBox)row.FindControl("txtLinkText");
        TextBox txtLinkURL = (TextBox)row.FindControl("txtLinkURL");
        DropDownList ddlLinkPicker = (DropDownList)row.FindControl("ddlLinkPicker");

        string linkText = txtLinkText.Text;
        string linkURL = txtLinkURL.Text;

        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "UPDATE Links SET LinkText=@LinkText, LinkURL=@LinkURL, UploadDate=@UploadDate WHERE LinkID=@LinkID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LinkText", linkText);
                cmd.Parameters.AddWithValue("@LinkURL", linkURL);
                cmd.Parameters.AddWithValue("@LinkID", linkID);
                cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Link updated successfully.";
                    lblMessage.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                    lblMessage.ForeColor = Color.Red;
                }
                finally
                {
                    gvLinks.EditIndex = -1;
                    BindGridView();
                }
            }
        }
    }

    protected void gvLinks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvLinks.EditIndex = -1;
        BindGridView();
    }

    protected void gvLinks_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int linkID = Convert.ToInt32(gvLinks.DataKeys[e.RowIndex].Values[0]);

        string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "DELETE FROM Links WHERE LinkID=@LinkID";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LinkID", linkID);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Link deleted successfully.";
                    lblMessage.ForeColor = Color.Green;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                    lblMessage.ForeColor = Color.Red;
                }
                finally
                {
                    BindGridView();
                }
            }
        }
    }

    protected void gvLinks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) > 0)
        {
            DropDownList ddlLinkPicker = (DropDownList)e.Row.FindControl("ddlLinkPicker");
            TextBox txtLinkURL = (TextBox)e.Row.FindControl("txtLinkURL");
            CheckBox chkboxExt = (CheckBox)e.Row.FindControl("chkboxExt");

            // Populate dropdown list with .aspx files in the root directory
            ddlLinkPicker.Items.Insert(0, new ListItem("--Select Link--", string.Empty));
            foreach (string filePath in Directory.GetFiles(Server.MapPath("~/"), "*.aspx"))
            {
                string relativePath = filePath.Replace(Server.MapPath("~/"), "").Replace("\\", "/");
                ddlLinkPicker.Items.Add(new ListItem(Path.GetFileNameWithoutExtension(filePath), relativePath));
            }

            // Initial setting based on current URL type
            string currentLinkURL = DataBinder.Eval(e.Row.DataItem, "LinkURL").ToString();
            if (currentLinkURL.StartsWith("http"))
            {
                chkboxExt.Checked = true;
                txtLinkURL.Enabled = true;
                ddlLinkPicker.Enabled = false;
                txtLinkURL.Text = currentLinkURL;
            }
            else
            {
                chkboxExt.Checked = false;
                txtLinkURL.Enabled = false;
                ddlLinkPicker.Enabled = true;
                ddlLinkPicker.SelectedValue = currentLinkURL;
            }
        }
    }

    protected void chkboxExt_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;

        TextBox txtLinkURL = (TextBox)row.FindControl("txtLinkURL");
        DropDownList ddlLinkPicker = (DropDownList)row.FindControl("ddlLinkPicker");
        CheckBox chkboxExt = (CheckBox)row.FindControl("chkboxExt");

        txtLinkURL.Enabled = chkboxExt.Checked;
        ddlLinkPicker.Enabled = !chkboxExt.Checked;

        if (!chkboxExt.Checked)
        {
            txtLinkURL.Text = string.Empty;
            ddlLinkPicker.SelectedIndex = 0;
        }
    }

    protected void ddlLinkPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = (sender as DropDownList).NamingContainer as GridViewRow;

        TextBox txtLinkURL = (TextBox)row.FindControl("txtLinkURL");
        DropDownList ddlLinkPicker = (DropDownList)row.FindControl("ddlLinkPicker");
        CheckBox chkboxExt = (CheckBox)row.FindControl("chkboxExt");

        if (ddlLinkPicker.SelectedValue != string.Empty)
        {
            txtLinkURL.Text = ddlLinkPicker.SelectedValue;
        }

        chkboxExt.Checked = false;
        txtLinkURL.Enabled = false;
    }
}
