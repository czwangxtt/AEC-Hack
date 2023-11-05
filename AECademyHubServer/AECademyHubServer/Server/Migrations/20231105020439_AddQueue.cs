using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AECademyHubServer.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddQueue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reviews",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PreviewUrl",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PermissionLevel",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Queue",
                columns: table => new
                {
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queue", x => x.UserGuid);
                });

            migrationBuilder.UpdateData(
                table: "Objects",
                keyColumn: "Guid",
                keyValue: new Guid("bc685571-7d85-42b3-90af-8f25afd827f3"),
                columns: new[] { "AuthorGuid", "Description", "DownloadNumber", "Name", "PermissionLevel", "PreviewUrl", "Type", "Url" },
                values: new object[] { new Guid("dc765a23-3913-44c2-aacb-0df49ce58385"), "A rhino facade model", 1, "CW_Panel_A1001.3dm", "Public", "https://aecademyhub.blob.core.windows.net/aecademyhub/bc685571-7d85-42b3-90af-8f25afd827f3/CW_Panel_A1001.png", "Rhino", "https://aecademyhub.blob.core.windows.net/aecademyhub/bc685571-7d85-42b3-90af-8f25afd827f3/CW_Panel_A1001.3dm" });

            migrationBuilder.InsertData(
                table: "Queue",
                columns: new[] { "UserGuid", "ObjectGuid" },
                values: new object[] { new Guid("f24be677-c15f-44d0-a110-d75181fba29e"), new Guid("bc685571-7d85-42b3-90af-8f25afd827f3") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queue");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Reviews",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PreviewUrl",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PermissionLevel",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Objects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Objects",
                keyColumn: "Guid",
                keyValue: new Guid("bc685571-7d85-42b3-90af-8f25afd827f3"),
                columns: new[] { "AuthorGuid", "Description", "DownloadNumber", "Name", "PermissionLevel", "PreviewUrl", "Type", "Url" },
                values: new object[] { new Guid("e4962d2e-3ca4-4716-93fc-777ee8314431"), "Test Description", 0, "Test Object", "Test Permission Level", "Test Preview Url", "Test Type", "Test Url" });
        }
    }
}
