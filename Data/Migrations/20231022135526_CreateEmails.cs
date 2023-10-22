using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class CreateEmails : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "emails",
            columns: table => new
            {
                id = table.Column<Guid>(
                    type: "uuid",
                    nullable: false
                ),

                address = table.Column<string>(
                    type: "varchar(50)",
                    nullable: false
                ),

                type = table.Column<string>(
                    type: "varchar(10)",
                    nullable: false,
                    defaultValue: "Personal"
                ),

                person_id = table.Column<Guid>(
                    type: "uuid",
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
                table.PrimaryKey(name:"PK_email_id", x => x.id);

                table.CheckConstraint(
                    name: "CK_emails_status",
                    sql: "status IN ('Active', 'Inactive', 'Deleted')"
                );

                table.CheckConstraint(
                    name: "CK_emails_type",
                    sql: "type IN ('Personal', 'Corporate')"
                );

                table.ForeignKey(
                    name: "FK_emails_person_id",
                    column: x => x.person_id,
                    principalTable: "persons",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade
                );
            });

        migrationBuilder.CreateIndex(
            name: "IX_email_address",
            table: "emails",
            column: "address",
            unique: true
        );

        migrationBuilder.CreateIndex(
            name: "IX_emails_person_id",
            table: "emails",
            column: "person_id"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "emails");
    }
}
