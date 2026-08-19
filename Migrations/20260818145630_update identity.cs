using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DVLD.Migrations
{
    /// <inheritdoc />
    public partial class updateidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019f016b-5d2c-7838-8817-b9bda94e8ded", "019f01db-ea48-73e7-8e51-4738f9a74412", false, false, "Admin", null },
                    { "019f016b-5d2c-7838-8817-b9bf6308b890", "019f01db-ea48-73e7-8e51-473dfbc6bcd6", false, false, "Member", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permissions", "applications:read", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 2, "Permissions", "applications:create", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 3, "Permissions", "applications:update", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 4, "Permissions", "application-types:read", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 5, "Permissions", "application-types:create", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 6, "Permissions", "application-types:update", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 7, "Permissions", "application-types:delete", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 8, "Permissions", "drivers:read", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 9, "Permissions", "drivers:create", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 10, "Permissions", "driving-license-applications:read", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 11, "Permissions", "driving-license-applications:create", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 12, "Permissions", "licenses:read", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 13, "Permissions", "licenses:create", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 14, "Permissions", "licenses:update", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 15, "Permissions", "license-types:read", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 16, "Permissions", "license-types:create", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 17, "Permissions", "license-types:update", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 18, "Permissions", "test-appointments:read", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 19, "Permissions", "test-appointments:create", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 20, "Permissions", "test-appointments:update", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 21, "Permissions", "tests:create", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 22, "Permissions", "test-types:read", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 23, "Permissions", "test-types:create", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 24, "Permissions", "test-types:update", "019f016b-5d2c-7838-8817-b9bda94e8ded" },
                    { 25, "Permissions", "test-types:delete", "019f016b-5d2c-7838-8817-b9bda94e8ded" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "019f016b-5d2c-7838-8817-b9bda94e8ded", "019f016b-5d2c-7838-8817-b9b9f916ab20" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019f016b-5d2c-7838-8817-b9bf6308b890");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019f016b-5d2c-7838-8817-b9bda94e8ded", "019f016b-5d2c-7838-8817-b9b9f916ab20" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019f016b-5d2c-7838-8817-b9bda94e8ded");
        }
    }
}
