using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.UI.ClassLib.Models.Responses
{
    public class CompareFacesResponse
    {
        public float similarity { get; set; }
        public required string[] messages { get; set; }
        public required string[] errors { get; set; }
    }
}