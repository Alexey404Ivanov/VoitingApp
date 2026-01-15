using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Askly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVoteEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    PollId = table.Column<Guid>(type: "uuid", nullable: false),
                    OptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnonUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => new { x.PollId, x.OptionId, x.AnonUserId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PollId_AnonUserId",
                table: "Votes",
                columns: new[] { "PollId", "AnonUserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");
        }
    }
}
