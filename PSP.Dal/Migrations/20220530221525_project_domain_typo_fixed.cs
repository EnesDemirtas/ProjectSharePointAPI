using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSP.Dal.Migrations
{
    public partial class project_domain_typo_fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Projects",
                newName: "DateCreated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Projects",
                newName: "CreatedDate");
        }
    }
}
