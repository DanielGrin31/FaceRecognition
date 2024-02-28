using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Refit;

namespace FaceRecognition.UI.ClassLib.Models.Requests
{
    public class ChangeGroupNameRequest
    {

        [AliasAs("old")]
        public required string OldGroupName { get; set; }
        [AliasAs("new")]
        public required string NewGroupName { get; set; }
    }
}