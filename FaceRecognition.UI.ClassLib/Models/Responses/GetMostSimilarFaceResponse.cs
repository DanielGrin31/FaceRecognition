using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.UI.ClassLib.Models.Responses
{
    public class GetMostSimilarFaceResponse
    {
        public string[] errors { get; set; }
        public int face { get; set; }
        public int face_length { get; set; }
        public string image { get; set; }
        public string[] messages { get; set; }

    }
}