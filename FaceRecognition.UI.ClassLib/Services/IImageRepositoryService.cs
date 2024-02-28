namespace FaceRecognition.UI.ClassLib.Services
{
    public interface IImageRepositoryService
    {
        Task<bool> UploadImage(string path);
        Task<bool> UploadImage(Stream file, string filename);
        Task<bool> Reset();
        Task<int> FilterImages(float threshold=0.96f);

    }
}