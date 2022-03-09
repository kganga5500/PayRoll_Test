using Microsoft.EntityFrameworkCore.Migrations;

namespace PaylocityProduct.Infrastructure.Migrations
{
    public partial class Mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "BenefitCostId",
                table: "Dependents",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "BenefitCostId",
                table: "Dependents",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte));
        }
    }
}
