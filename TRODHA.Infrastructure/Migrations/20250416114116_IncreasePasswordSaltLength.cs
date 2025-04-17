using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TRODHA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncreasePasswordSaltLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordSalt",
                table: "Users",
                type: "nvarchar(255)",  // Increase from 100 to 255
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
