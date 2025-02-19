using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace application.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_SmartWatches_SmartWatchesId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_SmartWatches_SmartWatchesId",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_SmartWatches_SmartWatchesId",
                table: "Carts",
                column: "SmartWatchesId",
                principalTable: "SmartWatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_SmartWatches_SmartWatchesId",
                table: "Reviews",
                column: "SmartWatchesId",
                principalTable: "SmartWatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_SmartWatches_SmartWatchesId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_SmartWatches_SmartWatchesId",
                table: "Reviews");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_SmartWatches_SmartWatchesId",
                table: "Carts",
                column: "SmartWatchesId",
                principalTable: "SmartWatches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_SmartWatches_SmartWatchesId",
                table: "Reviews",
                column: "SmartWatchesId",
                principalTable: "SmartWatches",
                principalColumn: "Id");
        }
    }
}
