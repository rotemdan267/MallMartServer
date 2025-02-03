using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MallMartDB.Migrations
{
    public partial class DeleteAcquisitionOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcquisitionOrderLines");

            migrationBuilder.DropTable(
                name: "AcquisitionOrders");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEM1gTXnBnVXf0xPk9CvvPF7Vyy2a7EAh2VYmz0OLeSZ2g5CXhISLgn7TXiNLuMf34Q==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEOantsllx4f6cNjIei89aEAYmo6gSiJcxPOluUAafzJYVfv7BfybHMzSHpTxNBoq1w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAELe7jXn4rCBp+8cib/4GAwOFPiJbBi3YDGkVYWKHSh9ZFMThbbI4ZPgH7tQbyCi+Hg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEJ6TZkV2+MbtS7ArAxYu3eo/ZIqjxtIsMat38oclQyFHU33Z4W6DgAVUltyEabGzBw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEPBK1fhRPbuk3nSwVqojSI4GrOq/hf8Sej+PP9CPIuzXoCVDsZEITbdu+UDSq3vAwg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEFnWE/dNYq70zoXbi3E02TFjH1WnS7Tx0x1oSXWouuSca0CUC8FpDAYggBrPnKV/FQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEKsNupSDWbV+t3WsvRg1CfXT60EslSFGpjvOIdJMI491H9uCUvqS+2UlwHc/8VDfGQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEInaH4PgR7Tnh0xj3nNHGHV2ebJgid4zJE1LCkQoL8RERO7s5oqOQ3kbeu7GD9O0uA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEMg+9LXMgKpdCkQVZk3LwHSk2Ev1yPQtdqcQ2fZ5EqMc5XMoMysPCz0zi1ccOdub7w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEDc5bzpueIjxxFop1mwUNfkKhW6DCa4dFSyTgZAMICvL15x8FuRbP++wYkw7lhkApA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEDSDj3vTOd2sJMbnKa4ww8CFk5eJKz0no8Q/a/M6OUsR1BOH7ckN7TsN3ZknVmcYgw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEKkWGvy6/vu0E43VdlwsIaG69KsiDZUi9KDPClhrgR1TCMRPqPhVJUgeiiCZUzWQ9A==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateTable(
                name: "AcquisitionOrders",
                columns: table => new
                {
                    AcquisitionOrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOrdered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueTimeFirst = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueTimeLast = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsOrderDone = table.Column<bool>(type: "bit", nullable: false),
                    PricePaid = table.Column<float>(type: "real", nullable: true),
                    TotalPrice = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcquisitionOrders", x => x.AcquisitionOrderId);
                    table.ForeignKey(
                        name: "FK_AcquisitionOrders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcquisitionOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcquisitionOrderLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcquisitionOrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcquisitionOrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcquisitionOrderLines_AcquisitionOrders_AcquisitionOrderId",
                        column: x => x.AcquisitionOrderId,
                        principalTable: "AcquisitionOrders",
                        principalColumn: "AcquisitionOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcquisitionOrderLines_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEGJGY3/aOWOZ5mN2KbwIpvyGVUvGKY5jm3j/YZykmnVxoEbQ5xAx0JgavKSA/bIRyA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEMs2+zJr0hcrb7MNtodq14bp+jrym+WG4/vd/tsgJrSF7OnjgpLQSswB61OZT070sg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEM99rslJTCujdr9M+JrnJ2MTzfXniw15pKzcUEBrmu5pUkHi+9w4jYxw+h2wwZGJJA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEPjH7MHF7uAosFiZ2w4cgzlyPPsrc0xjkP71O++31iZWX5VWSs711uGhLSawnq1tDw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAENgJ/SWwtciea0mDUWkXl3h1yUgT5ukjWbHtlC2guMKHGmQMcmrmD61SLIqhhVokfA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEJrb5JVUXSvhDhljcaoIpqcyRYEq7+9OgGPup70K2IcCXmMMeK316PaJnvNh3KY71w==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAENgC4zNJqRS3CF9jNE2Ku4oXPmZCkaz1Irufy64Dc9tDUruVN321Le0aaDTsbRp3LQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEJ6ayii5sJCcch5+oSdiSC4U0azBEgVacAoMLUqHqRtx9x5dTPBoV+IzItf/L9e6JQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEESppWFJrZIAh+zJWCpERAU1BH57gJp+a7Nd69aR8ruA0aqCWafun+QGwFNRquJ4kg==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEBCaT0awuh+19eO+14XsmNwcnipOOUEhZPAEohmdx+syKq1H0i838flo6d5SKUOmoQ==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 11,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAENVqtfVxP9YenkQoz1S6qCFFyimvPsyWSvZdg3Iou+hi647FDjn1FHVfKWo+J9xlHA==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 12,
                column: "HashedPassword",
                value: "AQAAAAEAACcQAAAAEKeMm8Z0NXdBanCMUSNBmdMtKzX+5S5Ua7ATHYFs1eY9VzVvtNaVQr9nKk2pFhjSgw==");

            migrationBuilder.CreateIndex(
                name: "IX_AcquisitionOrderLines_AcquisitionOrderId",
                table: "AcquisitionOrderLines",
                column: "AcquisitionOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AcquisitionOrderLines_ProductId",
                table: "AcquisitionOrderLines",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AcquisitionOrders_EmployeeId",
                table: "AcquisitionOrders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcquisitionOrders_SupplierId",
                table: "AcquisitionOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_AddressId",
                table: "Suppliers",
                column: "AddressId");
        }
    }
}
