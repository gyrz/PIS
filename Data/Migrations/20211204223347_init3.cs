using Microsoft.EntityFrameworkCore.Migrations;

namespace PIS.Data.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DefaultUnitPriceId",
                table: "Material",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Material_DefaultUnitPriceId",
                table: "Material",
                column: "DefaultUnitPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Price_DefaultUnitPriceId",
                table: "Material",
                column: "DefaultUnitPriceId",
                principalTable: "Price",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_Price_DefaultUnitPriceId",
                table: "Material");

            migrationBuilder.DropIndex(
                name: "IX_Material_DefaultUnitPriceId",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "DefaultUnitPriceId",
                table: "Material");
        }
    }
}
