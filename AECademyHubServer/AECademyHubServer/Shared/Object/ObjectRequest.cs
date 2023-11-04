using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AECademyHubServer.Shared.Object
{
    public class ObjectRequest
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? PreviewUrl { get; set; }
        public int DownloadNumber { get; set; }
        public string? PermissionLevel { get; set; }
        public Guid AuthorGuid { get; set; }
        public string? Reviews { get; set; }
        
    }



}
