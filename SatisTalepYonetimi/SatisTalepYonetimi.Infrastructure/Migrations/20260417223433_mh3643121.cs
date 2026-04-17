using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SatisTalepYonetimi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mh3643121 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceCards_Products_ProductId",
                table: "MaintenanceCards");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseQuotes_Suppliers_SupplierId",
                table: "PurchaseQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequestItems_Products_ProductId",
                table: "SalesRequestItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "TedarikciTanimlari");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "StokTanimlari");

            migrationBuilder.RenameIndex(
                name: "IX_Products_Code",
                table: "StokTanimlari",
                newName: "IX_StokTanimlari_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TedarikciTanimlari",
                table: "TedarikciTanimlari",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StokTanimlari",
                table: "StokTanimlari",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceCards_StokTanimlari_ProductId",
                table: "MaintenanceCards",
                column: "ProductId",
                principalTable: "StokTanimlari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseQuotes_TedarikciTanimlari_SupplierId",
                table: "PurchaseQuotes",
                column: "SupplierId",
                principalTable: "TedarikciTanimlari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequestItems_StokTanimlari_ProductId",
                table: "SalesRequestItems",
                column: "ProductId",
                principalTable: "StokTanimlari",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceCards_StokTanimlari_ProductId",
                table: "MaintenanceCards");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseQuotes_TedarikciTanimlari_SupplierId",
                table: "PurchaseQuotes");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesRequestItems_StokTanimlari_ProductId",
                table: "SalesRequestItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TedarikciTanimlari",
                table: "TedarikciTanimlari");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StokTanimlari",
                table: "StokTanimlari");

            migrationBuilder.RenameTable(
                name: "TedarikciTanimlari",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "StokTanimlari",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_StokTanimlari_Code",
                table: "Products",
                newName: "IX_Products_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceCards_Products_ProductId",
                table: "MaintenanceCards",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseQuotes_Suppliers_SupplierId",
                table: "PurchaseQuotes",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesRequestItems_Products_ProductId",
                table: "SalesRequestItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
