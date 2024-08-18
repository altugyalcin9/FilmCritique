using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmCritique.Entities.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReview_Movies_MovieId",
                table: "UserReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReview",
                table: "UserReview");

            migrationBuilder.RenameTable(
                name: "UserReview",
                newName: "UserReviews");

            migrationBuilder.RenameIndex(
                name: "IX_UserReview_MovieId",
                table: "UserReviews",
                newName: "IX_UserReviews_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReviews",
                table: "UserReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviews_Movies_MovieId",
                table: "UserReviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserReviews_Movies_MovieId",
                table: "UserReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserReviews",
                table: "UserReviews");

            migrationBuilder.RenameTable(
                name: "UserReviews",
                newName: "UserReview");

            migrationBuilder.RenameIndex(
                name: "IX_UserReviews_MovieId",
                table: "UserReview",
                newName: "IX_UserReview_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserReview",
                table: "UserReview",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReview_Movies_MovieId",
                table: "UserReview",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
