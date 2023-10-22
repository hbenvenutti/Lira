using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class CreatePhones : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "phones",
            columns: table => new
            {
                id = table.Column<Guid>(
                    type: "uuid",
                    nullable: false
                ),

                number = table.Column<string>(
                    type: "varchar(9)",
                    nullable: false
                ),

                ddd = table.Column<string>(
                    type: "varchar(2)",
                    nullable: false
                ),

                person_id = table.Column<Guid>(
                    type: "uuid",
                    nullable: false
                ),

                status = table.Column<string>(
                    type: "varchar(10)",
                    nullable: false, defaultValue: "Active"
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
                    name: "PK_phone_id",
                    columns: columns => columns.id
                );

                table.CheckConstraint(
                    name: "CK_phones_status",
                    sql: "status IN ('Active', 'Inactive', 'Deleted')"
                );

                table.ForeignKey(
                    name: "FK_phones_person_id",
                    column: columns => columns.person_id,
                    principalTable: "persons",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade
                );
            });

        migrationBuilder.CreateIndex(
            name: "IX_phones_person_id",
            table: "phones",
            column: "person_id"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "phones"
        );
    }
}
