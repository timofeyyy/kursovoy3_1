using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace application.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "cart_seq");

            migrationBuilder.CreateSequence<int>(
                name: "color_seq");

            migrationBuilder.CreateSequence<int>(
                name: "headphones_image_seq");

            migrationBuilder.CreateSequence<int>(
                name: "laptop_image_seq");

            migrationBuilder.CreateSequence<int>(
                name: "orders_seq");

            migrationBuilder.CreateSequence<int>(
                name: "os_brand_seq");

            migrationBuilder.CreateSequence<int>(
                name: "os_seq");

            migrationBuilder.CreateSequence<int>(
                name: "phone_image_seq");

            migrationBuilder.CreateSequence<int>(
                name: "processor_brand_seq");

            migrationBuilder.CreateSequence<int>(
                name: "processor_model_seq");

            migrationBuilder.CreateSequence<int>(
                name: "producer_seq");

            migrationBuilder.CreateSequence<int>(
                name: "product_seq");

            migrationBuilder.CreateSequence<int>(
                name: "smartwatch_image_seq");

            migrationBuilder.CreateSequence<int>(
                name: "user_seq");

            migrationBuilder.CreateSequence<int>(
                name: "videocard_brand_seq");

            migrationBuilder.CreateSequence<int>(
                name: "videocard_model_seq");

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for color_seq"),
                    Value = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OSBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for os_brand_seq"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    IsLaptop = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessorBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for processor_brand_seq"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for producer_seq"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for user_seq"),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UserImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoCardBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for videocard_brand_seq"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCardBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for os_seq"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OS_OSBrand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "OSBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessorModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for processor_model_seq"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsLaptop = table.Column<bool>(type: "bit", nullable: false),
                    BaseFrequency = table.Column<float>(type: "real", nullable: false),
                    BoostFrequency = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessorModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessorModel_ProcessorBrand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "ProcessorBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for orders_seq"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoCardModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for videocard_model_seq"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BaseFrequency = table.Column<float>(type: "real", nullable: false),
                    BoostFrequency = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCardModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoCardModel_VideoCardBrand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "VideoCardBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Headphones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for product_seq"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    Wireless = table.Column<bool>(type: "bit", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    Wheight = table.Column<float>(type: "real", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headphones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Headphones_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Headphones_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Headphones_Producer_Id",
                        column: x => x.Id,
                        principalTable: "Producer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for product_seq"),
                    ProcessorId = table.Column<int>(type: "int", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<float>(type: "real", maxLength: 30, nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Wheight = table.Column<float>(type: "real", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    OSId = table.Column<int>(type: "int", nullable: false),
                    RAM = table.Column<int>(type: "int", nullable: false),
                    InternalMemorySize = table.Column<int>(type: "int", nullable: false),
                    Camera = table.Column<int>(type: "int", nullable: false),
                    Battery = table.Column<int>(type: "int", nullable: false),
                    ProcessorFrequncy = table.Column<float>(type: "real", nullable: false),
                    WaterProtection = table.Column<bool>(type: "bit", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phone_OS_OSId",
                        column: x => x.OSId,
                        principalTable: "OS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phone_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Phone_ProcessorModel_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "ProcessorModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phone_Producer_Id",
                        column: x => x.Id,
                        principalTable: "Producer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartWatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for product_seq"),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Wheight = table.Column<float>(type: "real", nullable: false),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<float>(type: "real", maxLength: 20, nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    OSId = table.Column<int>(type: "int", nullable: false),
                    Wifi = table.Column<bool>(type: "bit", nullable: false),
                    Bleatouth = table.Column<bool>(type: "bit", nullable: false),
                    Gps = table.Column<bool>(type: "bit", nullable: false),
                    Calls = table.Column<bool>(type: "bit", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartWatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartWatches_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmartWatches_OS_OSId",
                        column: x => x.OSId,
                        principalTable: "OS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmartWatches_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SmartWatches_Producer_Id",
                        column: x => x.Id,
                        principalTable: "Producer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Laptop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for product_seq"),
                    ProducerId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Width = table.Column<float>(type: "real", maxLength: 20, nullable: false),
                    Height = table.Column<float>(type: "real", nullable: false),
                    Wheight = table.Column<float>(type: "real", nullable: false),
                    ProcessorId = table.Column<int>(type: "int", nullable: false),
                    OSId = table.Column<int>(type: "int", nullable: false),
                    RAMMemorySize = table.Column<int>(type: "int", nullable: false),
                    SSDMemorySize = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    VideoCardModelId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laptop_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptop_OS_OSId",
                        column: x => x.OSId,
                        principalTable: "OS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptop_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Laptop_ProcessorModel_ProcessorId",
                        column: x => x.ProcessorId,
                        principalTable: "ProcessorModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptop_Producer_Id",
                        column: x => x.Id,
                        principalTable: "Producer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptop_VideoCardModel_VideoCardModelId",
                        column: x => x.VideoCardModelId,
                        principalTable: "VideoCardModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeadphonesImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for headphones_image_seq"),
                    HeadphonesId = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadphonesImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeadphonesImages_Headphones_HeadphonesId",
                        column: x => x.HeadphonesId,
                        principalTable: "Headphones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhoneImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for phone_image_seq"),
                    PhoneId = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhoneImages_Phone_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SmartWatchesImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for smartwatch_image_seq"),
                    SmartWatchesId = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartWatchesImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartWatchesImages_SmartWatches_SmartWatchesId",
                        column: x => x.SmartWatchesId,
                        principalTable: "SmartWatches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for cart_seq"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LaptopId = table.Column<int>(type: "int", nullable: false),
                    HeadphonesId = table.Column<int>(type: "int", nullable: false),
                    PhonesId = table.Column<int>(type: "int", nullable: false),
                    SmartWatchesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Headphones_HeadphonesId",
                        column: x => x.HeadphonesId,
                        principalTable: "Headphones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_Laptop_LaptopId",
                        column: x => x.LaptopId,
                        principalTable: "Laptop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_Phone_PhonesId",
                        column: x => x.PhonesId,
                        principalTable: "Phone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Carts_SmartWatches_SmartWatchesId",
                        column: x => x.SmartWatchesId,
                        principalTable: "SmartWatches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LaptopImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "next value for laptop_image_seq"),
                    LaptopId = table.Column<int>(type: "int", nullable: false),
                    Img = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaptopImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaptopImages_Laptop_LaptopId",
                        column: x => x.LaptopId,
                        principalTable: "Laptop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_HeadphonesId",
                table: "Carts",
                column: "HeadphonesId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_LaptopId",
                table: "Carts",
                column: "LaptopId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_PhonesId",
                table: "Carts",
                column: "PhonesId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_SmartWatchesId",
                table: "Carts",
                column: "SmartWatchesId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Headphones_ColorId",
                table: "Headphones",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Headphones_OrdersId",
                table: "Headphones",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadphonesImages_HeadphonesId",
                table: "HeadphonesImages",
                column: "HeadphonesId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_ColorId",
                table: "Laptop",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_OrdersId",
                table: "Laptop",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_OSId",
                table: "Laptop",
                column: "OSId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_ProcessorId",
                table: "Laptop",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptop_VideoCardModelId",
                table: "Laptop",
                column: "VideoCardModelId");

            migrationBuilder.CreateIndex(
                name: "IX_LaptopImages_LaptopId",
                table: "LaptopImages",
                column: "LaptopId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OS_BrandId",
                table: "OS",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_ColorId",
                table: "Phone",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_OrdersId",
                table: "Phone",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_OSId",
                table: "Phone",
                column: "OSId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_ProcessorId",
                table: "Phone",
                column: "ProcessorId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneImages_PhoneId",
                table: "PhoneImages",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessorModel_BrandId",
                table: "ProcessorModel",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartWatches_ColorId",
                table: "SmartWatches",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartWatches_OrdersId",
                table: "SmartWatches",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartWatches_OSId",
                table: "SmartWatches",
                column: "OSId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartWatchesImages_SmartWatchesId",
                table: "SmartWatchesImages",
                column: "SmartWatchesId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoCardModel_BrandId",
                table: "VideoCardModel",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "HeadphonesImages");

            migrationBuilder.DropTable(
                name: "LaptopImages");

            migrationBuilder.DropTable(
                name: "PhoneImages");

            migrationBuilder.DropTable(
                name: "SmartWatchesImages");

            migrationBuilder.DropTable(
                name: "Headphones");

            migrationBuilder.DropTable(
                name: "Laptop");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "SmartWatches");

            migrationBuilder.DropTable(
                name: "VideoCardModel");

            migrationBuilder.DropTable(
                name: "ProcessorModel");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "OS");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropTable(
                name: "VideoCardBrand");

            migrationBuilder.DropTable(
                name: "ProcessorBrand");

            migrationBuilder.DropTable(
                name: "OSBrand");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropSequence(
                name: "cart_seq");

            migrationBuilder.DropSequence(
                name: "color_seq");

            migrationBuilder.DropSequence(
                name: "headphones_image_seq");

            migrationBuilder.DropSequence(
                name: "laptop_image_seq");

            migrationBuilder.DropSequence(
                name: "orders_seq");

            migrationBuilder.DropSequence(
                name: "os_brand_seq");

            migrationBuilder.DropSequence(
                name: "os_seq");

            migrationBuilder.DropSequence(
                name: "phone_image_seq");

            migrationBuilder.DropSequence(
                name: "processor_brand_seq");

            migrationBuilder.DropSequence(
                name: "processor_model_seq");

            migrationBuilder.DropSequence(
                name: "producer_seq");

            migrationBuilder.DropSequence(
                name: "product_seq");

            migrationBuilder.DropSequence(
                name: "smartwatch_image_seq");

            migrationBuilder.DropSequence(
                name: "user_seq");

            migrationBuilder.DropSequence(
                name: "videocard_brand_seq");

            migrationBuilder.DropSequence(
                name: "videocard_model_seq");
        }
    }
}
