using Microsoft.EntityFrameworkCore.Migrations;

namespace PaylocityProduct.Infrastructure.Migrations
{
    public partial class Mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependents_BenefitCost_BenefitCostId",
                table: "Dependents");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BenefitCost_BenefitCostId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BenefitCost",
                table: "BenefitCost");

            migrationBuilder.RenameTable(
                name: "BenefitCost",
                newName: "BenefitCosts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BenefitCosts",
                table: "BenefitCosts",
                column: "BenefitCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependents_BenefitCosts_BenefitCostId",
                table: "Dependents",
                column: "BenefitCostId",
                principalTable: "BenefitCosts",
                principalColumn: "BenefitCostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BenefitCosts_BenefitCostId",
                table: "Employees",
                column: "BenefitCostId",
                principalTable: "BenefitCosts",
                principalColumn: "BenefitCostId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependents_BenefitCosts_BenefitCostId",
                table: "Dependents");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BenefitCosts_BenefitCostId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BenefitCosts",
                table: "BenefitCosts");

            migrationBuilder.RenameTable(
                name: "BenefitCosts",
                newName: "BenefitCost");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BenefitCost",
                table: "BenefitCost",
                column: "BenefitCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependents_BenefitCost_BenefitCostId",
                table: "Dependents",
                column: "BenefitCostId",
                principalTable: "BenefitCost",
                principalColumn: "BenefitCostId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BenefitCost_BenefitCostId",
                table: "Employees",
                column: "BenefitCostId",
                principalTable: "BenefitCost",
                principalColumn: "BenefitCostId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
