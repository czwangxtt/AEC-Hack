using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AECademyHubServer.Shared.Suggestion
{
    public class SuggestionRequest
    {
        public string prompt { get; set; }
        public string Base64ImageData { get;set; }
    }
}
