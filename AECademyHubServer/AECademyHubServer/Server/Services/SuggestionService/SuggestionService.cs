using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AECademyHubServer.Server.Services.SuggestionService
{
    public class SuggestionService : ISuggestionService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;


        public SuggestionService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<List<Suggestion>> HandleSuggestionAsync(SuggestionRequest request)
        {
            List<Suggestion> suggestions = new List<Suggestion>();
            var Objects = await _context.Objects.ToListAsync();
            foreach (var obj in Objects)
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
            
            return suggestions;
        }
    }
}
