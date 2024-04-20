using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextEcommerceWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddtorcatrandOrderMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductColor",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductSize1Name",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductSize2Name",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductColor",
                table: "AddToCarts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductSize1Name",
                table: "AddToCarts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductSize2Name",
                table: "AddToCarts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductColor",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductSize1Name",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductSize2Name",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductColor",
                table: "AddToCarts");

            migrationBuilder.DropColumn(
                name: "ProductSize1Name",
                table: "AddToCarts");

            migrationBuilder.DropColumn(
                name: "ProductSize2Name",
                table: "AddToCarts");
        }
    }
}
