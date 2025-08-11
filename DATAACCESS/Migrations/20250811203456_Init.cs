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
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 8, 11, 23, 34, 54, 908, DateTimeKind.Local).AddTicks(3475), null, "Admin", "ADMIN", 1, null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 8, 11, 23, 34, 54, 908, DateTimeKind.Local).AddTicks(3505), null, "TalepOluşturanBirim", "TALEPOLUSTURANBIRIM", 1, null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 8, 11, 23, 34, 54, 908, DateTimeKind.Local).AddTicks(3507), null, "IhtiyacTespitKomisyonu", "IHTIYACTESPITKOMISYONU", 1, null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 8, 11, 23, 34, 54, 908, DateTimeKind.Local).AddTicks(3509), null, "SatinAlmaBirimi", "SATINALMABIRIMI", 1, null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(2025, 8, 11, 23, 34, 54, 908, DateTimeKind.Local).AddTicks(3511), null, "DepoBirimi", "DEPOBIRIMI", 1, null },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), null, new DateTime(2025, 8, 11, 23, 34, 54, 908, DateTimeKind.Local).AddTicks(3513), null, "OdemeBirimi", "ODEMEBIRIMI", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthdate", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "FirstName", "HasFirstPasswordChanged", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("99999999-9999-9999-9999-999999999999"), 0, new DateOnly(1, 1, 1), "66b1cb20-0363-4773-95cc-09f94f07f525", new DateTime(2025, 8, 11, 23, 34, 55, 209, DateTimeKind.Local).AddTicks(2423), null, "fatma.oz@example.com", true, "Fatma", true, "Öz", false, null, "FATMA.OZ@EXAMPLE.COM", "FATMAOZ", "AQAAAAIAAYagAAAAEKmOZi/GPTU5Srs21KWo3wfyonoYdhIzz0VkKhsFw3/Atmo/6GFCw18lHIkVM6eCIA==", null, false, "33cc7f09-8d93-4938-bc81-79fda2bd9a16", 1, false, null, "fatmaoz" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 0, new DateOnly(1, 1, 1), "76636936-00f3-4376-a94d-0ce9f1bb6dcd", new DateTime(2025, 8, 11, 23, 34, 54, 908, DateTimeKind.Local).AddTicks(3800), null, "admin@example.com", true, "Sistem", true, "Yöneticisi", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEN0f1e0P38PaFXvygL6mG0qtRVR/8EEbQoAYjO9dMxitSyjEIIxQTL6qo2ZKJaseZw==", null, false, "7bc9e953-6d45-41cb-a14d-7ddeb3d01afe", 1, false, null, "admin" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0, new DateOnly(1, 1, 1), "0bc44677-6ebf-424a-be3c-3be782a3cbce", new DateTime(2025, 8, 11, 23, 34, 54, 968, DateTimeKind.Local).AddTicks(7419), null, "ahmet.yilmaz@example.com", true, "Ahmet", true, "Yılmaz", false, null, "AHMET.YILMAZ@EXAMPLE.COM", "AHMETYILMAZ", "AQAAAAIAAYagAAAAECN0sZ1RKQWm7QHOnB8veEWR09r62u9GmX2J92/4FwTOGdY8q84VUoHxr1aLB61D4Q==", null, false, "1a01df05-02c0-42fb-b148-1ad4a6776e24", 1, false, null, "ahmetyilmaz" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0, new DateOnly(1, 1, 1), "5e94555f-dcb1-4fd3-bc48-0a4cb71b74f3", new DateTime(2025, 8, 11, 23, 34, 55, 26, DateTimeKind.Local).AddTicks(7400), null, "elif.kara@example.com", true, "Elif", true, "Kara", false, null, "ELIF.KARA@EXAMPLE.COM", "ELIFKARA", "AQAAAAIAAYagAAAAEHV9bRmeoOCsaPG7gvrM9cjPXzxFE9btp70NPDfGvpFt/z812oQm38ytbDCnd9lM9Q==", null, false, "da1244b2-c887-4dae-9653-47b431295ec7", 1, false, null, "elifkara" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 0, new DateOnly(1, 1, 1), "a32d84aa-18c8-4794-9ded-7b485252bf6b", new DateTime(2025, 8, 11, 23, 34, 55, 86, DateTimeKind.Local).AddTicks(8401), null, "mehmet.demir@example.com", true, "Mehmet", true, "Demir", false, null, "MEHMET.DEMIR@EXAMPLE.COM", "MEHMETDEMIR", "AQAAAAIAAYagAAAAEF5zyzMhRDHOhVnEWPV0ib7qMlksUUDMYfLe4fTcJidg+ZHal1kzDuNYEYWW70gtig==", null, false, "5114e2bf-1acc-43e7-bb5d-7532536f4760", 1, false, null, "mehmetdemir" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 0, new DateOnly(1, 1, 1), "0b592fe1-b660-445e-a88d-8622df5bbf18", new DateTime(2025, 8, 11, 23, 34, 55, 147, DateTimeKind.Local).AddTicks(668), null, "zeynep.sahin@example.com", true, "Zeynep", true, "Şahin", false, null, "ZEYNEP.SAHIN@EXAMPLE.COM", "ZEYNEPSAHIN", "AQAAAAIAAYagAAAAEARhYv1ob5SBuDv3dK42pyeXb7AGeKaoIy70VuDNZPkUvodfI+bYSqC7WWP4668OLw==", null, false, "d7150d37-ca96-4ae4-8e8b-88c54dae40f6", 1, false, null, "zeynepsahin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("99999999-9999-9999-9999-999999999999") },
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
