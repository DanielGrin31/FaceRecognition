using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.UI.ClassLib.Models.Responses
{
    public class FilterImagesResponse
    {
        public bool success { get; set; }
        public int? deleted { get; set; }
        public string? error { get; set; }
    }
}