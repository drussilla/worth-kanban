using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanBoard.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderToStage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Stages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Stages");
        }
    }
}
