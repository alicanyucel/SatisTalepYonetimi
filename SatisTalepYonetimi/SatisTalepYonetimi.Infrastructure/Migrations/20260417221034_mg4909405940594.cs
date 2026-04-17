using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatisTalepYonetimi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg4909405940594 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", nullable: false),
                    Address = table.Column<string>(type: "varchar(500)", nullable: false),
                    TaxNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    Unit = table.Column<string>(type: "varchar(20)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestNumber = table.Column<string>(type: "varchar(50)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusValue = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "varchar(1000)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesRequests_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesRequests_Users_RequestedByUserId",
                        column: x => x.RequestedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesRequestItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesRequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesRequestItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalesRequestItems_SalesRequests_SalesRequestId",
                        column: x => x.SalesRequestId,
                        principalTable: "SalesRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesRequestItems_ProductId",
                table: "SalesRequestItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRequestItems_SalesRequestId",
                table: "SalesRequestItems",
                column: "SalesRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRequests_CustomerId",
                table: "SalesRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRequests_RequestedByUserId",
                table: "SalesRequests",
                column: "RequestedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesRequests_RequestNumber",
                table: "SalesRequests",
                column: "RequestNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesRequestItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SalesRequests");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
