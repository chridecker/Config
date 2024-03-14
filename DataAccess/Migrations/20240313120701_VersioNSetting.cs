using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class VersioNSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Versions_ServiceVersionId",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "ServiceVersionId",
                table: "Settings",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings_ServiceVersionId",
                table: "Settings",
                newName: "IX_Settings_ServiceId");

            migrationBuilder.AddColumn<int>(
                name: "SettingId",
                table: "Versions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Versions_SettingId",
                table: "Versions",
                column: "SettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Services_ServiceId",
                table: "Settings",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Versions_Settings_SettingId",
                table: "Versions",
                column: "SettingId",
                principalTable: "Settings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Services_ServiceId",
                table: "Settings");

            migrationBuilder.DropForeignKey(
                name: "FK_Versions_Settings_SettingId",
                table: "Versions");

            migrationBuilder.DropIndex(
                name: "IX_Versions_SettingId",
                table: "Versions");

            migrationBuilder.DropColumn(
                name: "SettingId",
                table: "Versions");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Settings",
                newName: "ServiceVersionId");

            migrationBuilder.RenameIndex(
                name: "IX_Settings_ServiceId",
                table: "Settings",
                newName: "IX_Settings_ServiceVersionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Versions_ServiceVersionId",
                table: "Settings",
                column: "ServiceVersionId",
                principalTable: "Versions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
