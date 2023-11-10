using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.API;
using FaceRecognition.UI.ClassLib.Models.Responses;
using Refit;

namespace FaceRecognition.UI.ClassLib.Services
{
    public class ImageUploadService
    {
        private readonly IFaceRecognitionAPI api;

        public ImageUploadService(IFaceRecognitionAPI api)
        {
            this.api = api;
        }

        public async Task<bool> UploadImage(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            var fileinfo = new FileInfo(path);
            Path.GetFileName(path);
            UploadImagesResponse response = await api.Upload(new FileInfoPart(fileinfo,));
            return true;
        }
        public Task UploadImage(Stream file)
        {

        }
    }
}