using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAssigment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCreated",
                table: "UserAssigmnents");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Assignments");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "UserAssigmnents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Files",
                table: "UserAssigmnents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Mark",
                table: "UserAssigmnents",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "UserAssigmnents");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "UserAssigmnents");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "UserAssigmnents");

            migrationBuilder.AddColumn<bool>(
                name: "IsCreated",
                table: "UserAssigmnents",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Assignments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Mark",
                table: "Assignments",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
