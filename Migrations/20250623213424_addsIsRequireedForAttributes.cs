using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BraveHeartBackend.Migrations
{
    /// <inheritdoc />
    public partial class addsIsRequireedForAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "ProductAttributes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "ProductAttributes");
        }
    }
}
