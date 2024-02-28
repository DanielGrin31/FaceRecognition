using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.API;
using FaceRecognition.UI.ClassLib.Models.Responses;
using Microsoft.Extensions.Logging;
using Refit;

namespace FaceRecognition.UI.ClassLib.Services
{
    public class ImageRepositoryService : IImageRepositoryService
    {
        private readonly IFaceRecognitionAPI api;
        private readonly ILogger<ImageRepositoryService> _logger;

        public ImageRepositoryService(IFaceRecognitionAPI api, ILogger<ImageRepositoryService> logger)
        {
            this.api = api;
            _logger = logger;
        }

        public async Task<int> FilterImages(float threshold = 0.96f)
        {
            var response = await api.FilterImages(threshold);
            return response.deleted ?? -1;
        }

        public async Task<bool> Reset()
        {
            try
            {
                var response = await api.ResetEmbeddings();
                return response?.Result?.ToLower().Trim() == "success";
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while restting embeddings:\n{e.Message}");
                return false;
            }
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
            try
            {
                UploadImagesResponse response = await api.UploadStream(new StreamPart(file, filename));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}