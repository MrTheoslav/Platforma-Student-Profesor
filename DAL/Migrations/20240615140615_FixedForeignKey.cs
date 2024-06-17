using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixedForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Assignments_FileID",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_FileID",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "FileID",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_AssigmentID",
                table: "Files",
                column: "AssigmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UserID",
                table: "Files",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Assignments_AssigmentID",
                table: "Files",
                column: "AssigmentID",
                principalTable: "Assignments",
                principalColumn: "AssignmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UserID",
                table: "Files",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Assignments_AssigmentID",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UserID",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_AssigmentID",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UserID",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "FileID",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Assignments_FileID",
                table: "Files",
                column: "FileID",
                principalTable: "Assignments",
                principalColumn: "AssignmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_FileID",
                table: "Files",
                column: "FileID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
