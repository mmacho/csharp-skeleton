using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aseme_api.Migrations
{
    /// <inheritdoc />
    public partial class AddedRestoreIcp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseOperation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Distributor = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseOperation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OnlineMeter",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SupplyPoint = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false),
                    OperationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineMeter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OnlineMeter_BaseOperation_Id",
                        column: x => x.Id,
                        principalTable: "BaseOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestoreIcp",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SupplyPoint = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    OperationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestoreIcp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RestoreIcp_BaseOperation_Id",
                        column: x => x.Id,
                        principalTable: "BaseOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnlineMeterDetails",
                columns: table => new
                {
                    OnlineMeterId = table.Column<long>(type: "bigint", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstallationYear = table.Column<int>(type: "int", nullable: false),
                    ReadingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Period = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineMeterDetails", x => x.OnlineMeterId);
                    table.ForeignKey(
                        name: "FK_OnlineMeterDetails_OnlineMeter_OnlineMeterId",
                        column: x => x.OnlineMeterId,
                        principalTable: "OnlineMeter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResctoreIcpDetails",
                columns: table => new
                {
                    RestoreIcpId = table.Column<long>(type: "bigint", nullable: false),
                    RestoreIcpStatus = table.Column<int>(type: "int", nullable: false),
                    ExecutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResctoreIcpDetails", x => x.RestoreIcpId);
                    table.ForeignKey(
                        name: "FK_ResctoreIcpDetails_RestoreIcp_RestoreIcpId",
                        column: x => x.RestoreIcpId,
                        principalTable: "RestoreIcp",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlineMeterDetails");

            migrationBuilder.DropTable(
                name: "ResctoreIcpDetails");

            migrationBuilder.DropTable(
                name: "OnlineMeter");

            migrationBuilder.DropTable(
                name: "RestoreIcp");

            migrationBuilder.DropTable(
                name: "BaseOperation");
        }
    }
}
