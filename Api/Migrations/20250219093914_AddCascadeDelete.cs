using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stocks_StockId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stocks_StockId",
                table: "Comments",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stocks_StockId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stocks_StockId",
                table: "Comments",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "StockId");
        }
    }
}
