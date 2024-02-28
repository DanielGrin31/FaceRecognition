using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.API;
using FaceRecognition.UI.ClassLib.Models;
using FaceRecognition.UI.ClassLib.Models.Responses;
using Microsoft.Extensions.Logging;
using Refit;

namespace FaceRecognition.UI.ClassLib.Services
{
    public class ImageProcessingService : IImageProcessingService
    {
        private readonly IFaceRecognitionAPI api;
        private readonly ILogger<ImageProcessingService> _logger;
        public ImageProcessingService(IFaceRecognitionAPI api, ILogger<ImageProcessingService> logger)
        {
            _logger = logger;
            this.api = api;
        }

        public async Task<float> Compare(string first, string second, int firstFace = -2, int secondFace = -2)
        {
            string[] files = new string[] { first, second };
            int[] faceSelections = new int[] { firstFace, secondFace };
            var response = await api.CompareFaces(files, faceSelections.Select(x => x.ToString()).ToArray());
            _logger.LogInformation("{@messages}", response.messages);

            if (response.errors.Length > 0)
            {
                _logger.LogError("{@errors}", response.errors);
                return -1;
            }
            return response.similarity;
        }

        public Task<int> DetectFaces(string filename)
        {

            string[] files = new string[] { filename };
            return DetectFaces(files);
        }
        public async Task<int> DetectFaces(string[] files)
        {

            var response = await api.DetectFaces(files);
            if (response.faces_length is not null)
            {
                return response.faces_length.Sum();
            }
            if (response.errors?.Length > 0)
            {
                _logger.LogError("Errors while aligning images: {@Errors}", response.errors);
            }
            return -1;
        }
        public Task<int> GenerateAlignedFaces(string filename)
        {
            string[] files = new string[] { filename, "405ea36d.png" };
            return GenerateAlignedFaces(files);
        }
        public async Task<int> GenerateAlignedFaces(string[] files)
        {
            var response = await api.AlignImages(files);
            if (response.faces_length is not null)
            {
                return response.faces_length.Sum();
            }
            if (response.errors?.Length > 0)
            {
                _logger.LogError("Errors while aligning images: {@Errors}", response.errors);
            }
            return -1;
        }

        public async Task<IEnumerable<BBox?>> GetFaceLocations(string filename)
        {
            var response = await api.GetFaceLocations(filename);
            return response.boxes!.Select(x => BBox.FromArray(x));
        }

        public async Task<string[]> GetKSimilarImages(string path, int k = 5)
        {
            if (!File.Exists(path))
            {
                return new string[0];
            }
            var fileinfo = new FileInfo(path);
            var filename = Path.GetFileName(path);

            var fileInfoPart = new FileInfoPart(fileinfo, filename);
            var response = await api.GetKSimilarImages(new()
            {
                Image=fileInfoPart,
                NumberOfImages=k+1
            });
            return response.images;
        }

        public async Task<string> GetMostSimilarImage(string filename, int faceNum)
        {
            var response = await api.GetMostSimilarFace(filename, faceNum);
            return response.image;
        }
    }
}