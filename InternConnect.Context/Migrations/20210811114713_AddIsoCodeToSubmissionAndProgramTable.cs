using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class AddIsoCodeToSubmissionAndProgramTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedByChair",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "AcceptedByCoordinator",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "AcceptedByDean",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "CompanyAgrees",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "EmailSentByCoordinator",
                table: "Submissions");

            migrationBuilder.RenameColumn(
                name: "Track",
                table: "Submissions",
                newName: "TrackId");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Submissions",
                newName: "ContactPersonPosition");

            migrationBuilder.RenameColumn(
                name: "CompanyAddress",
                table: "Submissions",
                newName: "ContactPersonEmail");

            migrationBuilder.RenameColumn(
                name: "ContactPerson",
                table: "Companies",
                newName: "AddressTwo");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Companies",
                newName: "AddressOne");

            migrationBuilder.AddColumn<int>(
                name: "AdminResponseId",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IsoCode",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsoCode",
                table: "Programs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DeanName",
                table: "PdfState",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressThree",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdminResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcceptedByCoordinator = table.Column<bool>(type: "bit", nullable: false),
                    AcceptedByChair = table.Column<bool>(type: "bit", nullable: false),
                    AcceptedByDean = table.Column<bool>(type: "bit", nullable: false),
                    EmailSentByCoordinator = table.Column<bool>(type: "bit", nullable: false),
                    CompanyAgrees = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminResponses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_AdminResponseId",
                table: "Submissions",
                column: "AdminResponseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_CompanyId",
                table: "Submissions",
                column: "CompanyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_AdminResponses_AdminResponseId",
                table: "Submissions",
                column: "AdminResponseId",
                principalTable: "AdminResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Submissions_Companies_CompanyId",
                table: "Submissions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_AdminResponses_AdminResponseId",
                table: "Submissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Submissions_Companies_CompanyId",
                table: "Submissions");

            migrationBuilder.DropTable(
                name: "AdminResponses");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_AdminResponseId",
                table: "Submissions");

            migrationBuilder.DropIndex(
                name: "IX_Submissions_CompanyId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "AdminResponseId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "IsoCode",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "IsoCode",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "DeanName",
                table: "PdfState");

            migrationBuilder.DropColumn(
                name: "AddressThree",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "TrackId",
                table: "Submissions",
                newName: "Track");

            migrationBuilder.RenameColumn(
                name: "ContactPersonPosition",
                table: "Submissions",
                newName: "CompanyName");

            migrationBuilder.RenameColumn(
                name: "ContactPersonEmail",
                table: "Submissions",
                newName: "CompanyAddress");

            migrationBuilder.RenameColumn(
                name: "AddressTwo",
                table: "Companies",
                newName: "ContactPerson");

            migrationBuilder.RenameColumn(
                name: "AddressOne",
                table: "Companies",
                newName: "Address");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptedByChair",
                table: "Submissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptedByCoordinator",
                table: "Submissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AcceptedByDean",
                table: "Submissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CompanyAgrees",
                table: "Submissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmailSentByCoordinator",
                table: "Submissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
