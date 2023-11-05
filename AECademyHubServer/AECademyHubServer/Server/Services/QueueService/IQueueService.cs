namespace AECademyHubServer.Server.Services.QueueService
{
    public interface IQueueService
    {
        Task<ServiceResponse<string>> UpdateQueueAsync(QueueRequest request);
        Task<ServiceResponse<List<Suggestion>>> GetSuggestionFromQueueAsync(string userGuid);
    }
}
