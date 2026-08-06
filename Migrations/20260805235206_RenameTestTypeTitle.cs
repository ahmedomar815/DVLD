using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVLD.Migrations
{
    /// <inheritdoc />
    public partial class RenameTestTypeTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestTypeTitle",
                table: "TestTypes",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TestTypeFees",
                table: "TestTypes",
                newName: "Fees");

            migrationBuilder.RenameColumn(
                name: "TestTypeDescription",
                table: "TestTypes",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_TestTypes_TestTypeTitle",
                table: "TestTypes",
                newName: "IX_TestTypes_Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TestTypes",
                newName: "TestTypeTitle");

            migrationBuilder.RenameColumn(
                name: "Fees",
                table: "TestTypes",
                newName: "TestTypeFees");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "TestTypes",
                newName: "TestTypeDescription");

            migrationBuilder.RenameIndex(
                name: "IX_TestTypes_Title",
                table: "TestTypes",
                newName: "IX_TestTypes_TestTypeTitle");
        }
    }
}
