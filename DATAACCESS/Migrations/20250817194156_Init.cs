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
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 8, 17, 22, 41, 54, 707, DateTimeKind.Local).AddTicks(5814), null, "Admin", "ADMIN", 1, null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 8, 17, 22, 41, 54, 707, DateTimeKind.Local).AddTicks(5845), null, "TalepOluşturanBirim", "TALEPOLUSTURANBIRIM", 1, null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 8, 17, 22, 41, 54, 707, DateTimeKind.Local).AddTicks(5851), null, "IhtiyacTespitKomisyonu", "IHTIYACTESPITKOMISYONU", 1, null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 8, 17, 22, 41, 54, 707, DateTimeKind.Local).AddTicks(5864), null, "SatinAlmaBirimi", "SATINALMABIRIMI", 1, null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(2025, 8, 17, 22, 41, 54, 707, DateTimeKind.Local).AddTicks(5866), null, "DepoBirimi", "DEPOBIRIMI", 1, null },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), null, new DateTime(2025, 8, 17, 22, 41, 54, 707, DateTimeKind.Local).AddTicks(5868), null, "OdemeBirimi", "ODEMEBIRIMI", 1, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthdate", "ConcurrencyStamp", "CreatedDate", "DeletedDate", "Email", "EmailConfirmed", "FirstName", "HasFirstPasswordChanged", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-00000000000a"), 0, new DateOnly(1, 1, 1), "84bb4c5e-4c0a-4303-8312-663186ef28bf", new DateTime(2025, 8, 17, 22, 41, 55, 192, DateTimeKind.Local).AddTicks(126), null, "kerem.acar@hospital.local", true, "Kerem", true, "Acar", false, null, "KEREM.ACAR@HOSPITAL.LOCAL", "KEREMACAR", "AQAAAAIAAYagAAAAEJWnhCfvOLrP8euFwooWpUR92kL7mEFSd7KYIdLEwyV8k9tb1pETwxOaj7reDWJwkg==", null, false, "4e59776f-0417-4d07-868d-95f3b99d0fe1", 1, false, null, "keremacar" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), 0, new DateOnly(1, 1, 1), "c9f3e38d-968b-4771-9933-e68c99e729de", new DateTime(2025, 8, 17, 22, 41, 55, 269, DateTimeKind.Local).AddTicks(7353), null, "derya.uslu@hospital.local", true, "Derya", true, "Uslu", false, null, "DERYA.USLU@HOSPITAL.LOCAL", "DERYAUSLU", "AQAAAAIAAYagAAAAEKINBLaHVBc4p2m91ceSv5qSYaIVLCvjje6pBc+fLGBgS80R+Up9GaMPMSSXUUDcSA==", null, false, "011a86fb-bd96-45cc-92a4-c56e311ee005", 1, false, null, "deryauslu" },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), 0, new DateOnly(1, 1, 1), "4a994d91-3f58-45e1-bd4e-86227dd34721", new DateTime(2025, 8, 17, 22, 41, 55, 330, DateTimeKind.Local).AddTicks(4821), null, "burak.keskin@hospital.local", true, "Burak", true, "Keskin", false, null, "BURAK.KESKIN@HOSPITAL.LOCAL", "BURAKKESKIN", "AQAAAAIAAYagAAAAEPCuTLCR7YbrCGrJswBz1H1engH6cXQU1HCT9H3xnoKlkDIsZW2dM88DuTaNgelPSA==", null, false, "ce776773-4c67-4ee9-a8fb-36eea83bdaa3", 1, false, null, "burakkeskin" },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), 0, new DateOnly(1, 1, 1), "976e3339-dfd0-446e-a3fb-3afcc463cf2b", new DateTime(2025, 8, 17, 22, 41, 55, 386, DateTimeKind.Local).AddTicks(6242), null, "selin.koral@hospital.local", true, "Selin", true, "Koral", false, null, "SELIN.KORAL@HOSPITAL.LOCAL", "SELINKORAL", "AQAAAAIAAYagAAAAEFEnUzvPUyOG3KSM/ph/wsRhbkkb1DCsc3TfmbslVGI7IS80mOjTPMx1x49xly5Sww==", null, false, "2d914b6f-bd29-454f-8ed6-b1df69a8cf87", 1, false, null, "selinkoral" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), 0, new DateOnly(1, 1, 1), "c17133d6-0c36-46e1-82c4-3b91a8d6b853", new DateTime(2025, 8, 17, 22, 41, 55, 48, DateTimeKind.Local).AddTicks(9651), null, "fatma.oz@hospital.local", true, "Fatma", true, "Öz", false, null, "FATMA.OZ@HOSPITAL.LOCAL", "FATMAOZ", "AQAAAAIAAYagAAAAECcGu2fSx0ZiaWcn66N1AOTjaMLfVAoPDqBMeLhyf2Q0LsipVgUMLw+yG4jd647ljQ==", null, false, "fabf2e08-0f17-4d2b-8fc2-82dbb7c92ff1", 1, false, null, "fatmaoz" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 0, new DateOnly(1, 1, 1), "09d17653-e6f8-4728-8a2d-bc9d58359f87", new DateTime(2025, 8, 17, 22, 41, 54, 707, DateTimeKind.Local).AddTicks(6097), null, "admin@example.com", true, "Sistem", true, "Yöneticisi", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEECQgh6lkgbeN+4b/qtg5LBvbyRAkfeQmANykz9QH3itHD2YzRR59bvqf8VUy5cwrQ==", null, false, "5dc20935-a266-4165-9101-783deae50679", 1, false, null, "admin" },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 0, new DateOnly(1, 1, 1), "2ad378c3-5bf9-420e-abf1-eba6175b22cb", new DateTime(2025, 8, 17, 22, 41, 54, 768, DateTimeKind.Local).AddTicks(6438), null, "ahmet.yilmaz@hospital.local", true, "Ahmet", true, "Yılmaz", false, null, "AHMET.YILMAZ@HOSPITAL.LOCAL", "AHMETYILMAZ", "AQAAAAIAAYagAAAAEFozcaO7jAyLFNfUmgHtRU30NXWmviiNVKr5dsPJGL1bzJ8YOun3MGscItqJz4dM+w==", null, false, "1b9d304b-9c11-47f3-9a61-85cb306f865f", 1, false, null, "ahmetyilmaz" },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 0, new DateOnly(1, 1, 1), "7374514b-6715-4171-92a5-85b2fb139910", new DateTime(2025, 8, 17, 22, 41, 54, 832, DateTimeKind.Local).AddTicks(3557), null, "elif.kara@hospital.local", true, "Elif", true, "Kara", false, null, "ELIF.KARA@HOSPITAL.LOCAL", "ELIFKARA", "AQAAAAIAAYagAAAAEO0RHM88kS3giJBH52dv5Zs3jWiFf9ZiLr+UPj6xx003hHhZJ6cTxksFULH6jTxIVw==", null, false, "7fb0860e-8984-41da-ae00-d66a65533a2d", 1, false, null, "elifkara" },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 0, new DateOnly(1, 1, 1), "2840ccb4-481c-4af7-9894-c2f18e7ce937", new DateTime(2025, 8, 17, 22, 41, 54, 889, DateTimeKind.Local).AddTicks(3321), null, "mehmet.demir@hospital.local", true, "Mehmet", true, "Demir", false, null, "MEHMET.DEMIR@HOSPITAL.LOCAL", "MEHMETDEMIR", "AQAAAAIAAYagAAAAEO2+QB4sfOhSvLXW51wqi24OsoS5qvdVVmCJFGV7BOBaRTxFUQrPBDtigRyP3x3TNg==", null, false, "4ab7aee0-e1a7-4336-9143-426736ca7423", 1, false, null, "mehmetdemir" },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 0, new DateOnly(1, 1, 1), "a28e3f9a-efed-433f-aa23-5b6e8600fcbc", new DateTime(2025, 8, 17, 22, 41, 54, 975, DateTimeKind.Local).AddTicks(4361), null, "zeynep.sahin@hospital.local", true, "Zeynep", true, "Şahin", false, null, "ZEYNEP.SAHIN@HOSPITAL.LOCAL", "ZEYNEPSAHIN", "AQAAAAIAAYagAAAAEDYq58AByVL41ZuIdcPOiFTWbsdGkaWU6WE1cUGm3gHYFsD9shxDljrkaHN3mD9ccA==", null, false, "4be7d901-ade4-40a5-8a15-81d78da091d9", 1, false, null, "zeynepsahin" },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), 0, new DateOnly(1, 1, 1), "1ddb19a8-babd-497b-88d9-870c7cc55f2e", new DateTime(2025, 8, 17, 22, 41, 55, 112, DateTimeKind.Local).AddTicks(7522), null, "ayse.akin@hospital.local", true, "Ayşe", true, "Akın", false, null, "AYSE.AKIN@HOSPITAL.LOCAL", "AYSEAKIN", "AQAAAAIAAYagAAAAEHZTHvUVeCfOqYNPUp5C/ws7Zq+k/v70+N9r0wLHfmjqV5z5rIKL/Ay5UvCMWMgYww==", null, false, "b5f6f8d6-1166-491b-af74-06a97a6ecfac", 1, false, null, "ayseakin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("00000000-0000-0000-0000-00000000000a") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("00000000-0000-0000-0000-00000000000b") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("00000000-0000-0000-0000-00000000000c") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("00000000-0000-0000-0000-00000000000d") },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee") },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff") }
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
