using Microsoft.AspNetCore.Http;
using NanoNexus.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NanoNexus.Business.Extension
{
    public static class Helper
    {
        public static string SaveFile(string rootPath, string folder, IFormFile file, string container)
        {
            if (!file.ContentType.Contains("image/"))
                throw new FileContentTypeException("Image type is wrong");

            //if (file.Length > 2097152)
            //	throw new FileSizeException("Image size must be 2mb");

            string extension = Path.GetExtension(file.FileName);
            string fileName = $"{container}-{Guid.NewGuid().ToString().ToLower()}{extension}";

            string path = rootPath + @$"\{folder}\" + fileName;

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        public static void DeleteFile(string rootPath, string folder, string fileName)
        {
            string path = rootPath + @$"\{folder}\" + fileName;

            if (!File.Exists(path))
                throw new EntityFileNotFoundException($"File does not exist in this {path}");

            File.Delete(path);
        }


    }
}
