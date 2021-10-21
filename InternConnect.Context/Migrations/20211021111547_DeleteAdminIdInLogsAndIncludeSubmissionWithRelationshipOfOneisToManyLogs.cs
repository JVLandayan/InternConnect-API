using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class DeleteAdminIdInLogsAndIncludeSubmissionWithRelationshipOfOneisToManyLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Admins_AdminId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_AdminId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_SubmissionId",
                table: "Logs",
                column: "SubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Submissions_SubmissionId",
                table: "Logs",
                column: "SubmissionId",
                principalTable: "Submissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Submissions_SubmissionId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_SubmissionId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Action",
                table: "Logs");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_AdminId",
                table: "Logs",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Admins_AdminId",
                table: "Logs",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
