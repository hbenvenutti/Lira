using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class AlteredManagerPassword : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "password",
            table: "managers",
            type: "varchar(100)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(50)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "password",
            table: "managers",
            type: "varchar(50)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(100)");
    }
}
