using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.UI.ClassLib.Models.Responses
{
    public class FindFacesResponse
    {
        public List<int[]>? boxes { get; set; }
        public string[]? errors { get; set; }
        public int faces_length { get; set; }
        public string[]? messages { get; set; }
    }
}