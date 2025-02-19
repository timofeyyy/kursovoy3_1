using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace application.Migrations
{
    /// <inheritdoc />
    public partial class update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headphones_Orders_OrdersId",
                table: "Headphones");

            migrationBuilder.DropForeignKey(
                name: "FK_Laptop_Orders_OrdersId",
                table: "Laptop");

            migrationBuilder.DropForeignKey(
                name: "FK_Phone_Orders_OrdersId",
                table: "Phone");

            migrationBuilder.DropForeignKey(
                name: "FK_SmartWatches_Orders_OrdersId",
                table: "SmartWatches");

            migrationBuilder.DropIndex(
                name: "IX_SmartWatches_OrdersId",
                table: "SmartWatches");

            migrationBuilder.DropIndex(
                name: "IX_Phone_OrdersId",
                table: "Phone");

            migrationBuilder.DropIndex(
                name: "IX_Laptop_OrdersId",
                table: "Laptop");

            migrationBuilder.DropIndex(
                name: "IX_Headphones_OrdersId",
                table: "Headphones");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "SmartWatches");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "Phone");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "Laptop");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "Headphones");

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HeadphonesId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaptopId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhonesId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SmartWatchesId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_HeadphonesId",
                table: "Orders",
                column: "HeadphonesId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LaptopId",
                table: "Orders",
                column: "LaptopId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PhonesId",
                table: "Orders",
                column: "PhonesId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SmartWatchesId",
                table: "Orders",
                column: "SmartWatchesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Headphones_HeadphonesId",
                table: "Orders",
                column: "HeadphonesId",
                principalTable: "Headphones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Laptop_LaptopId",
                table: "Orders",
                column: "LaptopId",
                principalTable: "Laptop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Phone_PhonesId",
                table: "Orders",
                column: "PhonesId",
                principalTable: "Phone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SmartWatches_SmartWatchesId",
                table: "Orders",
                column: "SmartWatchesId",
                principalTable: "SmartWatches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Headphones_HeadphonesId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Laptop_LaptopId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Phone_PhonesId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SmartWatches_SmartWatchesId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_HeadphonesId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_LaptopId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PhonesId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SmartWatchesId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "HeadphonesId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LaptopId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhonesId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SmartWatchesId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "SmartWatches",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "Phone",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "Laptop",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdersId",
                table: "Headphones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SmartWatches_OrdersId",
                table: "SmartWatches",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_OrdersId",
                table: "Phone",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_OrdersId",
                table: "Laptop",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Headphones_OrdersId",
                table: "Headphones",
                column: "OrdersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Headphones_Orders_OrdersId",
                table: "Headphones",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptop_Orders_OrdersId",
                table: "Laptop",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Phone_Orders_OrdersId",
                table: "Phone",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SmartWatches_Orders_OrdersId",
                table: "SmartWatches",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
