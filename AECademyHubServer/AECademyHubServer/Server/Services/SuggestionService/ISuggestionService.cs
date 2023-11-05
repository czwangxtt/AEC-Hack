namespace AECademyHubServer.Server.Services.SuggestionService
{
    public interface ISuggestionService
    {
        Task<List<Suggestion>> HandleSuggestionAsync(SuggestionRequest request);
    }
}
