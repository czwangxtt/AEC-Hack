
using Object = AECademyHubServer.Shared.Object.Object;

namespace AECademyHubServer.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Object>().HasData(
                new Object
                {
                    Guid = Guid.NewGuid(),
                    Name = "Test Object",
                    Type = "Test Type",
                    Description = "Test Description",
                    Url = "Test Url",
                    PreviewUrl = "Test Preview Url",
                    DownloadNumber = 0,
                    PermissionLevel = "Test Permission Level",
                    AuthorGuid = Guid.NewGuid(),
                    Reviews = "Test Reviews"
                }
            );
        }

        public DbSet<Object> Objects { get; set; }
    }
}