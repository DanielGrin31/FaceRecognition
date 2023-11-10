using FaceRecognition.UI.ClassLib.API;
using Refit;

public class Application
{
    private readonly IFaceRecognitionAPI api;

    public Application(IFaceRecognitionAPI api)
    {
        this.api = api;
    }
    public async Task StartAsync()
    {
        string filename="test.png";
        string imagePath = $"./Data/{filename}"; 
        FileInfo info=new FileInfo(imagePath);
       var response= await api.Upload(new FileInfoPart(info,"test1.png"));
        System.Console.WriteLine("test");
        // Console.WriteLine(await api.Ping());
    }
}