using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class CreatePersonOrixas : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "person_orixas",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<string>(type: "varchar(10)", nullable: false),
                person_id = table.Column<Guid>(type: "uuid", nullable: false),
                orixa_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<string>(type: "varchar(10)", nullable: false, defaultValue: "Active"),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_person_orixas_id", x => x.id);
                table.CheckConstraint("CK_person_orixas_status", "status IN ('Active', 'Inactive', 'Deleted')");
                table.CheckConstraint("CK_person_orixas_type", "type IN ('Front', 'Ancestor', 'Close')");
                table.ForeignKey(
                    name: "FK_person_orixas_orixa_id",
                    column: x => x.orixa_id,
                    principalTable: "orixas",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_person_orixas_person_id",
                    column: x => x.person_id,
                    principalTable: "persons",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_person_orixas_orixa_id",
            table: "person_orixas",
            column: "orixa_id");

        migrationBuilder.CreateIndex(
            name: "IX_person_orixas_person_id",
            table: "person_orixas",
            column: "person_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "person_orixas");
    }
}
