using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.UserManagement.DataServices.Migrations
{
    /// <inheritdoc />
    public partial class _454545 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Lecturers_LecturerId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_LecturerId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Students",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<Guid>(
                name: "LecturerId",
                table: "Students",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_LecturerId",
                table: "Students",
                column: "LecturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Lecturers_LecturerId",
                table: "Students",
                column: "LecturerId",
                principalTable: "Lecturers",
                principalColumn: "Id");
        }
    }
}
