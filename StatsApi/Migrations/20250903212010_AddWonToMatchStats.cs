using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StatsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddWonToMatchStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Won",
                table: "MatchStats",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Won",
                table: "MatchStats");
        }
    }
}
