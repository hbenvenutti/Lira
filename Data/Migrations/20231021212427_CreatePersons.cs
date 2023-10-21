using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    cpf = table.Column<string>(type: "varchar(11)", nullable: false),
                    name = table.Column<string>(type: "varchar(30)", nullable: false),
                    surname = table.Column<string>(type: "varchar(30)", nullable: false),
                    status = table.Column<string>(type: "varchar", nullable: false, defaultValue: "Active"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_persons_id", x => x.id);
                    table.CheckConstraint("CK_persons_status", "status IN ('Active', 'Inactive', 'Deleted')");
                });

            migrationBuilder.CreateIndex(
                name: "IX_persons_cpf",
                table: "persons",
                column: "cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "persons");
        }
    }
}
