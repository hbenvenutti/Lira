using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class AlteredOrixasConstraint : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropCheckConstraint(
            name: "CK_person_orixas_type",
            table: "person_orixas");

        migrationBuilder.AddCheckConstraint(
            name: "CK_person_orixas_type",
            table: "person_orixas",
            sql: "type IN ('Front', 'Ancestral', 'Adjunct')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropCheckConstraint(
            name: "CK_person_orixas_type",
            table: "person_orixas");

        migrationBuilder.AddCheckConstraint(
            name: "CK_person_orixas_type",
            table: "person_orixas",
            sql: "type IN ('Front', 'Ancestor', 'Close')");
    }
}
