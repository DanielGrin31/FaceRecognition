using FaceRecognition.UI.ClassLib.Models;

namespace FaceRecognition.UI.ClassLib.Services;

class ImageMetadataService : IImageMetadataService
{
    public async Task<IEnumerable<BBox?>> GetFaceLocations(string filename,ModelsConfig models)
    {
        var response = await api.GetFaceLocations(filename);
        return response.boxes!.Select(x => BBox.FromArray(x));
    }
}