using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVLD.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019f016b-5d2c-7838-8817-b9b9f916ab20",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { "ADMIN@DVLD.COM", "admin@dvld.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019f016b-5d2c-7838-8817-b9b9f916ab20",
                columns: new[] { "NormalizedUserName", "UserName" },
                values: new object[] { null, null });
        }
    }
}
