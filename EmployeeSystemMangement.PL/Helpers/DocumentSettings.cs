using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace EmployeeSystemMangement.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            //1 Get located Folder Path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",folderName);
            if (!File.Exists(folderPath))
            {   
                Directory.CreateDirectory(folderPath);
            }

            //2 Get FileName And make it unique
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            //3 Get the file path (FolderPath + FileName)
            var filePath= Path.Combine(folderPath, fileName);

            //4 Save file As Streams(Data Per time)
           using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return fileName;
        }
        public static void DeleteFile(string fileName,string folderName)
        {
            var filePath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",folderName,fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }


        }
    }
}
