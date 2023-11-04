namespace AECademyHubServer.Server.Services.SuggestionService
{
    public interface ISuggestionService
    {
        Task<ServiceResponse<List<Suggestion>>> HandleSuggestionAsync(SuggestionRequest request);
    }
}
