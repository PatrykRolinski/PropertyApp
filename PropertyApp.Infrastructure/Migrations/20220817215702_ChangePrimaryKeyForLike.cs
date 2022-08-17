using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyApp.Infrastructure.Migrations
{
    public partial class ChangePrimaryKeyForLike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LikedProperties",
                table: "LikedProperties");

            migrationBuilder.DropIndex(
                name: "IX_LikedProperties_UserId",
                table: "LikedProperties");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LikedProperties");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikedProperties",
                table: "LikedProperties",
                columns: new[] { "UserId", "PropertyId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LikedProperties",
                table: "LikedProperties");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "LikedProperties",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LikedProperties",
                table: "LikedProperties",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LikedProperties_UserId",
                table: "LikedProperties",
                column: "UserId");
        }
    }
}
