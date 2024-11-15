using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "QuantityStock");

            migrationBuilder.AddColumn<int>(
                name: "QuantityTransaction",
                table: "DealProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantityTransaction",
                table: "DealProducts");

            migrationBuilder.RenameColumn(
                name: "QuantityStock",
                table: "Products",
                newName: "Quantity");
        }
    }
}
