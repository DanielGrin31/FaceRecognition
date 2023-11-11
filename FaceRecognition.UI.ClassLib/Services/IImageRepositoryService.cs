namespace FaceRecognition.UI.ClassLib.Services
{
    public interface IImageRepositoryService
    {
        Task<bool> UploadImage(string path);
        Task<bool> UploadImage(Stream file, string filename);
        Task<bool> Reset();
    }
}