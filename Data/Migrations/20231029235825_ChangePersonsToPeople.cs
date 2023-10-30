using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class ChangePersonsToPeople : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_persons_id",
            table: "persons");

        migrationBuilder.DropCheckConstraint(
            name: "CK_persons_status",
            table: "persons");

        migrationBuilder.RenameTable(
            name: "persons",
            newName: "people");

        migrationBuilder.RenameIndex(
            name: "IX_persons_cpf",
            table: "people",
            newName: "IX_people_cpf");

        migrationBuilder.AddPrimaryKey(
            name: "PK_people_id",
            table: "people",
            column: "id");

        migrationBuilder.AddCheckConstraint(
            name: "CK_people_status",
            table: "people",
            sql: "status IN ('Active', 'Inactive', 'Deleted')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_people_id",
            table: "people");

        migrationBuilder.DropCheckConstraint(
            name: "CK_people_status",
            table: "people");

        migrationBuilder.RenameTable(
            name: "people",
            newName: "persons");

        migrationBuilder.RenameIndex(
            name: "IX_people_cpf",
            table: "persons",
            newName: "IX_persons_cpf");

        migrationBuilder.AddPrimaryKey(
            name: "PK_persons_id",
            table: "persons",
            column: "id");

        migrationBuilder.AddCheckConstraint(
            name: "CK_persons_status",
            table: "persons",
            sql: "status IN ('Active', 'Inactive', 'Deleted')");
    }
}
