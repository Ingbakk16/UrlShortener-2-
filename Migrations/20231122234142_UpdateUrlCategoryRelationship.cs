using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener_2_.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUrlCategoryRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "NewUrls",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NewUrls_CategoryId",
                table: "NewUrls",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_NewUrls_Categories_CategoryId",
                table: "NewUrls",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewUrls_Categories_CategoryId",
                table: "NewUrls");

            migrationBuilder.DropIndex(
                name: "IX_NewUrls_CategoryId",
                table: "NewUrls");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "NewUrls");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");
        }
    }
}
