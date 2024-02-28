using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.API;
using FaceRecognition.UI.ClassLib.Models.Requests;
using FaceRecognition.UI.ClassLib.Models.Responses;
using Microsoft.Extensions.Logging;

namespace FaceRecognition.UI.ClassLib.Services
{
    public class ClusteringService : IClusteringService
    {
        private readonly ILogger<ClusteringService> _logger;
        private readonly IFaceRecognitionAPI _api;
        public ClusteringService(ILogger<ClusteringService> logger, IFaceRecognitionAPI api)
        {
            this._api = api;
            _logger = logger;

        }

        public async Task<bool> ChangeGroupName(string oldName, string newName)
        {
            var request=new ChangeGroupNameRequest()
            {
                NewGroupName = newName,
                OldGroupName = oldName
            };
           var response= await _api.ChangeGroupName(request);
            return response.success;
        }

        public async Task<Dictionary<string, string[]>> ClusterFaces(float maxDistance = 0.4F, int minSamples = 4)
        {
            var request = new ClusterFacesRequest()
            {
                MaxDistance = maxDistance,
                MinSamples = minSamples,
                Retrain = true
            };
            var response = await _api.ClusterFaces(request);
            return response;
        }

        public async Task<Dictionary<string, string[]>> GetGroups(float maxDistance = 0.4F, int minSamples = 4)
        {
            var request = new ClusterFacesRequest()
            {
                MaxDistance = maxDistance,
                MinSamples = minSamples,
                Retrain = false
            };
            var response = await _api.ClusterFaces(request);
            return response;
        }
    }
}