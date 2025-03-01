using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class StepOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Step",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Step");
        }
    }
}
