using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEcommerce2011.DAL.Migrations
{
    public partial class 添加商品信息 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsProp",
                columns: table => new
                {
                    GPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GPTId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsProp", x => x.GPId);
                });

            migrationBuilder.CreateTable(
                name: "GoodsPropType",
                columns: table => new
                {
                    GPTId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GPTName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsPropType", x => x.GPTId);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    GId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BId = table.Column<int>(type: "int", nullable: false),
                    GCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuggestPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    JLDW = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AddTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GTId = table.Column<int>(type: "int", nullable: false),
                    GTIdAll = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GPTId = table.Column<int>(type: "int", nullable: false),
                    GPContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.GId);
                    table.ForeignKey(
                        name: "FK_Goods_Brand_BId",
                        column: x => x.BId,
                        principalTable: "Brand",
                        principalColumn: "BId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goods_GoodsPropType_GPTId",
                        column: x => x.GPTId,
                        principalTable: "GoodsPropType",
                        principalColumn: "GPTId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Goods_BId",
                table: "Goods",
                column: "BId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_GPTId",
                table: "Goods",
                column: "GPTId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "GoodsProp");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "GoodsPropType");
        }
    }
}
