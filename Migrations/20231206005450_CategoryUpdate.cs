using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener_2_.Migrations
{
    /// <inheritdoc />
    public partial class CategoryUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "NewUrls",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewUrls_CategoryId1",
                table: "NewUrls",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NewUrls_Categories_CategoryId1",
                table: "NewUrls",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewUrls_Categories_CategoryId1",
                table: "NewUrls");

            migrationBuilder.DropIndex(
                name: "IX_NewUrls_CategoryId1",
                table: "NewUrls");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "NewUrls");
        }
    }
}
