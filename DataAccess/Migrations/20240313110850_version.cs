using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class version : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Settings",
                newName: "ServiceVersionId");

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ServiceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Version = table.Column<string>(type: "TEXT", nullable: false),
                    ServiceConfiguration = table.Column<byte>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Versions_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_ServiceVersionId",
                table: "Settings",
                column: "ServiceVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_ServiceId",
                table: "Versions",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_Versions_ServiceVersionId",
                table: "Settings",
                column: "ServiceVersionId",
                principalTable: "Versions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_Versions_ServiceVersionId",
                table: "Settings");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropIndex(
                name: "IX_Settings_ServiceVersionId",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "ServiceVersionId",
                table: "Settings",
                newName: "ServiceId");

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Services",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
