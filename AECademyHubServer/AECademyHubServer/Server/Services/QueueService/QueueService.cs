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
