using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace FaceRecognition.UI.ClassLib.Models.Requests
{
    public class GetKSimilarImagesRequest
    {
        [AliasAs("image")]
        public required FileInfoPart Image { get; set; }
        [AliasAs("number_of_images")]
        public int NumberOfImages { get; set; }
    }
}