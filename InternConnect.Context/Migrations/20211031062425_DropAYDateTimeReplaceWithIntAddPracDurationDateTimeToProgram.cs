using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InternConnect.Context.Migrations
{
    public partial class DropAYDateTimeReplaceWithIntAddPracDurationDateTimeToProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AcademicYear");

            migrationBuilder.AddColumn<DateTime>(
                name: "PracticumEnd",
                table: "Programs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PracticumStart",
                table: "Programs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EndYear",
                table: "AcademicYear",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartYear",
                table: "AcademicYear",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PracticumEnd",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "PracticumStart",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "EndYear",
                table: "AcademicYear");

            migrationBuilder.DropColumn(
                name: "StartYear",
                table: "AcademicYear");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "AcademicYear",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "AcademicYear",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
