using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AECademyHubServer.Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviewUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DownloadNumber = table.Column<int>(type: "int", nullable: false),
                    PermissionLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reviews = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Guid);
                });

            migrationBuilder.InsertData(
                table: "Objects",
                columns: new[] { "Guid", "AuthorGuid", "Description", "DownloadNumber", "Name", "PermissionLevel", "PreviewUrl", "Reviews", "Type", "Url" },
                values: new object[] { new Guid("bc685571-7d85-42b3-90af-8f25afd827f3"), new Guid("e4962d2e-3ca4-4716-93fc-777ee8314431"), "Test Description", 0, "Test Object", "Test Permission Level", "Test Preview Url", "Test Reviews", "Test Type", "Test Url" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Objects");
        }
    }
}
