using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aseme_api.Migrations
{
    /// <inheritdoc />
    public partial class AddedIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineMeterDetails_OnlineMeter_OnlineMeterId",
                table: "OnlineMeterDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ResctoreIcpDetails_RestoreIcp_RestoreIcpId",
                table: "ResctoreIcpDetails");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "OnlineMeterDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "OnlineMeterDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<long>(
                name: "OnlineMeterDetailsOnlineMeterId",
                table: "OnlineMeter",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_RestoreIcp_SerialNumber",
                table: "RestoreIcp",
                column: "SerialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_RestoreIcp_SupplyPoint",
                table: "RestoreIcp",
                column: "SupplyPoint");

            migrationBuilder.CreateIndex(
                name: "IX_ResctoreIcpDetails_ExecutionDate",
                table: "ResctoreIcpDetails",
                column: "ExecutionDate");

            migrationBuilder.CreateIndex(
                name: "IX_ResctoreIcpDetails_RestoreIcpStatus",
                table: "ResctoreIcpDetails",
                column: "RestoreIcpStatus");

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

            migrationBuilder.CreateIndex(
                name: "IX_OnlineMeterDetails_Manufacturer",
                table: "OnlineMeterDetails",
                column: "Manufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineMeterDetails_ReadingDate",
                table: "OnlineMeterDetails",
                column: "ReadingDate");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineMeterDetails_SerialNumber",
                table: "OnlineMeterDetails",
                column: "SerialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineMeter_OnlineMeterDetailsOnlineMeterId",
                table: "OnlineMeter",
                column: "OnlineMeterDetailsOnlineMeterId");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineMeter_OperationStatus",
                table: "OnlineMeter",
                column: "OperationStatus");

            migrationBuilder.CreateIndex(
                name: "IX_OnlineMeter_SupplyPoint",
                table: "OnlineMeter",
                column: "SupplyPoint");

            migrationBuilder.CreateIndex(
                name: "IX_BaseOperation_CreatedDate",
                table: "BaseOperation",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_BaseOperation_Distributor",
                table: "BaseOperation",
                column: "Distributor");

            migrationBuilder.CreateIndex(
                name: "IX_BaseOperation_LastModifiedDate",
                table: "BaseOperation",
                column: "LastModifiedDate");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineMeter_OnlineMeterDetails_OnlineMeterDetailsOnlineMeterId",
                table: "OnlineMeter",
                column: "OnlineMeterDetailsOnlineMeterId",
                principalTable: "OnlineMeterDetails",
                principalColumn: "OnlineMeterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineMeterDetails_OnlineMeter_OnlineMeterId",
                table: "OnlineMeterDetails",
                column: "OnlineMeterId",
                principalTable: "OnlineMeter",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ResctoreIcpDetails_RestoreIcp_RestoreIcpId",
                table: "ResctoreIcpDetails",
                column: "RestoreIcpId",
                principalTable: "RestoreIcp",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OnlineMeter_OnlineMeterDetails_OnlineMeterDetailsOnlineMeterId",
                table: "OnlineMeter");

            migrationBuilder.DropForeignKey(
                name: "FK_OnlineMeterDetails_OnlineMeter_OnlineMeterId",
                table: "OnlineMeterDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ResctoreIcpDetails_RestoreIcp_RestoreIcpId",
                table: "ResctoreIcpDetails");

            migrationBuilder.DropIndex(
                name: "IX_RestoreIcp_SerialNumber",
                table: "RestoreIcp");

            migrationBuilder.DropIndex(
                name: "IX_RestoreIcp_SupplyPoint",
                table: "RestoreIcp");

            migrationBuilder.DropIndex(
                name: "IX_ResctoreIcpDetails_ExecutionDate",
                table: "ResctoreIcpDetails");

            migrationBuilder.DropIndex(
                name: "IX_ResctoreIcpDetails_RestoreIcpStatus",
                table: "ResctoreIcpDetails");

            migrationBuilder.DropIndex(
                name: "IX_RequestsLog_EntityId",
                table: "RequestsLog");

            migrationBuilder.DropIndex(
                name: "IX_RequestsLog_HttpStatusCode",
                table: "RequestsLog");

            migrationBuilder.DropIndex(
                name: "IX_RequestsLog_IpAddress",
                table: "RequestsLog");

            migrationBuilder.DropIndex(
                name: "IX_RequestsLog_ReceivedDateTime",
                table: "RequestsLog");

            migrationBuilder.DropIndex(
                name: "IX_OnlineMeterDetails_Manufacturer",
                table: "OnlineMeterDetails");

            migrationBuilder.DropIndex(
                name: "IX_OnlineMeterDetails_ReadingDate",
                table: "OnlineMeterDetails");

            migrationBuilder.DropIndex(
                name: "IX_OnlineMeterDetails_SerialNumber",
                table: "OnlineMeterDetails");

            migrationBuilder.DropIndex(
                name: "IX_OnlineMeter_OnlineMeterDetailsOnlineMeterId",
                table: "OnlineMeter");

            migrationBuilder.DropIndex(
                name: "IX_OnlineMeter_OperationStatus",
                table: "OnlineMeter");

            migrationBuilder.DropIndex(
                name: "IX_OnlineMeter_SupplyPoint",
                table: "OnlineMeter");

            migrationBuilder.DropIndex(
                name: "IX_BaseOperation_CreatedDate",
                table: "BaseOperation");

            migrationBuilder.DropIndex(
                name: "IX_BaseOperation_Distributor",
                table: "BaseOperation");

            migrationBuilder.DropIndex(
                name: "IX_BaseOperation_LastModifiedDate",
                table: "BaseOperation");

            migrationBuilder.DropColumn(
                name: "OnlineMeterDetailsOnlineMeterId",
                table: "OnlineMeter");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "OnlineMeterDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "OnlineMeterDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlineMeterDetails_OnlineMeter_OnlineMeterId",
                table: "OnlineMeterDetails",
                column: "OnlineMeterId",
                principalTable: "OnlineMeter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResctoreIcpDetails_RestoreIcp_RestoreIcpId",
                table: "ResctoreIcpDetails",
                column: "RestoreIcpId",
                principalTable: "RestoreIcp",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}