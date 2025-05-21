using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MoviesApp.Core.Services;
using MoviesApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Repositories.Service
{
    public class ImageService : IImageService
    {
        private readonly IFileProvider _fileProvider;
        public ImageService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public async Task<string> AddMedia(IFormFile file , string mediaType)
        {
            string folderName = mediaType.ToLower() == "video" ? "Videos" : "Images";
            string ImageDirectory = Path.Combine("wwwroot", "Movies",folderName);
            if(!Directory.Exists(ImageDirectory))
                Directory.CreateDirectory(ImageDirectory);

            string filePath = Path.Combine(ImageDirectory, file.FileName);
            using (var filestream = new FileStream(filePath, FileMode.Create))
            {
              await file.CopyToAsync(filestream);
            };

            return $"/Movies/{folderName}/" + file.FileName;
        }

        public void DeleteMedia(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);  
        }
    }
}
    