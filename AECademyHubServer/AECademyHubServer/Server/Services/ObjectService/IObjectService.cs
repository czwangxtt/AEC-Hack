namespace AECademyHubServer.Server.Services.ObjectService
{
    public interface IObjectService
    {
        Task<ServiceResponse<ObjectRequest>> HandleObjectAsync(ObjectRequest request);
    }
}
