using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AECademyHubServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService _QueueService;

        public QueueController(IQueueService QueueService)
        {
            _QueueService = QueueService;
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<string>>> Queue(QueueRequest request)
        {
            var result = await _QueueService.UpdateQueueAsync(request);
            return Ok(result);
        }
    }
}
