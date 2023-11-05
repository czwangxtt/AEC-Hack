
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
                    Guid = new Guid("bc685571-7d85-42b3-90af-8f25afd827f3"),
                    Name = "CW_Panel_A1001.3dm",
                    Type = "Rhino",
                    Description = "A rhino facade model",
                    Url = "https://aecademyhub.blob.core.windows.net/aecademyhub/bc685571-7d85-42b3-90af-8f25afd827f3/CW_Panel_A1001.3dm",
                    PreviewUrl = "https://aecademyhub.blob.core.windows.net/aecademyhub/bc685571-7d85-42b3-90af-8f25afd827f3/CW_Panel_A1001.png",
                    DownloadNumber = 1,
                    PermissionLevel = "Public",
                    AuthorGuid = Guid.NewGuid(),
                    Reviews = "Test Reviews"
                }
            );

            modelBuilder.Entity<QueueRequest>().HasData(
                               new QueueRequest
                               {
                                   UserGuid = Guid.NewGuid(),
                                   ObjectGuid = new Guid("bc685571-7d85-42b3-90af-8f25afd827f3")
                               }
                                          );
        }

        public DbSet<Object> Objects { get; set; }
        public DbSet<QueueRequest> Queue { get; set; }

    }
}