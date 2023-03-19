using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecentReads.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Books_BookId1",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_BookId1",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "Ratings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId1",
                table: "Ratings",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BookId1",
                table: "Ratings",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Books_BookId1",
                table: "Ratings",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
