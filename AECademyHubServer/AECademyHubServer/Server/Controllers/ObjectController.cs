using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Object = AECademyHubServer.Shared.Object.Object;

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

        [HttpPost("Upload")]
        [SwaggerOperation(Summary = "Uploads a new object and file")]

        public async Task<ServiceResponse<Object>> PostObject(string objectRequestJson, IFormFile file)
        {

            try
            {
                var objectRequest = JsonConvert.DeserializeObject<ObjectRequest>(objectRequestJson);
                if (objectRequest == null)
                {
                    return new ServiceResponse<Object>()
                    {
                        Success = false,
                        Message = "Object data is invalid."
                    };
                }

                var response = await _objectService.HandleObjectAsync(objectRequest, file);

                // You might want to return the path or some confirmation
                return response;
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON deserialization errors
                return new ServiceResponse<Object>()
                {
                    Success = false,
                    Message = $"Invalid JSON format: {jsonEx.Message}"
                };
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return new ServiceResponse<Object>()
                {
                    Success = false,
                    Message = $"Unexpected error: {ex.Message}"
                };
            }
            
        }

        //[HttpPost("UploadTest")]

        //public async Task<ServiceResponse<ObjectRequest>> PostObjectTest(string objectRequest, IFormFile file)
        //{
        //    //var objects = await _objectService.HandleObjectAsync(objectRequest, file);
        //    var response = new ServiceResponse<ObjectRequest>()
        //    {
        //        Data = new ObjectRequest(),
        //        Success = true,
        //    };
        //    // You might want to return the path or some confirmation
        //    return response;
        //}

    }
}
