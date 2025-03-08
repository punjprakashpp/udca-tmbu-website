using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class UploadOtherFile : System.Web.UI.Page
{
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string noticeTitle = txtNoticeText.Text.Trim();
        string filePath = null;

        if (!string.IsNullOrEmpty(noticeTitle))
        {
            try
            {
                if (fileUpload.HasFile)
                {
                    string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

                    StringBuilder safeTitleBuilder = new StringBuilder();
                    foreach (char c in noticeTitle)
                    {
                        if (char.IsLetterOrDigit(c))
                        {
                            safeTitleBuilder.Append(c);
                        }
                    }
                    string safeTitle = safeTitleBuilder.ToString();
                    string fileName = "File_" + safeTitle + (fileExtension==".zip"?".zip":".pdf");

                    string uploadFolder = Server.MapPath("~/uploads/Files/");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    filePath = Path.Combine(uploadFolder, fileName);

                    if (fileExtension == ".pdf" || fileExtension == ".zip")
                    {
                        fileUpload.SaveAs(filePath);
                    }
                    else if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                    {
                        using (var ms = new MemoryStream(fileUpload.FileBytes))
                        using (var img = System.Drawing.Image.FromStream(ms))
                        {
                            var doc = new Document(PageSize.A4, 50, 50, 50, 50);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                PdfWriter.GetInstance(doc, stream);
                                doc.Open();

                                // Compress image by converting to JPEG format with 50% quality
                                using (var compressedMs = new MemoryStream())
                                {
                                    var jpegEncoder = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders()
                                                       .FirstOrDefault(codec => codec.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                                    if (jpegEncoder != null)
                                    {
                                        var encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                                        encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L);

                                        img.Save(compressedMs, jpegEncoder, encoderParams);
                                        compressedMs.Seek(0, SeekOrigin.Begin);

                                        var pdfImg = iTextSharp.text.Image.GetInstance(compressedMs);
                                        pdfImg.Alignment = Element.ALIGN_CENTER;
                                        pdfImg.ScaleToFit(doc.PageSize.Width - 100, doc.PageSize.Height - 100);

                                        doc.Add(pdfImg);
                                    }
                                    else
                                    {
                                        lblMessage.Text = "Error: JPEG encoder not found.";
                                        lblMessage.ForeColor = Color.Red;
                                        return;
                                    }
                                }

                                doc.Close();
                            }
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Only PDF or image files are allowed.";
                        lblMessage.ForeColor = Color.Red;
                        return;
                    }

                    string relativeFilePath = "uploads/Files/" + fileName;
                    string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "INSERT INTO Docs (Type, Title, FilePath, UploadDate) VALUES (@Type, @Title, @FilePath, @UploadDate)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Type", "Other");
                            cmd.Parameters.AddWithValue("@Title", noticeTitle);
                            cmd.Parameters.AddWithValue("@FilePath", relativeFilePath);
                            cmd.Parameters.AddWithValue("@UploadDate", DateTime.Now);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            lblMessage.Text = "File uploaded successfully!";
                            lblMessage.ForeColor = Color.Green;

                            txtNoticeText.Text = string.Empty;
                            fileUpload.Attributes.Clear();
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "Please upload a PDF or image file.";
                    lblMessage.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
        }
        else
        {
            lblMessage.Text = "Please enter a valid File Title";
            lblMessage.ForeColor = Color.Red;
        }
    }

    protected void btnManage_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageOtherFiles.aspx");
    }
}