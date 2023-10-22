using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class CreateOrixas : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "orixas",
            columns: table => new
            {
                id = table.Column<Guid>(
                    type: "uuid",
                    nullable: false
                ),

                name = table.Column<string>(
                    type: "varchar(50)",
                    nullable: false
                ),

                status = table.Column<string>(
                    type: "varchar(10)",
                    nullable: false,
                    defaultValue: "Active"
                ),

                created_at = table.Column<DateTime>(
                    type: "timestamp with time zone",
                    nullable: false
                ),
                updated_at = table.Column<DateTime>(
                    type: "timestamp with time zone",
                    nullable: true
                ),

                deleted_at = table.Column<DateTime>(
                    type: "timestamp with time zone",
                    nullable: true
                )
            },
            constraints: table =>
            {
                table.PrimaryKey(
                    name: "PK_orixas_id",
                    columns => columns.id
                );

                table.CheckConstraint(
                    name: "CK_orixas_status",
                    sql: "status IN ('Active', 'Inactive', 'Deleted')"
                );
            });

        migrationBuilder.CreateIndex(
            name: "IX_orixas_name",
            table: "orixas",
            column: "name",
            unique: true
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "orixas"
        );
    }
}
