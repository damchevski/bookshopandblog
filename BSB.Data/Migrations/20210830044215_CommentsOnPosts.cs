using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BSB.Data.Migrations
{
    public partial class CommentsOnPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_ForPostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ForPostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ForPostId",
                table: "Comments");

            migrationBuilder.CreateTable(
                name: "CommentInPost",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CommentId = table.Column<Guid>(nullable: false),
                    PostId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentInPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentInPost_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentInPost_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentInUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CommentId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentInUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentInUser_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentInUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentInPost_CommentId",
                table: "CommentInPost",
                column: "CommentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentInPost_PostId",
                table: "CommentInPost",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInUser_CommentId",
                table: "CommentInUser",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInUser_UserId",
                table: "CommentInUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentInPost");

            migrationBuilder.DropTable(
                name: "CommentInUser");

            migrationBuilder.AddColumn<Guid>(
                name: "ForPostId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ForPostId",
                table: "Comments",
                column: "ForPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_ForPostId",
                table: "Comments",
                column: "ForPostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
