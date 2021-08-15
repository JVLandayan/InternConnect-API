using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class DbConfigs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Programs_ProgramId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_ProgramId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_SectionId",
                table: "Admins");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "Admins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramId",
                table: "Admins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Programs_ProgramId",
                table: "Admins",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Programs_ProgramId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_ProgramId",
                table: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_SectionId",
                table: "Admins");

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProgramId",
                table: "Admins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_ProgramId",
                table: "Admins",
                column: "ProgramId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admins_SectionId",
                table: "Admins",
                column: "SectionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Programs_ProgramId",
                table: "Admins",
                column: "ProgramId",
                principalTable: "Programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
