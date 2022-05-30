using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PSP.Dal.Migrations
{
    public partial class deneme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                table: "ProjectInteraction",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInteraction_UserProfileId",
                table: "ProjectInteraction",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectInteraction_UserProfiles_UserProfileId",
                table: "ProjectInteraction",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectInteraction_UserProfiles_UserProfileId",
                table: "ProjectInteraction");

            migrationBuilder.DropIndex(
                name: "IX_ProjectInteraction_UserProfileId",
                table: "ProjectInteraction");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "ProjectInteraction");
        }
    }
}
