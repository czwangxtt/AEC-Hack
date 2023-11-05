using Object = AECademyHubServer.Shared.Object.Object;

namespace AECademyHubServer.Server.Services.ObjectService
{
    public interface IObjectService
    {
        Task<ServiceResponse<Object>> HandleObjectAsync(ObjectRequest request, IFormFile file);
    }
}
