using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEcommerce2011.DAL.Migrations
{
    public partial class 增加了状态 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "UserInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "UserInfo");
        }
    }
}
