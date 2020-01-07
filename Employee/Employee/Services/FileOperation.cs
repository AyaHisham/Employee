using System;
using System.IO;
using System.Web;

namespace Employee.Services
{
    public static class FileOperation
    {
        public static void SaveFile(HttpPostedFileBase file, string folderName, out string NameForDataBase)
        {
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(folderName)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(folderName));
            }
            string randomNumber = Guid.NewGuid().ToString().Substring(0, 10);
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            if (fileName.Length > 30)
            {
                fileName = fileName.Substring(0, 30);
            }
            var fileExtension = Path.GetExtension(file.FileName);
            var Serverpath = Path.Combine(HttpContext.Current.Server.MapPath(folderName)+ randomNumber + fileName + fileExtension);
            file.SaveAs(Serverpath);
            NameForDataBase = randomNumber + fileName + fileExtension;
        }
        public static void DeleteFile(string fullPath)
        {
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
    }
}
