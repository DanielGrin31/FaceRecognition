using FaceRecognition.UI.ClassLib.API;
using FaceRecognition.UI.ClassLib.Services;
using FaceRecognition.UI.Console.Services;
using Refit;

public class Application
{
    private readonly IOptionHandler handler;

    public Application(IOptionHandler handler)
    {
        this.handler = handler;
    }
    public async Task StartAsync()
    {
        int choice = 0;
        while (true)
        {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Upload test.png");
            Console.WriteLine("2. Align faces in test.png");
            Console.WriteLine("3. Detect faces in test.png");
            Console.WriteLine("4. Compare faces in test.png and 405ea36d.png");
            Console.WriteLine("5. Find face locations in test.png");
            Console.WriteLine("6. Get most similar image to test.png");
            Console.WriteLine("7. Filter images inside database");
            Console.WriteLine("8. Reset Embeddings");
            Console.WriteLine("99. Exit");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out choice))
            {
                await handler.HandleOption(choice);
            }
            else
            {
                Console.WriteLine("Invalid input,try again");
            }
        }

    }
}