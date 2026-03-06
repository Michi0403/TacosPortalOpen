using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TacosPortal.Migrations
{
    /// <inheritdoc />
    public partial class newey3 : Migration
    {

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatToIgnore",
                table: "TelegramChats");

            migrationBuilder.CreateTable(
                name: "TelegramChatsToIgnore",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GCRecord = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelegramChatsToIgnore", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelegramChatsToIgnore_TelegramChats_ChatID",
                        column: x => x.ChatID,
                        principalTable: "TelegramChats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TelegramChatsToIgnore_ChatID",
                table: "TelegramChatsToIgnore",
                column: "ChatID");
        }
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelegramChatsToIgnore");

            migrationBuilder.AddColumn<bool>(
                name: "ChatToIgnore",
                table: "TelegramChats",
                type: "bit",
                nullable: true);
        }
    }
}
