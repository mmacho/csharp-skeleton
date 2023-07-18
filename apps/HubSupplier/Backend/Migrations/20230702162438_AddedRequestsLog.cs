using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aseme_api.Migrations
{
    /// <inheritdoc />
    public partial class AddedRequestsLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestsLog",
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
                    table.PrimaryKey("PK_RequestsLog", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestsLog");
        }
    }
}