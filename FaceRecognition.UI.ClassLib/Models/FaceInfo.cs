namespace FaceRecognition.UI.ClassLib.Models.Responses;

public record FaceInfo(int[,] Landmarks,int[] BBox);