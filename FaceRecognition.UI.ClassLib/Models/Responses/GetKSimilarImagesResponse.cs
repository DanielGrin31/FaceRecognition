using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.UI.ClassLib.Models.Responses
{
    public class GetKSimilarImagesResponse
    {
        public string[] images { get; set; }
        public string[] errors { get; set; }
    }
}