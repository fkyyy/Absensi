using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Attendances_IdAttendance",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "IdAttendance",
                table: "Attachments",
                newName: "AttendanceIdAttendance");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_IdAttendance",
                table: "Attachments",
                newName: "IX_Attachments_AttendanceIdAttendance");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Attendances_AttendanceIdAttendance",
                table: "Attachments",
                column: "AttendanceIdAttendance",
                principalTable: "Attendances",
                principalColumn: "IdAttendance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Attendances_AttendanceIdAttendance",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "AttendanceIdAttendance",
                table: "Attachments",
                newName: "IdAttendance");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_AttendanceIdAttendance",
                table: "Attachments",
                newName: "IX_Attachments_IdAttendance");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Attendances_IdAttendance",
                table: "Attachments",
                column: "IdAttendance",
                principalTable: "Attendances",
                principalColumn: "IdAttendance",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
