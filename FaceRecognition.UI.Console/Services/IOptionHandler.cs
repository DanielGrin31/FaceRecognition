using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceRecognition.UI.Console.Services
{
    public interface IOptionHandler
    {

        Task HandleOption(int choice);
    }
}