namespace FaceRecognition.UI.ClassLib.Models.EventArgs;

public class DetectorChangedEventArgs:System.EventArgs
{
    public DetectorChangedEventArgs(DetectorName old, DetectorName @new)
    {
        Old = old;
        New = @new;
    }

    public DetectorName Old { get; set; }
    public DetectorName New { get; set; }
}