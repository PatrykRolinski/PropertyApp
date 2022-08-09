using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyApp.Infrastructure.Migrations
{
    public partial class changeUserOnDeleteBehaviourSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_CreatedById",
                table: "Properties");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_CreatedById",
                table: "Properties",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Users_CreatedById",
                table: "Properties");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Users_CreatedById",
                table: "Properties",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
