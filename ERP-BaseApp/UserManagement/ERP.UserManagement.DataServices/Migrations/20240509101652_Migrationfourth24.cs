using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.UserManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class Migrationfourth24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Lecturers_AdvisorId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "AdvisorId",
                table: "Students",
                newName: "LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_AdvisorId",
                table: "Students",
                newName: "IX_Students_LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Lecturers_LecturerId",
                table: "Students",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Lecturers_LecturerId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "LecturerId",
                table: "Students",
                newName: "AdvisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_LecturerId",
                table: "Students",
                newName: "IX_Students_AdvisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Lecturers_AdvisorId",
                table: "Students",
                column: "AdvisorId",
                principalTable: "Lecturers",
                principalColumn: "Id");
        }
    }
}
