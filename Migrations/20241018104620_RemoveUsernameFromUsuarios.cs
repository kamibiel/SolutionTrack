using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolutionTrack.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUsernameFromUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
        name: "Username",
        table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
