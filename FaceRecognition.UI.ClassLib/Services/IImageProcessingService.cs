using FaceRecognition.UI.ClassLib.Models;
using Refit;

namespace FaceRecognition.UI.ClassLib.Services
{
    public interface IImageProcessingService
    {
        Task<int> GenerateAlignedFaces(string filename);
        Task<int> GenerateAlignedFaces(string[] files);
        Task<int> DetectFaces(string filename);
        Task<int> DetectFaces(string[] files);
        Task<float> Compare(string first, string second, int firstFace = -2, int secondFace = -2);
        Task<IEnumerable<BBox?>> GetFaceLocations(string filename);
        Task<string> GetMostSimilarImage(string filename,int faceNum);
        Task<string[]> GetKSimilarImages(string path,int k=5);
    }
}