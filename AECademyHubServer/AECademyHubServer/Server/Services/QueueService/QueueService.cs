using Azure.Core;
using Newtonsoft.Json;

namespace AECademyHubServer.Server.Services.QueueService
{
    public class QueueService : IQueueService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public QueueService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<List<Suggestion>>> GetSuggestionFromQueueAsync(string userGuid)
        {
            // Parse userGuid to Guid
            if (!Guid.TryParse(userGuid, out Guid parsedUserGuid))
            {
                return new ServiceResponse<List<Suggestion>>
                {
                    Success = false,
                    Message = "Invalid user GUID format."
                };
            }

            // Get all QueueRequests for the user
            var queueEntries = await _context.Queue
                .Where(q => q.UserGuid == parsedUserGuid)
                .ToListAsync();

            // Extract all unique objectIds from those queue entries
            var objectIds = queueEntries.Select(q => q.ObjectGuid).Distinct();

            // Get all Suggestions for the objectIds
            var objects = await _context.Objects
                .Where(o => objectIds.Contains(o.Guid))
                .ToListAsync();

            if (objects.Count == 0)
            {
                return new ServiceResponse<List<Suggestion>>
                {
                    Success = false,
                    Message = "No suggestions found for the given user GUID."
                };
            }

            List<Suggestion> suggestions = new List<Suggestion>();
            foreach (var obj in objects)
            {
                var suggestion = new Suggestion()
                {
                    Guid = obj.Guid,
                    Description = obj.Description,
                    Type = obj.Type,
                    Url = obj.Url,
                    PreviewUrl = obj.PreviewUrl,
                    DownloadNumber = obj.DownloadNumber,
                    Reviews = obj.Reviews,
                    Author = obj.AuthorGuid.ToString(),
                    DownloadCount = obj.DownloadNumber
                };
                suggestions.Add(suggestion);
            }

            // Remove the queue entries
            _context.Queue.RemoveRange(queueEntries);
            await _context.SaveChangesAsync();

            // Return the suggestions in a service response
            return new ServiceResponse<List<Suggestion>>
            {
                Data = suggestions,
                Success = true,
                Message = "Retrieved suggestions successfully."
            };


        }


        public async Task<ServiceResponse<string>> UpdateQueueAsync(QueueRequest request)
        {
            // Find an existing request with the same objectGuid and userGuid
            var existingRequest = await _context.Queue
                .FirstOrDefaultAsync(r => r.ObjectGuid == request.ObjectGuid && r.UserGuid == request.UserGuid);

            // Create a response object
            var response = new ServiceResponse<string>();

            if (existingRequest != null)
            {
                // If the request with the same objectGuid and userGuid is already in the queue, remove it
                _context.Queue.Remove(existingRequest);
                response.Data = "Request with matching ObjectGuid and UserGuid removed from the queue.";
                response.Success = true;
            }
            else
            {
                // If the request is not in the queue, add it
                _context.Queue.Add(request);
                response.Data = JsonConvert.SerializeObject(request);
                response.Success = true;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            return response;
        }


        //public async Task<ServiceResponse<string>> UpdateQueueAsync(QueueRequest request)
        //{
        //    int index = _context.Queue.Count() + 1;

        //    _context.Queue.Add(request);
        //    await _context.SaveChangesAsync();

        //    var response = new ServiceResponse<string>
        //    {
        //        Data = JsonConvert.SerializeObject(request)
        //    };

        //    return response;
        //}
    }
}
