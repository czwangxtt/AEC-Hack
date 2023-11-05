using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AECademyHubServer.Shared.Object
{
    public class Object
    {
        [Key]
        public Guid Guid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string PreviewUrl { get; set; } = string.Empty;
        public int DownloadNumber { get; set; } = 0;
        public string PermissionLevel { get; set; } = string.Empty;
        public Guid AuthorGuid { get; set; }
        public string Reviews { get; set; } = string.Empty;

        public Object() { }

        public Object(ObjectRequest request)
        {
            Guid = request.Guid;
            Name = request.Name;
            Type = request.Type;
            Description = request.Description;
            PermissionLevel = request.PermissionLevel;
            AuthorGuid = request.AuthorGuid;
            Reviews = request.Reviews;
        } 
    }
}
