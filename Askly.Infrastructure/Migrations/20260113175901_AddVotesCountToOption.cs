using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Askly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVotesCountToOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VotesCount",
                table: "Options",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VotesCount",
                table: "Options");
        }
    }
}
