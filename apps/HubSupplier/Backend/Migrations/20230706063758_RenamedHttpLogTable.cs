using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aseme_api.Migrations
{
    /// <inheritdoc />
    public partial class RenamedHttpLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestsLog");

            migrationBuilder.CreateTable(
                name: "HttpLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Scheme = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    HttpMethod = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    HttpPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HttpQueryParams = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpRequestHeaders = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpRequestBody = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpResponseHeaders = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpResponseBody = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpStatusCode = table.Column<int>(type: "int", nullable: false),
                    ExecutionTime = table.Column<int>(type: "int", nullable: true),
                    EntityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HttpLog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HttpLog_EntityId",
                table: "HttpLog",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_HttpLog_HttpStatusCode",
                table: "HttpLog",
                column: "HttpStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_HttpLog_IpAddress",
                table: "HttpLog",
                column: "IpAddress");

            migrationBuilder.CreateIndex(
                name: "IX_HttpLog_ReceivedDateTime",
                table: "HttpLog",
                column: "ReceivedDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HttpLog");

            migrationBuilder.CreateTable(
                name: "RequestsLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EntityId = table.Column<long>(type: "bigint", nullable: true),
                    ExecutionTime = table.Column<int>(type: "int", nullable: true),
                    HttpMethod = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    HttpPath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HttpQueryParams = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpRequestBody = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpRequestHeaders = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpResponseBody = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpResponseHeaders = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    HttpStatusCode = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    ReceivedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Scheme = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestsLog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestsLog_EntityId",
                table: "RequestsLog",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsLog_HttpStatusCode",
                table: "RequestsLog",
                column: "HttpStatusCode");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsLog_IpAddress",
                table: "RequestsLog",
                column: "IpAddress");

            migrationBuilder.CreateIndex(
                name: "IX_RequestsLog_ReceivedDateTime",
                table: "RequestsLog",
                column: "ReceivedDateTime");
        }
    }
}
