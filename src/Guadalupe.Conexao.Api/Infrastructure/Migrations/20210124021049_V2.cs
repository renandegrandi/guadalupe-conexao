using Microsoft.EntityFrameworkCore.Migrations;

namespace Guadalupe.Conexao.Api.Infrastructure.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "fcm_token",
                table: "user",
                unicode: false,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fcm_token",
                table: "user");
        }
    }
}
