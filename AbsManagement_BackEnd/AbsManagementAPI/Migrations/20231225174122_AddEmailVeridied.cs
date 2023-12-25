using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbsManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailVeridied : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmailVerified",
                table: "CanBo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerified",
                table: "CanBo");
        }
    }
}
