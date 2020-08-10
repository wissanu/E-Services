using System;
using System.Collections.Generic;
using System.Web;

namespace Git_system.Models.ECom
{
    public class RandomId
    {
        public string sId { get; set; }

        public long iId { get; set; }

        public RandomId()
        {
            long ticksTime = DateTime.Now.Ticks;
            this.sId = Git_system.App_Code.Base36.Encode(ticksTime);
            this.iId = ticksTime;
        }
    }

    public class FileLink
    {
        public FileLink()
        {
            this.FileId = new RandomId().sId;
        }

        public string FileId { get; set; }

        public string Filename { get; set; }

        public string SaveFilename { get; set; }

        public string Directory { get; set; }

        public string FilePath { get; set; }
    }

    public static class FileLinkExtension
    {
        /// <summary>
        /// ทำการอัพโหลดไฟล์ และ ทำการบันทึกลง Object
        /// </summary>
        /// <param name="fileLinks">ไฟล์ทั้งหมด</param>
        /// <param name="fileUpload">ไฟล์ที่ต้องการอัพโหลด</param>
        /// <param name="directory">ที่อยู่ที่จะเก็บ</param>
        /// <param name="pathDirectoryFile">ที่อยู่ที่จะเก็บรูปแบบเต็ม</param>
        /// <returns>ไฟล์ทั้งหมดที่ทำการเพิ่มสำเร็จ</returns>
        public static List<FileLink> UploadAndSave(this List<FileLink> fileLinks, HttpPostedFileBase fileUpload, string directory)
        {
            if (Git_system.App_Code.UploadFile.checkFileNull(fileUpload))
            {
                string pathDirectoryFile = HttpContext.Current.Server.MapPath(directory);
                Git_system.App_Code.UploadFileStatus uploadStatus = Git_system.App_Code.UploadFile.uploadFile(fileUpload, pathDirectoryFile);

                if (uploadStatus.status == true)
                {
                    FileLink fileLinkImage = new FileLink();
                    fileLinkImage.FileId = fileLinkImage.FileId + Git_system.App_Code.Cryptography.SHA1(fileUpload.FileName).Substring(20);
                    fileLinkImage.Filename = fileUpload.FileName;
                    fileLinkImage.Directory = directory;
                    fileLinkImage.SaveFilename = uploadStatus.filename;
                    fileLinkImage.FilePath = directory + uploadStatus.filename;
                    fileLinks.Add(fileLinkImage);
                }
            }
            return fileLinks;
        }

        /// <summary>
        /// ทำการอัพโหลดไฟล์ และ ทำการบันทึกลง Object
        /// </summary>
        /// <param name="fileLinks">ไฟล์ทั้งหมด</param>
        /// <param name="fileUpload">ไฟล์ที่ต้องการอัพโหลด</param>
        /// <param name="directory">ที่อยู่ที่จะเก็บ</param>
        /// <param name="pathDirectoryFile">ที่อยู่ที่จะเก็บรูปแบบเต็ม</param>
        /// <returns>ไฟล์ทั้งหมดที่ทำการเพิ่มสำเร็จ</returns>
        public static List<FileLink> UploadAndSave(this List<FileLink> fileLinks, HttpPostedFile fileUpload, string directory)
        {
            if (Git_system.App_Code.UploadFile.checkFileNull(fileUpload))
            {
                string pathDirectoryFile = HttpContext.Current.Server.MapPath(directory);
                Git_system.App_Code.UploadFileStatus uploadStatus = Git_system.App_Code.UploadFile.uploadFile(fileUpload, pathDirectoryFile);

                if (uploadStatus.status == true)
                {
                    FileLink fileLinkImage = new FileLink();
                    fileLinkImage.FileId = fileLinkImage.FileId + Git_system.App_Code.Cryptography.SHA1(fileUpload.FileName).Substring(20);
                    fileLinkImage.Filename = fileUpload.FileName;
                    fileLinkImage.Directory = directory;
                    fileLinkImage.SaveFilename = uploadStatus.filename;
                    fileLinkImage.FilePath = directory + uploadStatus.filename;
                    fileLinks.Add(fileLinkImage);
                }
            }
            return fileLinks;
        }

        /// <summary>
        /// ทำการอัพไฟล์ทั้งหมด และ คืนค่าเป็น Class FileLink เป็น List
        /// </summary>
        /// <param name="arrayFileUpload">อาเรย์ของไฟล์ที่จะอัพโหลด</param>
        /// <param name="directory">ที่อยู่ที่จะเก็บ</param>
        /// <returns>ค่าเป็น List ของ FileLink ที่ทำการอัพโหลดเสร็จแล้ว</returns>
        public static List<FileLink> UploadAndSave(this HttpPostedFileBase[] arrayFileUpload, string directory)
        {
            List<FileLink> fileLinks = new List<FileLink>();
            if (arrayFileUpload != null)
            {
                foreach (HttpPostedFileBase fileUpload in arrayFileUpload)
                {
                    fileLinks = fileLinks.UploadAndSave(fileUpload, directory);
                }
            }

            return fileLinks;
        }

