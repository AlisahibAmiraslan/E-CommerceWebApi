using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextEcommerceWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddtoCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StockQty",
                table: "AddToCarts",
                newName: "SeriePrice");

            migrationBuilder.AddColumn<int>(
                name: "ProductSizeId",
                table: "AddToCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "AddToCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSizeId",
                table: "AddToCarts");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "AddToCarts");

            migrationBuilder.RenameColumn(
                name: "SeriePrice",
                table: "AddToCarts",
                newName: "StockQty");
        }
    }
}
