﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommandService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "platforms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_platforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "commands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HowTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommandLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_commands_platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_commands_PlatformId",
                table: "commands",
                column: "PlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commands");

            migrationBuilder.DropTable(
                name: "platforms");
        }
    }
}
