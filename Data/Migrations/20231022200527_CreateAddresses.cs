using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class CreateAddresses : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "addresses",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                street = table.Column<string>(type: "varchar(50)", nullable: false),
                number = table.Column<string>(type: "varchar(10)", nullable: false),
                neighborhood = table.Column<string>(type: "varchar(50)", nullable: false),
                city = table.Column<string>(type: "varchar(50)", nullable: false),
                state = table.Column<string>(type: "varchar(2)", nullable: false),
                zip_code = table.Column<string>(type: "varchar(8)", nullable: false),
                complement = table.Column<string>(type: "varchar(50)", nullable: true),
                person_id = table.Column<Guid>(type: "uuid", nullable: false),
                status = table.Column<string>(type: "varchar(10)", nullable: false, defaultValue: "Active"),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey(
                    name: "PK_address_id",
                    columns: columns => columns.id
                );

                table.CheckConstraint(
                    name: "CK_addresses_state",
                    sql:
                    "state IN ('AC', 'AL', 'AP', 'AM', 'BA', 'CE', 'DF', 'ES', 'GO', 'MA', 'MT', 'MS', 'MG', 'PA', 'PB', 'PR', 'PE', 'PI', 'RJ', 'RN', 'RS', 'RO', 'RR', 'SC', 'SP', 'SE', 'TO')"
                );

                table.CheckConstraint(
                    name: "CK_addresses_status",
                    sql: "status IN ('Active', 'Inactive', 'Deleted')"
                );

                table.ForeignKey(
                    name: "FK_addresses_person_id",
                    column: columns => columns.person_id,
                    principalTable: "persons",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade
                );
            });

        migrationBuilder.CreateIndex(
            name: "IX_addresses_person_id",
            table: "addresses",
            column: "person_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "addresses");
    }
}
