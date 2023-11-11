using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.Services;

namespace FaceRecognition.UI.Console.Services
{
    public class OptionHandler : IOptionHandler
    {
        private readonly IImageRepositoryService imageRepository;

        public OptionHandler(IImageRepositoryService imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public async Task HandleOption(int choice)
        {
            switch (choice)
            {
                case 1:
                    System.Console.WriteLine("Uploading test.png...");
                    await Upload();
                    break;
                case 8:
                    System.Console.WriteLine("Resetting Embeddings...");
                    await Reset();
                    break;
                case 9:
                    System.Console.WriteLine("Exiting...");
                    Environment.Exit(0);
                    break;
                default:
                    System.Console.WriteLine("Invalid option");
                    break;
            }
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