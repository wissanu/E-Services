using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Git_system.Controllers
{
    public class AuthenticationFileController : Controller
    {
        //
        // GET: /AuthenticationFile/
        public string CheckAuthenticationForUploadFileInToDir(string filename)
        {
            string outp = "";
            outp += "Name : " + User.Identity.Name + "<br>";
            outp += "AuthenticationType : " + User.Identity.AuthenticationType + "<br>";
            outp += "IsAuthenticated : " + User.Identity.IsAuthenticated + "<br>";
            outp += "IsInRole : " + User.IsInRole("Administrators") + "<br>";
            outp += "GetHashCode : " + User.GetHashCode() + "<br>";
            outp += "<hr>";

            if (User.Identity.IsAuthenticated)
            {
                System.Web.Security.FormsIdentity FormsIdentity;
                FormsIdentity = (System.Web.Security.FormsIdentity)HttpContext.User.Identity;
                outp += "CookiePath : " + FormsIdentity.Ticket.CookiePath + "<br>";
                outp += "Expiration : " + FormsIdentity.Ticket.Expiration + "<br>";
                outp += "Expired : " + FormsIdentity.Ticket.Expired + "<br>";
                outp += "IsPersistent : " + FormsIdentity.Ticket.IsPersistent + "<br>";
                outp += "IssueDate : " + FormsIdentity.Ticket.IssueDate + "<br>";
                outp += "Name : " + FormsIdentity.Ticket.Name + "<br>";
                outp += "UserData : " + FormsIdentity.Ticket.UserData + "<br>";
                outp += "Version : " + FormsIdentity.Ticket.Version + "<br>";
            }

            string ServerMapPath = Server.MapPath(@"~/E-Commerce/Confirm/" + filename);
            StreamReader reader = new StreamReader(ServerMapPath);
            //reader.CurrentEncoding.GetBytes(reader.ReadToEnd());
            //string path = "~/E-Commerce/Confirm/";

            //CheckAuthenticationForUploadFileInToDirAndDownloadFile(filename, path);
            return outp + "Test output : " + filename + "out ";
        }

        /// Ex.
        /// http://localhost:52488/AuthenticationFile/EComDownload?filename=i1.jpg&path=~/E-Commerce/Confirm/&downloadname=i1.jpg
        /// <summary>
        /// ทำการรับชื่อไฟล์ ที่อยู่ไฟล์ และชื่อที่จะดาวโหลด และทำการโหลดไฟล์
        /// </summary>
        /// <param name="filename">ชื่อไฟล์ที่จะโหลด</param>
        /// <param name="path">ที่อยู่ไฟล์</param>
        /// <param name="downloadname">ชื่อไฟล์เมื่อดาวโหลด</param>
        public void EComDownload(string filename, string path, string downloadname)
        {
            string ServerMapPath;
            byte[] fileBytes;
            try
            {
                ServerMapPath = Server.MapPath(path + filename);
                fileBytes = System.IO.File.ReadAllBytes(ServerMapPath);
                if (downloadname == "" || downloadname == null)
                    StreamFileToBrowser(filename, fileBytes);
                else
                    StreamFileToBrowser(filename, fileBytes, downloadname);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Gets the MIME type of the file name specified based on the file name's
        /// extension.  If the file's extension is unknown, returns "octet-stream"
        /// generic for streaming file bytes.
        /// </summary>
        /// <param name="sFileName">The name of the file for which the MIME type
        /// refers to.</param>
        private string GetMimeTypeByFileName(string sFileName)
        {
            string sMime = "application/octet-stream";

            string sExtension = System.IO.Path.GetExtension(sFileName);
            if (!string.IsNullOrEmpty(sExtension))
            {
                sExtension = sExtension.Replace(".", "");
                sExtension = sExtension.ToLower();

                if (sExtension == "xls" || sExtension == "xlsx")
                {
                    sMime = "application/ms-excel";
                }
                else if (sExtension == "doc" || sExtension == "docx")
                {
                    sMime = "application/msword";
                }
                else if (sExtension == "ppt" || sExtension == "pptx")
                {
                    sMime = "application/ms-powerpoint";
                }
                else if (sExtension == "rtf")
                {
                    sMime = "application/rtf";
                }
                else if (sExtension == "zip")
                {
                    sMime = "application/zip";
                }
                else if (sExtension == "mp3")
                {
                    sMime = "audio/mpeg";
                }
                else if (sExtension == "bmp")
                {
                    sMime = "image/bmp";
                }
                else if (sExtension == "gif")
                {
                    sMime = "image/gif";
                }
                else if (sExtension == "jpg" || sExtension == "jpeg")
                {
                    sMime = "image/jpeg";
                }
                else if (sExtension == "png")
                {
                    sMime = "image/png";
                }
                else if (sExtension == "tiff" || sExtension == "tif")
                {
                    sMime = "image/tiff";
                }
                else if (sExtension == "txt")
                {
                    sMime = "text/plain";
                }
            }

            return sMime;
        }

        /// <summary>
        /// Streams the bytes specified as a file with the name specified using HTTP to the
        /// calling browser.
        /// </summary>
        /// <param name="sFileName">The name of the file as it will apear when the user
        /// clicks either open or save as in their browser to accept the file
        /// download.</param>
        /// <param name="fileBytes">The file as a byte array to be streamed.</param>
        private void StreamFileToBrowser(string sFileName, byte[] fileBytes)
        {
            StreamFileToBrowser(sFileName, fileBytes, sFileName);
        }

        /// <summary>
        /// Streams the bytes specified as a file with the name specified using HTTP to the
        /// calling browser.
        /// </summary>
        /// <param name="sFileName">The name of the file as it will apear when the user
        /// clicks either open or save as in their browser to accept the file
        /// download.</param>
        /// <param name="fileBytes">The file as a byte array to be streamed.</param>
        private void StreamFileToBrowser(string sFileName, byte[] fileBytes, string reName)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ClearContent();
            context.Response.AppendHeader("content-length", fileBytes.Length.ToString());
            context.Response.ContentType = GetMimeTypeByFileName(sFileName);
            context.Response.AppendHeader("content-disposition", "attachment; filename=" + reName);
            context.Response.BinaryWrite(fileBytes);

            // use this instead of response.end to avoid thread aborted exception (known issue):
            // http://support.microsoft.com/kb/312629/EN-US
            context.ApplicationInstance.CompleteRequest();
        }
    }
}