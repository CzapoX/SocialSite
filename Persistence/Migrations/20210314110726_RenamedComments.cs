using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RenamedComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cooments_AspNetUsers_AuthorId",
                table: "Cooments");

            migrationBuilder.DropForeignKey(
                name: "FK_Cooments_Posts_PostId",
                table: "Cooments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cooments",
                table: "Cooments");

            migrationBuilder.RenameTable(
                name: "Cooments",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Cooments_PostId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Cooments_AuthorId",
                table: "Comments",
                newName: "IX_Comments_AuthorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AuthorId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Cooments");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "Cooments",
                newName: "IX_Cooments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorId",
                table: "Cooments",
                newName: "IX_Cooments_AuthorId");

            migrationBuilder.AlterColumn<Guid>(
                name: "PostId",
                table: "Cooments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cooments",
                table: "Cooments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cooments_AspNetUsers_AuthorId",
                table: "Cooments",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cooments_Posts_PostId",
                table: "Cooments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
