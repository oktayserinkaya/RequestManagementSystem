using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DATAACCESS.Migrations
{
    /// <inheritdoc />
    public partial class Initil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AppUserId", "CreatedDate" },
                values: new object[] { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 8, 6, 17, 45, 55, 161, DateTimeKind.Local).AddTicks(1148) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AppUserId", "CreatedDate" },
                values: new object[] { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 8, 6, 17, 45, 55, 161, DateTimeKind.Local).AddTicks(1156) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AppUserId", "CreatedDate" },
                values: new object[] { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 8, 6, 17, 45, 55, 161, DateTimeKind.Local).AddTicks(1162) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "AppUserId", "CreatedDate" },
                values: new object[] { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2025, 8, 6, 17, 45, 55, 161, DateTimeKind.Local).AddTicks(1166) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "AppUserId", "CreatedDate" },
                values: new object[] { new Guid("7ddc2e8c-e258-4bba-8cc6-3b7e1cffc42c"), new DateTime(2025, 8, 2, 17, 44, 36, 105, DateTimeKind.Local).AddTicks(9121) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "AppUserId", "CreatedDate" },
                values: new object[] { new Guid("b09651ff-1e53-44ab-bede-e64e35256995"), new DateTime(2025, 8, 2, 17, 44, 36, 105, DateTimeKind.Local).AddTicks(9149) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "AppUserId", "CreatedDate" },
                values: new object[] { new Guid("44a182e1-39a4-418b-b1cd-9d465597b629"), new DateTime(2025, 8, 2, 17, 44, 36, 105, DateTimeKind.Local).AddTicks(9153) });

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "AppUserId", "CreatedDate" },
                values: new object[] { new Guid("af1d86fb-4dba-4b97-9939-15562e6fa3c7"), new DateTime(2025, 8, 2, 17, 44, 36, 105, DateTimeKind.Local).AddTicks(9156) });
        }
    }
}
