using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class RemoveIsoCodePropertyInProgramTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsoCode",
                table: "Programs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsoCode",
                table: "Programs",
                type: "int",
                nullable: true);
        }
    }
}
