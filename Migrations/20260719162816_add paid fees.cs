using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DVLD.Migrations
{
    /// <inheritdoc />
    public partial class addpaidfees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaidFees",
                table: "Applications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidFees",
                table: "Applications");
        }
    }
}
