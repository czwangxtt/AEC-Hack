using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AECademyHubServer.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionController : ControllerBase
    {

        private readonly ISuggestionService _suggestionService;

        public SuggestionController(ISuggestionService SuggestionService)
        {
            _suggestionService = SuggestionService;
        }

        [HttpPost]
        [Route("GetSuggestion")]
        public async Task<ServiceResponse<List<Suggestion>>> GetSuggestion(SuggestionRequest suggestion)
        {
          
            // Process the image as required, for example storing to the temp folder
            var suggestionList = await _suggestionService.HandleSuggestionAsync(suggestion);
            var response = new ServiceResponse<List<Suggestion>> ()
            {
                Data = suggestionList,
                Success = true,
            };

            // You might want to return the path or some confirmation
            return response;
        }

        //[HttpPost, Authorize(Roles = "Admin")]
        //public async Task<ActionResult<ServiceResponse<List<Suggestion>>>> PutImage(SuggestionRequest request)
        //{
        //    var result = await _SuggestionService.GetSuggestionAsync(request);
        //    return Ok(result);
        //}

    }
}
