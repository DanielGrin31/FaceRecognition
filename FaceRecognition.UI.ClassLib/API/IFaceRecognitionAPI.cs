using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.Models.Requests;
using FaceRecognition.UI.ClassLib.Models.Responses;
using Refit;

namespace FaceRecognition.UI.ClassLib.API
{
    public interface IFaceRecognitionAPI
    {
        [Post("/upload")]
        [Multipart]
        Task<UploadImagesResponse> Upload([AliasAs("image1")] FileInfoPart fileInfo);

        [Post("/upload")]
        [Multipart]
        Task<UploadImagesResponse> UploadStream([AliasAs("image1")] StreamPart stream);
        [Get("/ping")]
        Task<string> Ping();
        [Delete("/delete")]
        Task<DeleteEmbeddingsResponse> ResetEmbeddings();

        [Multipart]
        [Post("/align")]
        Task<AlignFacesResponse> AlignImages([AliasAs("images")] string[] files);
        [Post("/detect")]
        [Multipart]
        Task<AlignFacesResponse> DetectFaces([AliasAs("images")] string[] files);
        [Post("/compare")]
        [Multipart]
        Task<CompareFacesResponse> CompareFaces([AliasAs("images")] string[] files, [AliasAs("selected_faces")] string[] faceSelections);
        [Multipart]
        [Post("/find")]
        Task<FindFacesResponse> GetFaceLocations([AliasAs("image")] string filename);
        [Multipart]
        [Post("/check")]
        Task<GetMostSimilarFaceResponse> GetMostSimilarFace([AliasAs("image")] string filename, [AliasAs("selected_face")] int selectedFace);
        [Multipart]
        [Post("/filter")]
        Task<FilterImagesResponse> FilterImages(float threshold);
        [Post("/cluster")]
        Task<Dictionary<string, string[]>> ClusterFaces(ClusterFacesRequest request);
        [Post("/change_group_name")]
        Task<ChangeGroupNameResponse> ChangeGroupName(ChangeGroupNameRequest request);
        [Post("/change_group_name")]
        Task<GetKSimilarImagesResponse> GetKSimilarImages(GetKSimilarImagesRequest request);
    }
}