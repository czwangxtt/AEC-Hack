namespace AECademyHubServer.Server.Services.QueueService
{
    public interface IQueueService
    {
        Task<ServiceResponse<string>> UpdateQueueAsync(QueueRequest request);

    }
}
