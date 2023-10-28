using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class SeedOrixas : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "orixas",
            columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
            values: new object[,]
            {
                { new Guid("111f4c7e-031e-4479-a593-b767b001f6c1"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6456), null, "Oxumaré", null },
                { new Guid("264a53d8-5b3f-407a-9212-61d9d6b6ea92"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6275), null, "Oxum", null },
                { new Guid("2a2c511d-8657-40fc-bfdf-883634393bd5"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6264), null, "Xangô", null },
                { new Guid("3b9af91e-cb36-4311-9ba4-edfad1084fd7"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6323), null, "Obaluaiê", null },
                { new Guid("4458452e-53a2-44a5-8f76-fa709b1e42d0"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6332), null, "Nanã", null },
                { new Guid("6b3e58c9-ecd1-4dce-b20f-075be1a11a6f"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6437), null, "Oxalá", null },
                { new Guid("7c3c483f-d24e-41a6-9d9a-f93b0406eb2c"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6313), null, "Egunitá", null },
                { new Guid("8ecad1d6-81a8-4a55-8b13-6200bc310caa"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6304), null, "Ogum", null },
                { new Guid("b6f808ee-0c0d-4785-ac54-b5047a7d2205"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6466), null, "Obá", null },
                { new Guid("ce781056-6a2a-4396-9ee6-c027fa9daf17"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6285), null, "Oxóssi", null },
                { new Guid("d004f815-142d-4c3e-94cf-8e2bc84a95e9"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6447), null, "Oyá Tempo", null },
                { new Guid("d92459ab-fb0e-4fc9-a823-3b20f0482a16"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6475), null, "Omolu", null },
                { new Guid("e590088d-1a32-4a2b-8144-54351d9c7f40"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6294), null, "Iansã", null },
                { new Guid("e845de4f-ccb5-4d02-9cf7-384afb9803dd"), new DateTime(2023, 10, 28, 17, 3, 18, 840, DateTimeKind.Utc).AddTicks(6341), null, "Iemanjá", null }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("111f4c7e-031e-4479-a593-b767b001f6c1"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("264a53d8-5b3f-407a-9212-61d9d6b6ea92"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("2a2c511d-8657-40fc-bfdf-883634393bd5"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("3b9af91e-cb36-4311-9ba4-edfad1084fd7"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("4458452e-53a2-44a5-8f76-fa709b1e42d0"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("6b3e58c9-ecd1-4dce-b20f-075be1a11a6f"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("7c3c483f-d24e-41a6-9d9a-f93b0406eb2c"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("8ecad1d6-81a8-4a55-8b13-6200bc310caa"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("b6f808ee-0c0d-4785-ac54-b5047a7d2205"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("ce781056-6a2a-4396-9ee6-c027fa9daf17"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("d004f815-142d-4c3e-94cf-8e2bc84a95e9"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("d92459ab-fb0e-4fc9-a823-3b20f0482a16"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("e590088d-1a32-4a2b-8144-54351d9c7f40"));

        migrationBuilder.DeleteData(
            table: "orixas",
            keyColumn: "id",
            keyValue: new Guid("e845de4f-ccb5-4d02-9cf7-384afb9803dd"));
    }
}
