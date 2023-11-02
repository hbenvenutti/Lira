using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class ChangedStatusMaxLength : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "status",
            table: "persons",
            type: "varchar(10)",
            nullable: false,
            defaultValue: "Active",
            oldClrType: typeof(string),
            oldType: "varchar",
            oldDefaultValue: "Active"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "status",
            table: "persons",
            type: "varchar",
            nullable: false,
            defaultValue: "Active",
            oldClrType: typeof(string),
            oldType: "varchar(10)",
            oldDefaultValue: "Active"
        );
    }
}
