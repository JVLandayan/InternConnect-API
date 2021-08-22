using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class MakeSectionAndProgramRelationShipAList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_ProgramId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SectionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Admins_ProgramId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_SectionId",
                table: "Admins");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramId",
                table: "Students",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_SectionId",
                table: "Students",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_ProgramId",
                table: "Admins",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_SectionId",
                table: "Admins",
                column: "SectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_ProgramId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_SectionId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Admins_ProgramId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_SectionId",
                table: "Admins");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ProgramId",
                table: "Students",
                column: "ProgramId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_SectionId",
                table: "Students",
                column: "SectionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_ProgramId",
                table: "Admins",
                column: "ProgramId",
                unique: true,
                filter: "[ProgramId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_SectionId",
                table: "Admins",
                column: "SectionId",
                unique: true,
                filter: "[SectionId] IS NOT NULL");
        }
    }
}
