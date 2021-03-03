using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RelationBetweenUserAndPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostOwnerId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostOwnerId",
                table: "Posts",
                column: "PostOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_PostOwnerId",
                table: "Posts",
                column: "PostOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_PostOwnerId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostOwnerId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostOwnerId",
                table: "Posts");
        }
    }
}
