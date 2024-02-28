using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace FaceRecognition.UI.ClassLib.Models.Requests
{
    public class ClusterFacesRequest
    {
        [AliasAs("max_distance")]
        public float MaxDistance { get; set; }
        [AliasAs("min_samples")]
        public int MinSamples { get; set; }
        [AliasAs("retrain")]
        public bool Retrain { get; set; }
    }
}