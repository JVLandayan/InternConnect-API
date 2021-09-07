using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class AddAuthToStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_AuthId",
                table: "Students",
                column: "AuthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Authorizations_AuthId",
                table: "Students",
                column: "AuthId",
                principalTable: "Authorizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Authorizations_AuthId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_AuthId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "Students");
        }
    }
}
