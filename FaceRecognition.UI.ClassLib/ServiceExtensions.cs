using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognition.UI.ClassLib.API;
using FaceRecognition.UI.ClassLib.Models;
using FaceRecognition.UI.ClassLib.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace FaceRecognition.UI.ClassLib
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterAPI(this IServiceCollection services, ApiSettings settings)
        {
            services.AddRefitClient<IFaceRecognitionAPI>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(settings.URL + "/api"));
            services.AddScoped<IImageRepositoryService, ImageRepositoryService>();
            return services;
        }
    }
}