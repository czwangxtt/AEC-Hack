using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AECademyHubServer.Server.Services.SuggestionService
{
    public class SuggestionService : ISuggestionService
    {
        public Task<ServiceResponse<List<Suggestion>>> HandleSuggestionAsync(SuggestionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
