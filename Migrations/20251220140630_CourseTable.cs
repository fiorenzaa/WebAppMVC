using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMVC.Migrations
{
    /// <inheritdoc />
    public partial class CourseTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "End",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lecturer",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Start",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Attendance",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendance",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 12, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Day", "End", "Lecturer", "Place", "Start" },
                values: new object[] { "Monday", "03.00 PM", "Bunyamin, S.Kom, M.Kom.", "TULT 1201", "01.00 PM" });

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CourseName", "Day", "End", "Lecturer", "Place", "Start" },
                values: new object[] { "Sistem Basis Data", "Wednesday", "10.00 AM", "Veronikha, S.Kom, M.Kom.", "Gedung A Ruangan 0210", "08.00 AM" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Lecturer",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Course");

            migrationBuilder.UpdateData(
                table: "Attendance",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Attendance",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Course",
                keyColumn: "Id",
                keyValue: 2,
                column: "CourseName",
                value: "Basis Data");
        }
    }
}
