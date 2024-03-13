using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfigApi.Migrations
{
    /// <inheritdoc />
    public partial class VersioNSetting2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Settings_SettingId",
                table: "Versions");

            migrationBuilder.AlterColumn<int>(
                name: "SettingId",
                table: "Versions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Settings_SettingId",
                table: "Versions",
                column: "SettingId",
                principalTable: "Settings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Settings_SettingId",
                table: "Versions");

            migrationBuilder.AlterColumn<int>(
                name: "SettingId",
                table: "Versions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Settings_SettingId",
                table: "Versions",
                column: "SettingId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
