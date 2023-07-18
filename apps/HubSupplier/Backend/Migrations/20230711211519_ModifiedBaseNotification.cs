using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aseme_api.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedBaseNotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseNotification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentState = table.Column<int>(type: "int", nullable: false),
                    EntityType = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailNotification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailNotification_BaseNotification_Id",
                        column: x => x.Id,
                        principalTable: "BaseNotification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseNotification_EntityId",
                table: "BaseNotification",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseNotification_EntityType",
                table: "BaseNotification",
                column: "EntityType");

            migrationBuilder.CreateIndex(
                name: "IX_BaseNotification_SentState",
                table: "BaseNotification",
                column: "SentState");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotification_EmailAddress",
                table: "EmailNotification",
                column: "EmailAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}