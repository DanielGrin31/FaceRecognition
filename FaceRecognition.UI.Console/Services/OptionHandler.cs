using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.Services;
using Microsoft.Extensions.Logging;

namespace FaceRecognition.UI.Console.Services
{
    public class OptionHandler : IOptionHandler
    {
        private readonly IImageRepositoryService imageRepository;
        private readonly IImageProcessingService processing;
        private readonly IClusteringService clustering;
        private readonly ILogger<OptionHandler> _logger;
        public OptionHandler(IImageRepositoryService imageRepository,
         IImageProcessingService processing,IClusteringService clustering, ILogger<OptionHandler> logger)
        {
            this.imageRepository = imageRepository;
            this.processing = processing;
            this.clustering = clustering;
            _logger = logger;
        }
        public async Task HandleOption(int choice)
        {
            switch (choice)
            {
                case 1:
                    _logger.LogInformation("Uploading test.png...");
                    await Upload();
                    break;
                case 2:
                    _logger.LogInformation("Aligning images");
                    await Align();
                    break;
                case 3:
                    _logger.LogInformation("Detecting faces");
                    await Detect();
                    break;
                case 4:
                    _logger.LogInformation("Comparing test.png and 405ea36d.png...");
                    await Compare();
                    break;
                case 5:
                    _logger.LogInformation("Get face locations in image...");
                    await Find();
                    break;
                case 6:
                    _logger.LogInformation("finding most similar face to test.png...");
                    await FindMostSimilar();
                    break;
                case 7:
                    _logger.LogInformation("filtering images in database...");
                    await Filter();
                    break;
                case 8:
                    _logger.LogInformation("Resetting Embeddings...");
                    await Reset();
                    break;
                case 9:
                    _logger.LogInformation("Clustering images...");
                    await Cluster();
                    break;
                case 99:
                    _logger.LogInformation("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    _logger.LogError("Invalid option");
                    break;
            }
        }

        private async Task Cluster()
        {
            var result=await clustering.ClusterFaces();

            result.Remove("-1");
            _logger.LogInformation("clusters: {@clusters}",result);
        }

        private async Task Filter()
        {
            var result = await imageRepository.FilterImages();
            _logger.LogInformation("{@deleted} images were filtered out", result);

        }

        private async Task FindMostSimilar()
        {
            string filename1 = "test.png";
            var result = await processing.GetMostSimilarImage(filename1, 0);
            _logger.LogInformation("The most similar image is {@image}", result);
        }

        private async Task Find()
        {
            string filename1 = "test.png";
            var result = await processing.GetFaceLocations(filename1);
            _logger.LogInformation("Boxes: {@boxes}", result);
        }

        private async Task Compare()
        {
            string filename1 = "test.png";
            string filename2 = "405ea36d.png";
            var result = await processing.Compare(filename1, filename2, 0, 0);

        }

        private async Task Detect()
        {
            string filename = "test.png";
            int faces = await processing.DetectFaces(filename);
            _logger.LogInformation("Number of faces is: {@faces}", faces);
        }

        private async Task Align()
        {
            string filename = "test.png";
            int faces = await processing.GenerateAlignedFaces(filename);
            _logger.LogInformation("Number of faces is: {@faces}", faces);
        }

        private async Task Reset()
        {
            await imageRepository.Reset();
        }

        private async Task Upload()
        {
            string filename = "test.png";
            string imagePath = $"./Data/{filename}";
            await imageRepository.UploadImage(imagePath);
        }
    }
}