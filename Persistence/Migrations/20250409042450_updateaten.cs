using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateaten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Attendances_IdAttendance",
                table: "Attachments");

            migrationBuilder.AddColumn<Guid>(
                name: "AttachmentId",
                table: "Attendances",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttendanceType",
                table: "Attendances",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_AttachmentId",
                table: "Attendances",
                column: "AttachmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Attendances_IdAttendance",
                table: "Attachments",
                column: "IdAttendance",
                principalTable: "Attendances",
                principalColumn: "IdAttendance",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Attachments_AttachmentId",
                table: "Attendances",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "IdAttachment",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Attendances_IdAttendance",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Attachments_AttachmentId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_AttachmentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "AttachmentId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "AttendanceType",
                table: "Attendances");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Attendances_IdAttendance",
                table: "Attachments",
                column: "IdAttendance",
                principalTable: "Attendances",
                principalColumn: "IdAttendance",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
