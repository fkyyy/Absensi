using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatemigrationattendancee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Attendances_AttendanceIdAttendance",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_AttendanceIdAttendance",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "AttendanceIdAttendance",
                table: "Attachments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttendanceIdAttendance",
                table: "Attachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_AttendanceIdAttendance",
                table: "Attachments",
                column: "AttendanceIdAttendance");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Attendances_AttendanceIdAttendance",
                table: "Attachments",
                column: "AttendanceIdAttendance",
                principalTable: "Attendances",
                principalColumn: "IdAttendance");
        }
    }
}