        /// <summary>
        /// ทำการอัพไฟล์ทั้งหมด และ คืนค่าเป็น Class FileLink เป็น List
        /// </summary>
        /// <param name="arrayFileUpload">อาเรย์ของไฟล์ที่จะอัพโหลด</param>
        /// <param name="directory">ที่อยู่ที่จะเก็บ</param>
        /// <returns>ค่าเป็น List ของ FileLink ที่ทำการอัพโหลดเสร็จแล้ว</returns>
        public static List<FileLink> UploadAndSave(this HttpPostedFile[] arrayFileUpload, string directory)
        {
            List<FileLink> fileLinks = new List<FileLink>();
            if (arrayFileUpload != null)
            {
                foreach (HttpPostedFile fileUpload in arrayFileUpload)
                {
                    fileLinks = fileLinks.UploadAndSave(fileUpload, directory);
                }
            }

            return fileLinks;
        }

        /// <summary>
        /// ทำการอัพไฟล์ทั้งหมด และ คืนค่าเป็น Class FileLink เป็น List
        /// </summary>
        /// <param name="arrayFileUpload">อาเรย์ของไฟล์ที่จะอัพโหลด</param>
        /// <param name="directory">ที่อยู่ที่จะเก็บ</param>
        /// <returns>ค่าเป็น List ของ FileLink ที่ทำการอัพโหลดเสร็จแล้ว</returns>
        public static List<FileLink> UploadAndSave(this HttpPostedFile arrayFileUpload, string directory)
        {
            List<FileLink> fileLinks = new List<FileLink>();
            fileLinks = fileLinks.UploadAndSave(arrayFileUpload, directory);
            return fileLinks;
        }

        /// <summary>
        /// แปลง FileLink ให้เป็น Json
        /// </summary>
        /// <param name="listFileLinks">List ของ FileLink ที่จะทำการแปลงเป็น Json</param>
        /// <returns>สตริง Json ที่ทำการแปลงแล้ว</returns>
        public static string ToJson(this List<FileLink> listFileLinks)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(listFileLinks);
        }

        /// <summary>
        /// Remove file by fileLink
        /// </summary>
        /// <param name="removeFileLink">fileLink will be remove</param>
        /// <returns>remove stauts</returns>
        public static bool removeFileLink(this FileLink removeFileLink)
        {
            if (removeFileLink != null)
            {
                string pathDirectoryFile = HttpContext.Current.Server.MapPath(removeFileLink.Directory);
                return Git_system.App_Code.UploadFile.removeFile(pathDirectoryFile + removeFileLink.SaveFilename);
            }
            return false;
        }

        /// <summary>
        /// ทำการลบไฟล์ และ บันทึกลง Object
        /// </summary>
        /// <param name="fileLinks">ไฟล์ทั้งหมด</param>
        /// <param name="removeFileLink">ไฟล์ที่จะทำการลบ</param>
        /// <returns>ไฟล์ทั้งหมดที่ทำการลบสำเร็จ</returns>
        public static List<FileLink> RemoveAndSave(this List<FileLink> fileLinks, FileLink removeFileLink)
        {
            if (removeFileLink != null)
            {
                if (removeFileLink.removeFileLink())
                    fileLinks.Remove(removeFileLink);
            }
            return fileLinks;
        }

        /// <summary>
        /// แปลงสตริงให้เป็น List ของ FileLink
        /// </summary>
        /// <param name="fileLinksString">สตริงในรูปแบบ Json ของ List ของ FileLink </param>
        /// <returns>ค่าของ List ของ FileLink</returns>
        public static List<FileLink> ToListFileLink(this String fileLinksString)
        {
            List<FileLink> fileLinks = new List<FileLink>();
            try
            {
                fileLinks = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FileLink>>(fileLinksString);
            }
            catch
            {
                fileLinks = new List<FileLink>();
            }
            return fileLinks;
        }

        /// <summary>
        /// ทำการเพิ่มค่าของ List FileLink
        /// </summary>
        /// <param name="addToFileLinks">ค่าเริ่มต้นของ List FileList</param>
        /// <param name="fileLinks">ค่าที่ต้องการเพิ่มของ List FileList</param>
        /// <returns>ค่าของ List FileLink ที่ทำการเพิ่มแล้ว</returns>
        public static List<FileLink> Add(this List<FileLink> addToFileLinks, List<FileLink> fileLinks)
        {
            if (fileLinks != null)
                foreach (FileLink fileLink in fileLinks)
                {
                    addToFileLinks.Add(fileLink);
                }
            return addToFileLinks;
        }
    }
}