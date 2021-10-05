using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class BindAdminToIsoCodeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "IsoCode",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IsoCode_AdminId",
                table: "IsoCode",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_IsoCode_Admins_AdminId",
                table: "IsoCode",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IsoCode_Admins_AdminId",
                table: "IsoCode");

            migrationBuilder.DropIndex(
                name: "IX_IsoCode_AdminId",
                table: "IsoCode");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "IsoCode");
        }
    }
}
