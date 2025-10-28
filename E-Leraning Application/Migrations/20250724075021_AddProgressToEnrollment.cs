using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Leraning_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddProgressToEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Progress",
                table: "Enrollments",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Enrollments");
        }
    }
}
