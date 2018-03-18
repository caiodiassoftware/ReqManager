using iTextSharp.text;
using iTextSharp.text.pdf;
using ReqManager.Services.Directories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ReqManager.Controllers
{
    [HandleError(ExceptionType = typeof(Exception), View = "Error")]
    public class FileController : Controller
    {
        private IScanDirectoryService directory { get; set; }

        public FileController(IScanDirectoryService directory)
        {
            this.directory = directory;
        }

        public FileResult RenderFile(string FilePath, string Title)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
            string fileName = Title + Path.GetExtension(FilePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

            //renderFile(FilePath, Title);
        }

        public JsonResult GetFolders(string path)
        {
            try
            {
                if (CheckExtensions(Path.GetExtension(path)))
                {
                    renderFile(path, "OpenFile");
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(directory.getFolders(path), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void renderFile(string FilePath, string Title)
        {
            try
            {
                string extension = Path.GetExtension(FilePath);
                byte[] bytes = null;

                if (!CheckExtensions(extension))
                {
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.ContentType = "application/octet-stream";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.AddHeader("content-disposition", "attachment;filename= " + Title + DateTime.Now.ToString() + ".pdf");
                    Response.Buffer = true;
                    Response.Clear();
                    bytes = ReadAllBytes(FilePath);
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.OutputStream.Flush();
                }
                else
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    try
                    {
                        proc.EnableRaisingEvents = false;
                        proc.StartInfo.Arguments = FilePath;
                        proc.StartInfo.FileName = FilePath;
                        proc.StartInfo.UseShellExecute = true;
                        proc.Start();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        proc.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private byte[] ReadAllBytes(string fileName)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var doc = new Document(PageSize.A4, 40, 40, 40, 80))
                    {
                        using (var writer = PdfWriter.GetInstance(doc, ms))
                        {
                            doc.Open();
                            string file = System.IO.File.ReadAllText(fileName);
                            doc.Add(new Paragraph(file));
                            doc.Close();
                        }
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckExtensions(string extension)
        {
            List<String> extensions = new List<String>();
            extensions.Add(".jpg");
            extensions.Add(".png");
            extensions.Add(".svg");
            extensions.Add(".bpm");
            extensions.Add(".pdf");
            extensions.Add(".txt");
            extensions.Add(".doc");
            extensions.Add(".docx");
            extensions.Add(".xlsx");
            extensions.Add(".csv");
            extensions.Add(".xls");
            return extensions.Contains(extension);
        }
    }
}