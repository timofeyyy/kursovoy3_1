using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace application.Migrations
{
    /// <inheritdoc />
    public partial class nupdate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headphones_Producer_Id",
                table: "Headphones");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptop_Producer_Id",
                table: "Laptop");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Producer_Id",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_SmartWatches_Producer_Id",
                table: "SmartWatches");

            migrationBuilder.DropColumn(
                name: "ProcessorFrequncy",
                table: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_SmartWatches_ProducerId",
                table: "SmartWatches",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_ProducerId",
                table: "Phone",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_ProducerId",
                table: "Laptop",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_Headphones_ProducerId",
                table: "Headphones",
                column: "ProducerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Headphones_Producer_ProducerId",
                table: "Headphones",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Laptop_Producer_ProducerId",
                table: "Laptop",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Producer_ProducerId",
                table: "Phone",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmartWatches_Producer_ProducerId",
                table: "SmartWatches",
                column: "ProducerId",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headphones_Producer_ProducerId",
                table: "Headphones");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptop_Producer_ProducerId",
                table: "Laptop");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Producer_ProducerId",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_SmartWatches_Producer_ProducerId",
                table: "SmartWatches");

            migrationBuilder.DropIndex(
                name: "IX_SmartWatches_ProducerId",
                table: "SmartWatches");

            migrationBuilder.DropIndex(
                name: "IX_Phone_ProducerId",
                table: "Phone");

            migrationBuilder.DropIndex(
                name: "IX_Laptop_ProducerId",
                table: "Laptop");

            migrationBuilder.DropIndex(
                name: "IX_Headphones_ProducerId",
                table: "Headphones");

            migrationBuilder.AddColumn<float>(
                name: "ProcessorFrequncy",
                table: "Phone",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddForeignKey(
                name: "FK_Headphones_Producer_Id",
                table: "Headphones",
                column: "Id",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Laptop_Producer_Id",
                table: "Laptop",
                column: "Id",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Producer_Id",
                table: "Phone",
                column: "Id",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SmartWatches_Producer_Id",
                table: "SmartWatches",
                column: "Id",
                principalTable: "Producer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
