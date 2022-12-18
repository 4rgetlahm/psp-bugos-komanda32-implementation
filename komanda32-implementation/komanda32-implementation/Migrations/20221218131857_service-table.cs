using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace komanda32_implementation.Migrations
{
    public partial class servicetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Franchises_FranchiseId",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Groups_GroupId",
                table: "Workers");

            migrationBuilder.RenameColumn(
                name: "Emplyment",
                table: "Workers",
                newName: "Employment");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Workers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FranchiseId",
                table: "Workers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ProductServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    isProduct = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FranciseId = table.Column<int>(type: "int", nullable: false),
                    PriceBeforeTaxe = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TaxeId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReservationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DiscountType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductServices", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Franchises_FranchiseId",
                table: "Workers",
                column: "FranchiseId",
                principalTable: "Franchises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Groups_GroupId",
                table: "Workers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Franchises_FranchiseId",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Groups_GroupId",
                table: "Workers");

            migrationBuilder.DropTable(
                name: "ProductServices");

            migrationBuilder.DropTable(
                name: "Shifts");

            migrationBuilder.RenameColumn(
                name: "Employment",
                table: "Workers",
                newName: "Emplyment");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FranchiseId",
                table: "Workers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Franchises_FranchiseId",
                table: "Workers",
                column: "FranchiseId",
                principalTable: "Franchises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Groups_GroupId",
                table: "Workers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
