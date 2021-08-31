using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BSB.Data.Migrations
{
    public partial class CommentsOnPostUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentInPost_Comments_CommentId",
                table: "CommentInPost");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentInPost_Posts_PostId",
                table: "CommentInPost");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentInUser_Comments_CommentId",
                table: "CommentInUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentInUser_AspNetUsers_UserId",
                table: "CommentInUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentInUser",
                table: "CommentInUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentInPost",
                table: "CommentInPost");

            migrationBuilder.DropIndex(
                name: "IX_CommentInPost_CommentId",
                table: "CommentInPost");

            migrationBuilder.DropIndex(
                name: "IX_CommentInPost_PostId",
                table: "CommentInPost");

            migrationBuilder.RenameTable(
                name: "CommentInUser",
                newName: "CommentInUsers");

            migrationBuilder.RenameTable(
                name: "CommentInPost",
                newName: "CommentInPosts");

            migrationBuilder.RenameIndex(
                name: "IX_CommentInUser_UserId",
                table: "CommentInUsers",
                newName: "IX_CommentInUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentInUser_CommentId",
                table: "CommentInUsers",
                newName: "IX_CommentInUsers_CommentId");

            migrationBuilder.AddColumn<Guid>(
                name: "CommentId1",
                table: "CommentInPosts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentInUsers",
                table: "CommentInUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentInPosts",
                table: "CommentInPosts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInPosts_CommentId",
                table: "CommentInPosts",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInPosts_CommentId1",
                table: "CommentInPosts",
                column: "CommentId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInPosts_Posts_CommentId",
                table: "CommentInPosts",
                column: "CommentId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInPosts_Comments_CommentId1",
                table: "CommentInPosts",
                column: "CommentId1",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInUsers_Comments_CommentId",
                table: "CommentInUsers",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInUsers_AspNetUsers_UserId",
                table: "CommentInUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentInPosts_Posts_CommentId",
                table: "CommentInPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentInPosts_Comments_CommentId1",
                table: "CommentInPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentInUsers_Comments_CommentId",
                table: "CommentInUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentInUsers_AspNetUsers_UserId",
                table: "CommentInUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentInUsers",
                table: "CommentInUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentInPosts",
                table: "CommentInPosts");

            migrationBuilder.DropIndex(
                name: "IX_CommentInPosts_CommentId",
                table: "CommentInPosts");

            migrationBuilder.DropIndex(
                name: "IX_CommentInPosts_CommentId1",
                table: "CommentInPosts");

            migrationBuilder.DropColumn(
                name: "CommentId1",
                table: "CommentInPosts");

            migrationBuilder.RenameTable(
                name: "CommentInUsers",
                newName: "CommentInUser");

            migrationBuilder.RenameTable(
                name: "CommentInPosts",
                newName: "CommentInPost");

            migrationBuilder.RenameIndex(
                name: "IX_CommentInUsers_UserId",
                table: "CommentInUser",
                newName: "IX_CommentInUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CommentInUsers_CommentId",
                table: "CommentInUser",
                newName: "IX_CommentInUser_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentInUser",
                table: "CommentInUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentInPost",
                table: "CommentInPost",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInPost_CommentId",
                table: "CommentInPost",
                column: "CommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentInPost_PostId",
                table: "CommentInPost",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInPost_Comments_CommentId",
                table: "CommentInPost",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInPost_Posts_PostId",
                table: "CommentInPost",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInUser_Comments_CommentId",
                table: "CommentInUser",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentInUser_AspNetUsers_UserId",
                table: "CommentInUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
