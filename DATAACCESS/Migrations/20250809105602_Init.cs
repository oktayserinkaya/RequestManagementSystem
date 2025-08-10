using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DATAACCESS.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Birthdate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    HasFirstPasswordChanged = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Name", "NormalizedName", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 8, 9, 13, 56, 0, 902, DateTimeKind.Local).AddTicks(8072), null, "Admin", "ADMIN", 1, null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 8, 9, 13, 56, 0, 902, DateTimeKind.Local).AddTicks(8091), null, "TalepOluşturanBirim", "TALEPOLUSTURANBIRIM", 1, null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 8, 9, 13, 56, 0, 902, DateTimeKind.Local).AddTicks(8093), null, "IhtiyacTespitKomisyonu", "IHTIYACTESPITKOMISYONU", 1, null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 8, 9, 13, 56, 0, 902, DateTimeKind.Local).AddTicks(8095), null, "SatinAlmaBirimi", "SATINALMABIRIMI", 1, null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(2025, 8, 9, 13, 56, 0, 902, DateTimeKind.Local).AddTicks(8096), null, "DepoBirimi", "DEPOBIRIMI", 1, null },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), null, new DateTime(2025, 8, 9, 13, 56, 0, 902, DateTimeKind.Local).AddTicks(8098), null, "OdemeBirimi", "ODEMEBIRIMI", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthdate", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "FirstName", "HasFirstPasswordChanged", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 0, new DateOnly(1, 1, 1), "b6a73de0-949b-40fc-914d-acf50fe44734", new DateTime(2025, 8, 9, 13, 56, 0, 902, DateTimeKind.Local).AddTicks(8301), null, "admin@example.com", true, "Sistem", true, "Yöneticisi", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEFzFPil4D/3C3IS2wjTN42aWVYINIDr9j7cmUpI/AaHxqoTQH0DA0e/wR08bR/uQDA==", null, false, "414aa0f9-a871-47e7-b1dd-e6bfa650e7d1", 1, false, null, "admin" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0, new DateOnly(1, 1, 1), "34fcc773-9672-4692-aae5-df30bafc0dd0", new DateTime(2025, 8, 9, 13, 56, 0, 960, DateTimeKind.Local).AddTicks(8011), null, "ahmet.yilmaz@example.com", true, "Ahmet", true, "Yılmaz", false, null, "AHMET.YILMAZ@EXAMPLE.COM", "AHMETYILMAZ", "AQAAAAIAAYagAAAAEMYebLVaKiCAodjXs4rzQZEc0ztOlsYmPc4WA51LA+kvNfBqMpAxOBp9tvGP3IJ8GA==", null, false, "08c925c0-4306-4548-a864-4c0bb7b11801", 1, false, null, "ahmetyilmaz" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0, new DateOnly(1, 1, 1), "e5ee6961-b0af-4ff4-9885-c1fe074f6196", new DateTime(2025, 8, 9, 13, 56, 1, 20, DateTimeKind.Local).AddTicks(1481), null, "elif.kara@example.com", true, "Elif", true, "Kara", false, null, "ELIF.KARA@EXAMPLE.COM", "ELIFKARA", "AQAAAAIAAYagAAAAEKg9DFKHm5FqSlQH/981snxP1BTcUIqiEpBcUD6OCPLDfcrkEvP7eH/SvQeV2cRAjw==", null, false, "1a0dbede-d276-4012-8e9c-404e3e7408a6", 1, false, null, "elifkara" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 0, new DateOnly(1, 1, 1), "a3cb1f8c-e025-4c43-b75b-e2c9d3f2f3fe", new DateTime(2025, 8, 9, 13, 56, 1, 74, DateTimeKind.Local).AddTicks(9597), null, "mehmet.demir@example.com", true, "Mehmet", true, "Demir", false, null, "MEHMET.DEMIR@EXAMPLE.COM", "MEHMETDEMIR", "AQAAAAIAAYagAAAAEMIAXj0eZY71i/RZiM90ni6ISOWVxHoU42ZhVwrPJSOo2lx8G1+zYI0GSnSyMpENhA==", null, false, "13308523-215e-4d5d-98f1-2d1729a39a5e", 1, false, null, "mehmetdemir" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 0, new DateOnly(1, 1, 1), "e896f240-df63-47fc-b031-4b15e0b2510d", new DateTime(2025, 8, 9, 13, 56, 1, 138, DateTimeKind.Local).AddTicks(2849), null, "zeynep.sahin@example.com", true, "Zeynep", true, "Şahin", false, null, "ZEYNEP.SAHIN@EXAMPLE.COM", "ZEYNEPSAHIN", "AQAAAAIAAYagAAAAELD2dpqT4ZBTkhsZy9QLUQALqKJkecksy3UFlmY1S2TCeaYIfrd4T2TSj4pqnzzAfQ==", null, false, "c6340de6-a609-4b20-85c2-a1dd808b218a", 1, false, null, "zeynepsahin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
