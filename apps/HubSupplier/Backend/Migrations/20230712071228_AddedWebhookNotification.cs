using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aseme_api.Migrations
{
    /// <inheritdoc />
    public partial class AddedWebhookNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebhookNotification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebhookNotification_BaseNotification_Id",
                        column: x => x.Id,
                        principalTable: "BaseNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebhookNotification_Url",
                table: "WebhookNotification",
                column: "Url");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebhookNotification");
        }
    }
}
