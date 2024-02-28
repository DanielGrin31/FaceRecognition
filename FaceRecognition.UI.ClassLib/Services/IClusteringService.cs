using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.Models.Requests;
using FaceRecognition.UI.ClassLib.Models.Responses;

namespace FaceRecognition.UI.ClassLib.Services
{
    public interface IClusteringService
    {
        Task<Dictionary<string,string[]>> ClusterFaces(float maxDistance=0.4f, int minSamples=4);
        Task<Dictionary<string,string[]>> GetGroups(float maxDistance=0.4f, int minSamples=4);
        Task<bool> ChangeGroupName(string oldName,string newName);
    }
}