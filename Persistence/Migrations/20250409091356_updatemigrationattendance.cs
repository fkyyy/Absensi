using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatemigrationattendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdAttendance",
                table: "Attachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_IdAttendance",
                table: "Attachments",
                column: "IdAttendance");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Attendances_IdAttendance",
                table: "Attachments",
                column: "IdAttendance",
                principalTable: "Attendances",
                principalColumn: "IdAttendance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Attendances_IdAttendance",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_IdAttendance",
                table: "Attachments");
    
            migrationBuilder.DropColumn(
                name: "IdAttendance",
                table: "Attachments");
        }
    }
}
