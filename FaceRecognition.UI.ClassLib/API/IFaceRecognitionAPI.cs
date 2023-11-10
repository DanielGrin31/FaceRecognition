using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}