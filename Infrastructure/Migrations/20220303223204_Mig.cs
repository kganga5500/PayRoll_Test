using Microsoft.EntityFrameworkCore.Migrations;

namespace PaylocityProduct.Infrastructure.Migrations
{
    public partial class Mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenefitCost",
                columns: table => new
                {
                    BenefitCostId = table.Column<byte>(nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenefitCost", x => x.BenefitCostId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<long>(nullable: false),
                    FirstName = table.Column<string>(type: "varchar(150)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(150)", nullable: false),
                    BenefitCostId = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_BenefitCost_BenefitCostId",
                        column: x => x.BenefitCostId,
                        principalTable: "BenefitCost",
                        principalColumn: "BenefitCostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Dependents",
                columns: table => new
                {
                    DependentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BenefitCostId = table.Column<byte>(nullable: true),
                    EmployeeId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependents", x => x.DependentId);
                    table.ForeignKey(
                        name: "FK_Dependents_BenefitCost_BenefitCostId",
                        column: x => x.BenefitCostId,
                        principalTable: "BenefitCost",
                        principalColumn: "BenefitCostId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dependents_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BenefitCost",
                columns: new[] { "BenefitCostId", "Rate" },
                values: new object[] { (byte)1, 1000m });

            migrationBuilder.InsertData(
                table: "BenefitCost",
                columns: new[] { "BenefitCostId", "Rate" },
                values: new object[] { (byte)2, 500m });

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_BenefitCostId",
                table: "Dependents",
                column: "BenefitCostId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_EmployeeId",
                table: "Dependents",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BenefitCostId",
                table: "Employees",
                column: "BenefitCostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependents");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "BenefitCost");
        }
    }
}
