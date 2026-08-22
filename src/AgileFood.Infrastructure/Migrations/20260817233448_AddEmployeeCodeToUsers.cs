using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileFood.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeCodeToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeCode",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.Sql(
                "UPDATE [Users] SET [EmployeeCode] = RIGHT('000000' + CAST([Id] AS VARCHAR(20)), 6) WHERE [EmployeeCode] IS NULL;");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeCode",
                table: "Users",
                column: "EmployeeCode",
                unique: true,
                filter: "[EmployeeCode] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_EmployeeCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmployeeCode",
                table: "Users");
        }
    }
}
