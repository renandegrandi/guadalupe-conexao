using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Guadalupe.Conexao.Api.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    nome = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    profile_image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    registration_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    modification_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    removal_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "notice",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    title = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    message = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    image_url = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    id_posted_by = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    registration_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    modification_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    removal_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notice", x => x.id);
                    table.ForeignKey(
                        name: "FK_notice_person_id_posted_by",
                        column: x => x.id_posted_by,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    id_person = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    code_access = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    refresh_token = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    fcm_token = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    registration_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    modification_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    removal_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_person_id_person",
                        column: x => x.id_person,
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notice_id_posted_by",
                table: "notice",
                column: "id_posted_by");

            migrationBuilder.CreateIndex(
                name: "IX_user_id_person",
                table: "user",
                column: "id_person",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notice");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "person");
        }
    }
}
