using Microsoft.AspNetCore.Mvc;

namespace AECademyHubServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectController : ControllerBase
    {
        private readonly IObjectService _objectService;

        public ObjectController(IObjectService ObjectService)
        {
            _objectService = ObjectService;
        }

        [HttpPost]
        [Route("PostObject")]
        public async Task<ServiceResponse<ObjectRequest>> PostObject(ObjectRequest objectRequest, IFormFile file)
        {
            var objects = await _objectService.HandleObjectAsync(objectRequest, file);
            var response = new ServiceResponse<ObjectRequest>()
            {
                Data = new ObjectRequest(),
                Success = true,
            };
            // You might want to return the path or some confirmation
            return response;
        }

    }
}
