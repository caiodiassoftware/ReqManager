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

        public void RenderFile(string FilePath, string Title)
        {
            try
            {
                string extension = Path.GetExtension(FilePath);

                if (!CheckExtensions(FilePath))
                {
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.ContentType = "application/octet-stream";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.AddHeader("content-disposition", "inline;filename= " + Title + DateTime.Now.ToString() + ".pdf");
                    Response.Buffer = true;
                    Response.Clear();
                    var bytes = ReadAllBytes(FilePath);
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.OutputStream.Flush();
                }
                else
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = FilePath;
                    proc.Start();
                    proc.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetFolders(string path)
        {
            try
            {
                dynamic json = "Search Again!";

                if(Directory.Exists(path))
                    json = directory.getFolders(path);
                return Json(json, JsonRequestBehavior.AllowGet);
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
            extensions.Add(".pdf");
            return extensions.Contains(extension);
        }
    }
}