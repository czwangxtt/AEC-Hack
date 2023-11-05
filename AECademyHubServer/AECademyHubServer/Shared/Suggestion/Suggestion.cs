using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AECademyHubServer.Shared.Suggestion
{
    public class Suggestion
    {
        public Guid Guid { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string PreviewUrl { get; set; } = string.Empty;
        public int DownloadNumber { get; set; }
        public string Reviews { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int DownloadCount { get; set; }
    }
}
