using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TranaltionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserInfo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserInfoTranslations",
                columns: table => new
                {
                    TranslationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TranslatedInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfoTranslations", x => x.TranslationId);
                    table.ForeignKey(
                        name: "FK_UserInfoTranslations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserInfoTranslations_UserId",
                table: "UserInfoTranslations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfoTranslations");

            migrationBuilder.DropColumn(
                name: "UserInfo",
                table: "Users");
        }
    }
}
