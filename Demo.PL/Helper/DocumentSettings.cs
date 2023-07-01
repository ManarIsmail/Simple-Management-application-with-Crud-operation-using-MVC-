using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace Demo.PL.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1. Get Located Folder Path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName);

            // 2. Get files name and make it unique
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";

            // 3. get File Path
            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }
    }
}
