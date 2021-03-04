using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class initialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMPANIES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommercialName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    CPNJ = table.Column<string>(type: "char(14)", unicode: false, fixedLength: true, maxLength: 14, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SUPPLIERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonResponsible = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CNPJ_CPF = table.Column<string>(type: "char(14)", unicode: false, fixedLength: true, maxLength: 14, nullable: false),
                    RG = table.Column<string>(type: "char(14)", unicode: false, fixedLength: true, maxLength: 14, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    DateRegistration = table.Column<DateTime>(type: "DATETIME2", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Telephone2 = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPPLIERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CompanySupplier",
                columns: table => new
                {
                    CompaniesID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanySupplier", x => new { x.CompaniesID, x.SupplierID });
                    table.ForeignKey(
                        name: "FK_CompanySupplier_COMPANIES_CompaniesID",
                        column: x => x.CompaniesID,
                        principalTable: "COMPANIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanySupplier_SUPPLIERS_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "SUPPLIERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_COMPANIES_CNPJ",
                table: "COMPANIES",
                column: "CPNJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanySupplier_SupplierID",
                table: "CompanySupplier",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "UQ_SUPPLIERS_CNPJ_CPF",
                table: "SUPPLIERS",
                column: "CNPJ_CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_SUPPLIERS_RG",
                table: "SUPPLIERS",
                column: "RG",
                unique: true,
                filter: "[RG] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanySupplier");

            migrationBuilder.DropTable(
                name: "COMPANIES");

            migrationBuilder.DropTable(
                name: "SUPPLIERS");
        }
    }
}
