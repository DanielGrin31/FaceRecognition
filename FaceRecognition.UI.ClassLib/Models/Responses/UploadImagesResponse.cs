using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.UI.ClassLib.Models.Responses
{
    public class UploadImagesResponse
    {
        public List<object> errors { get; set; }=new();
        public List<int> faces_length { get; set; }=new();
        public List<string> images { get; set; }=new();
    }

}