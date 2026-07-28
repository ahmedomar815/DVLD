using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVLD.Migrations
{
    /// <inheritdoc />
    public partial class addmoreentites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaidFees",
                table: "Applications",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "LicenseType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MinimumAllowedAge = table.Column<int>(type: "int", nullable: false),
                    DefaultValidityLength = table.Column<int>(type: "int", nullable: false),
                    Fees = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseType", x => x.Id);
                    table.CheckConstraint("CK_LicenseTypes_Fees", "[Fees] >= 0");
                    table.CheckConstraint("CK_LicenseTypes_MinimumAllowedAge", "[MinimumAllowedAge] >= 18");
                });

            migrationBuilder.CreateTable(
                name: "DrivingLicenseApplication",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LicenseTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrivingLicenseApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrivingLicenseApplication_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrivingLicenseApplication_LicenseType_LicenseTypeId",
                        column: x => x.LicenseTypeId,
                        principalTable: "LicenseType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Application_PaidFees_Positive1",
                table: "ApplicationTypes",
                sql: "[Fees] > 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Application_PaidFees_Positive",
                table: "Applications",
                sql: "[PaidFees] > 0");

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicenseApplication_ApplicationId",
                table: "DrivingLicenseApplication",
                column: "ApplicationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DrivingLicenseApplication_LicenseTypeId",
                table: "DrivingLicenseApplication",
                column: "LicenseTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LicenseType_Name",
                table: "LicenseType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrivingLicenseApplication");

            migrationBuilder.DropTable(
                name: "LicenseType");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Application_PaidFees_Positive1",
                table: "ApplicationTypes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Application_PaidFees_Positive",
                table: "Applications");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaidFees",
                table: "Applications",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
