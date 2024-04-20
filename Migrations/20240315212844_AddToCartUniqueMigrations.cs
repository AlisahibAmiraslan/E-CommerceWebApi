using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NextEcommerceWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddToCartUniqueMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "AddToCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "AddToCarts");
        }
    }
}
