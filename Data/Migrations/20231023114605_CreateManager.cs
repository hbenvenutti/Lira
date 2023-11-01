using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class CreateManager : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "managers",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                username = table.Column<string>(type: "varchar(50)", nullable: false),
                password = table.Column<string>(type: "varchar(50)", nullable: false),
                person_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<string>(type: "varchar(10)", nullable: false, defaultValue: "Active"),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_manager_id", x => x.id);
                table.ForeignKey(
                    name: "FK_managers_person_id",
                    column: x => x.person_id,
                    principalTable: "persons",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade
                );
            });

        migrationBuilder.CreateIndex(
            name: "IX_manager_person_id",
            table: "managers",
            column: "person_id",
            unique: true
        );

        migrationBuilder.CreateIndex(
            name: "IX_manager_username",
            table: "managers",
            column: "username",
            unique: true
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "managers"
        );
    }
}
