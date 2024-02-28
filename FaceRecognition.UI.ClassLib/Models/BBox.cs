using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.UI.ClassLib.Models
{
    public record BBox(int X1, int X2, int Y1, int Y2)
    {
        public static BBox? FromArray(int[] arr)
        {
            if (arr.Length == 4)
            {
                BBox box = new(arr[0],arr[1],arr[2],arr[3]);
                return box;
            }
            return null;
        }
    }
}