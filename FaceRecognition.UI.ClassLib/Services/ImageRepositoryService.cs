using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.API;
using FaceRecognition.UI.ClassLib.Models.Responses;
using Refit;

namespace FaceRecognition.UI.ClassLib.Services
{
    public class ImageRepositoryService : IImageRepositoryService
    {
        private readonly IFaceRecognitionAPI api;

        public ImageRepositoryService(IFaceRecognitionAPI api)
        {
            this.api = api;
        }

        public async Task<bool> Reset()
        {
            var response = await api.ResetEmbeddings();
            return response?.Result?.ToLower().Trim() == "success";
        }

        public async Task<bool> UploadImage(string path)
        {
            if (!File.Exists(path))
            {
                return false;
            }
            var fileinfo = new FileInfo(path);
            var filename = Path.GetFileName(path);
            UploadImagesResponse response = await api.Upload(new FileInfoPart(fileinfo, filename));
            return true;
        }
        public async Task<bool> UploadImage(Stream file, string filename)
        {
            UploadImagesResponse response = await api.UploadStream(new StreamPart(file, filename));
            return true;
        }
    }
}