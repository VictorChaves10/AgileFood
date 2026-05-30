using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileFood.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfAndTransactionPinToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Users",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionPinHash",
                table: "Users",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "PIN_RESET_REQUIRED");

            migrationBuilder.Sql(
                "UPDATE Users SET Cpf = RIGHT('00000000000' + CAST(Id AS varchar(11)), 11) WHERE Cpf = ''");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Cpf",
                table: "Users",
                column: "Cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Cpf",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TransactionPinHash",
                table: "Users");
        }
    }
}
