using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVLD.Migrations
{
    /// <inheritdoc />
    public partial class modifytheapplicationtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApplicationTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ApplicationTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationTypes_Name",
                table: "ApplicationTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationTypes_Name",
                table: "ApplicationTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ApplicationTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ApplicationTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
