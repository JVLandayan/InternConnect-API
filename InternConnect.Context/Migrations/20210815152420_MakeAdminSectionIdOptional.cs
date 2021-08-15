using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class MakeAdminSectionIdOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Sections_SectionId",
                table: "Admins");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Sections_SectionId",
                table: "Admins",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Sections_SectionId",
                table: "Admins");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Sections_SectionId",
                table: "Admins",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
