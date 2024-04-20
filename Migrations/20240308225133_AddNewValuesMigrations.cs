using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextEcommerceWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNewValuesMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductCatImage",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "FavoriteProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCatImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "FavoriteProducts");
        }
    }
}
