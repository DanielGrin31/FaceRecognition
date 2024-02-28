using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.UI.ClassLib.Models.Responses
{
    public class AlignFacesResponse
    {
        public string[]? errors { get; set; }
        public int[]? faces_length { get; set; }
        public string[]? images { get; set; }
        public string[]? messages { get; set; }

    }
}