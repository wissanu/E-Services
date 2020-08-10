using System.IO;
using System.Web;

namespace Git_system.App_Code
{
    public class UploadFile
    {
        /// <summary>
        /// ตรวจสอบไฟล์ว่ามีหรือไม่
        /// </summary>
        /// <param name="file">ไฟล์ที่จะทำการ upload</param>
        /// <returns>สถานะ</returns>
        public static bool checkFileNull(HttpPostedFileBase file)
        {
            if (file != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ตรวจสอบไฟล์ว่ามีหรือไม่
        /// </summary>
        /// <param name="file">ไฟล์ที่จะทำการ upload</param>
        /// <returns>สถานะ</returns>
        public static bool checkFileNull(HttpPostedFile file)
        {
            if (file != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// ทำการอัพโหลดไฟล์ และเปลี่ยนชื่อให้อัตโนมัน
        /// </summary>
        /// <param name="file">ไฟล์ที่จะทำการ upload</param>
        /// <param name="path">ที่อยู่ที่ต้องการเก็บ</param>
        /// <returns>สถานะการอัพโหลด และชื่อไฟล์</returns>
        public static UploadFileStatus uploadFile(HttpPostedFileBase file, string path)
        {
            string filename = new Git_system.Models.ECom.RandomId().sId + Git_system.App_Code.Cryptography.SHA1(file.FileName).Substring(20);
            UploadFileStatus uploadfile = new UploadFile().uploadFile(file, path, filename);
            return uploadfile;
        }

        /// <summary>
        /// ทำการอัพโหลดไฟล์ และเปลี่ยนชื่อให้อัตโนมัน
        /// </summary>
        /// <param name="file">ไฟล์ที่จะทำการ upload</param>
        /// <param name="path">ที่อยู่ที่ต้องการเก็บ</param>
        /// <returns>สถานะการอัพโหลด และชื่อไฟล์</returns>
        public static UploadFileStatus uploadFile(HttpPostedFile file, string path)
        {
            string filename = new Git_system.Models.ECom.RandomId().sId + Git_system.App_Code.Cryptography.SHA1(file.FileName).Substring(20);
            UploadFileStatus uploadfile = new UploadFile().uploadFile(file, path, filename);
            return uploadfile;
        }

        /// <summary>
        /// ทำการอัพโหลดไฟล์ และทำการเปลี่ยนชื่อตามที่กำนหด
        /// </summary>
        /// <param name="saveingFile">ไฟล์ที่จะทำการ upload</param>
        /// <param name="path">ที่อยู่ที่ต้องการเก็บ</param>
        /// <param name="saveToFilename">ชื่อที่ต้องการเก็บ</param>
        /// <returns>สถานะการอัพโหลด และชื่อไฟล์</returns>
        private UploadFileStatus uploadFile(HttpPostedFileBase saveingFile, string path, string saveToFilename)
        {
            var fileName = Path.GetFileName(saveingFile.FileName);
            string filename = saveToFilename + Path.GetExtension(fileName);
            string pathAndFileName = Path.Combine(path, filename);
            try
            {
                saveingFile.SaveAs(pathAndFileName);
                return new UploadFileStatus(filename);
            }
            catch
            {
                removeFile(pathAndFileName);
                return new UploadFileStatus();
            }
        }

        /// <summary>
        /// ทำการอัพโหลดไฟล์ และทำการเปลี่ยนชื่อตามที่กำนหด
        /// </summary>
        /// <param name="saveingFile">ไฟล์ที่จะทำการ upload</param>
        /// <param name="path">ที่อยู่ที่ต้องการเก็บ</param>
        /// <param name="saveToFilename">ชื่อที่ต้องการเก็บ</param>
        /// <returns>สถานะการอัพโหลด และชื่อไฟล์</returns>
        private UploadFileStatus uploadFile(HttpPostedFile saveingFile, string path, string saveToFilename)
        {
            var fileName = Path.GetFileName(saveingFile.FileName);
            string filename = saveToFilename + Path.GetExtension(fileName);
            string pathAndFileName = Path.Combine(path, filename);
            try
            {
                saveingFile.SaveAs(pathAndFileName);
                return new UploadFileStatus(filename);
            }
            catch
            {
                removeFile(pathAndFileName);
                return new UploadFileStatus();
            }
        }

        public static bool removeFile(string pathAndFileName)
        {
            FileInfo fileInfo = new FileInfo(pathAndFileName);
            fileInfo.Delete();
            return true;
        }
    }

    public class UploadFileStatus
    {
        public UploadFileStatus()
        {
            this.status = false;
            this.filename = null;
        }

        public UploadFileStatus(string filename)
        {
            this.status = true;
            this.filename = filename;
        }

        public bool status { get; set; }

        public string filename { get; set; }
    }
}