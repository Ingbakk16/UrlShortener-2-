using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener_2_.Migrations
{
    /// <inheritdoc />
    public partial class LimitOfConvertionsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainingShortUrls",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingShortUrls",
                table: "Users");
        }
    }
}
