using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class CreateMediums : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "mediums",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                first_amaci = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                last_amaci = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                person_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<string>(type: "varchar(10)", nullable: false, defaultValue: "Active"),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_medium_id", x => x.id);
                table.CheckConstraint("CK_mediums_status", "status IN ('Active', 'Inactive', 'Deleted')");
                table.ForeignKey(
                    name: "FK_mediums_person_id",
                    column: x => x.person_id,
                    principalTable: "persons",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_medium_person_id",
            table: "mediums",
            column: "person_id",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "mediums");
    }
}
