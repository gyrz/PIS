using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PIS.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strPostCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ePublicPlace = table.Column<int>(type: "int", nullable: false),
                    strPlaceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strHouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtBeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strAbbrev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtBeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BillingAddressId = table.Column<long>(type: "bigint", nullable: true),
                    MailingAddressId = table.Column<long>(type: "bigint", nullable: true),
                    strOrderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eStatus = table.Column<int>(type: "int", nullable: false),
                    ePayment = table.Column<int>(type: "int", nullable: false),
                    strRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dtBeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Address_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_Address_MailingAddressId",
                        column: x => x.MailingAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ePriceType = table.Column<int>(type: "int", nullable: false),
                    nPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: true),
                    MaterialId = table.Column<long>(type: "bigint", nullable: true),
                    dtBeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Price_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dQuantity = table.Column<double>(type: "float", nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: true),
                    strRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<long>(type: "bigint", nullable: true),
                    dtBeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strAbbrev = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    iSIMultiplier = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: true),
                    dtBeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Material",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    strDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryUnitId = table.Column<long>(type: "bigint", nullable: true),
                    eMaterialType = table.Column<int>(type: "int", nullable: false),
                    dtBeg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dtCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Material", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Material_Unit_PrimaryUnitId",
                        column: x => x.PrimaryUnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Material_PrimaryUnitId",
                table: "Material",
                column: "PrimaryUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_BillingAddressId",
                table: "Order",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MailingAddressId",
                table: "Order",
                column: "MailingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_MaterialId",
                table: "OrderItem",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Price_CurrencyId",
                table: "Price",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Price_MaterialId",
                table: "Price",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_MaterialId",
                table: "Unit",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Material_MaterialId",
                table: "Price",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Material_MaterialId",
                table: "OrderItem",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Material_MaterialId",
                table: "Unit",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_Unit_PrimaryUnitId",
                table: "Material");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Material");
        }
    }
}
