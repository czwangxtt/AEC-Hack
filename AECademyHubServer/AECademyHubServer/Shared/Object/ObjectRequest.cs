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
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PermissionLevel { get; set; } = string.Empty;
        public Guid AuthorGuid { get; set; }
        public string Reviews { get; set; } = string.Empty;

    }



}
