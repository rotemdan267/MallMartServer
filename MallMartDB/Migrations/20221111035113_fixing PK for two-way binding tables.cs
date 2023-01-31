using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallMartDB.Migrations
{
    public partial class fixingPKfortwowaybindingtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRegions",
                table: "EmployeeRegions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcquisitionOrderLines",
                table: "AcquisitionOrderLines");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderLines",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmployeeRegions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AcquisitionOrderLines",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRegions",
                table: "EmployeeRegions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcquisitionOrderLines",
                table: "AcquisitionOrderLines",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRegions_EmployeeId",
                table: "EmployeeRegions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcquisitionOrderLines_AcquisitionOrderId",
                table: "AcquisitionOrderLines",
                column: "AcquisitionOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines");

            migrationBuilder.DropIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeRegions",
                table: "EmployeeRegions");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeRegions_EmployeeId",
                table: "EmployeeRegions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcquisitionOrderLines",
                table: "AcquisitionOrderLines");

            migrationBuilder.DropIndex(
                name: "IX_AcquisitionOrderLines_AcquisitionOrderId",
                table: "AcquisitionOrderLines");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderLines");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeeRegions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AcquisitionOrderLines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderLines",
                table: "OrderLines",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeRegions",
                table: "EmployeeRegions",
                columns: new[] { "EmployeeId", "RegionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcquisitionOrderLines",
                table: "AcquisitionOrderLines",
                columns: new[] { "AcquisitionOrderId", "ProductId" });
        }
    }
}
