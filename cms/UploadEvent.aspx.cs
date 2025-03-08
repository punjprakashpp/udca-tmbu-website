using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;

public partial class UploadEvent : System.Web.UI.Page
{
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string noticeTitle = txtNoticeText.Text.Trim();
        DateTime uploadDate = DateTime.Now;
        DateTime noticeDate;
        string filePath = null;

        if (!string.IsNullOrEmpty(noticeTitle))
        {
            try
            {
                noticeDate = Convert.ToDateTime(txtNoticeDate.Text.Trim());

                if (fileUpload.HasFile)
                {
                    string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                    string formattedDate = noticeDate.ToString("dd-MM-yyyy");

                    StringBuilder safeTitleBuilder = new StringBuilder();
                    foreach (char c in noticeTitle)
                    {
                        if (char.IsLetterOrDigit(c))
                        {
                            safeTitleBuilder.Append(c);
                        }
                    }
                    string safeTitle = safeTitleBuilder.ToString();
                    string fileName = "Event_" + formattedDate + "_" + safeTitle + ".pdf";

                    string uploadFolder = Server.MapPath("~/uploads/event/");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    filePath = Path.Combine(uploadFolder, fileName);

                    if (fileExtension == ".pdf")
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

                    string relativeFilePath = "uploads/event/" + fileName;
                    string connStr = ConfigurationManager.ConnectionStrings["WebsiteConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "INSERT INTO Docs (Type, Title, Date, FilePath, UploadDate) VALUES (@Type, @Title, @Date, @FilePath, @UploadDate)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Type", "Event");
                            cmd.Parameters.AddWithValue("@Title", noticeTitle);
                            cmd.Parameters.AddWithValue("@Date", noticeDate);
                            cmd.Parameters.AddWithValue("@FilePath", relativeFilePath);
                            cmd.Parameters.AddWithValue("@UploadDate", uploadDate);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            lblMessage.Text = "Event uploaded successfully!";
                            lblMessage.ForeColor = Color.Green;

                            txtNoticeText.Text = string.Empty;
                            txtNoticeDate.Text = string.Empty;
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
            catch (FormatException)
            {
                lblMessage.Text = "Please enter a valid date in the format dd-MM-yyyy.";
                lblMessage.ForeColor = Color.Red;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "An error occurred: " + ex.Message;
                lblMessage.ForeColor = Color.Red;
            }
        }
        else
        {
            lblMessage.Text = "Please enter a valid Event Title and Event Date.";
            lblMessage.ForeColor = Color.Red;
        }
    }

    protected void btnManage_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageEvents.aspx");
    }
}