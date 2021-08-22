using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class MakeAuthRelationShipToAdminAList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Admins_AuthId",
                table: "Admins");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AuthId",
                table: "Admins",
                column: "AuthId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Admins_AuthId",
                table: "Admins");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AuthId",
                table: "Admins",
                column: "AuthId",
                unique: true);
        }
    }
}
