using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DATAACCESS.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class Initil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Titles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TitleName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Titles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubCategoryName = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TitleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductName = table.Column<string>(type: "text", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    StockAmount = table.Column<double>(type: "double precision", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    TitleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    SpecialProductName = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: true),
                    ProductFeaturesFilePath = table.Column<string>(type: "text", nullable: true),
                    ProductFeatures = table.Column<string>(type: "text", nullable: true),
                    CommissionNote = table.Column<string>(type: "text", nullable: true),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    TitleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    ProductId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    TitleId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Employees_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Products_ProductId1",
                        column: x => x.ProductId1,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Requests_Titles_TitleId",
                        column: x => x.TitleId,
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Titles_TitleId1",
                        column: x => x.TitleId1,
                        principalTable: "Titles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AmountToPay = table.Column<double>(type: "double precision", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "text", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    SupplierName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SupplierTaxNo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SupplierIban = table.Column<string>(type: "character varying(34)", maxLength: 34, nullable: true),
                    SupplierEmail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SupplierPhone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    OfferNo = table.Column<string>(type: "text", nullable: true),
                    OfferDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    PaymentTerms = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    DiscountRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    VatRate = table.Column<decimal>(type: "numeric(5,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    VatAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    GrandTotal = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    Currency = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    OfferPdfPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchases_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StockOutAmount = table.Column<double>(type: "double precision", nullable: false),
                    StockInAmount = table.Column<double>(type: "double precision", nullable: false),
                    GeneralStockAmount = table.Column<double>(type: "double precision", nullable: false),
                    WaybillNumber = table.Column<string>(type: "text", nullable: false),
                    WaybillPrice = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouses_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouses_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouses_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "DeletedDate", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Tıbbi Cihazlar", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Sarf Malzemeleri", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Sterilizasyon ve Hijyen", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Laboratuvar", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "İlaç ve Serum", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Radyoloji ve Görüntüleme", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Ortopedi", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Anestezi", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Yoğun Bakım", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Diş Hekimliği", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Status", "TitleName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("f0000011-aaaa-bbbb-cccc-0000000000ab"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Uzman Hekim", null },
                    { new Guid("f0000012-aaaa-bbbb-cccc-0000000000ac"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Hemşire", null },
                    { new Guid("f0000013-aaaa-bbbb-cccc-0000000000ad"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Eczacı", null },
                    { new Guid("f0000014-aaaa-bbbb-cccc-0000000000ae"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Radyoloji Teknisyeni", null },
                    { new Guid("f0000015-aaaa-bbbb-cccc-0000000000af"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Laboratuvar Teknisyeni", null },
                    { new Guid("f0000016-aaaa-bbbb-cccc-0000000000b0"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Biyomedikal Mühendisi", null },
                    { new Guid("f0000017-aaaa-bbbb-cccc-0000000000b1"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Anestezi Teknikeri", null },
                    { new Guid("f0000018-aaaa-bbbb-cccc-0000000000b2"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Ortopedi Teknikeri", null },
                    { new Guid("f0000019-aaaa-bbbb-cccc-0000000000b3"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Yoğun Bakım Hemşiresi", null },
                    { new Guid("f0000020-aaaa-bbbb-cccc-0000000000b4"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Diş Hekimi", null }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "DepartmentName", "Status", "TitleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Acil Servis", 1, new Guid("f0000011-aaaa-bbbb-cccc-0000000000ab"), null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ameliyathane", 1, new Guid("f0000017-aaaa-bbbb-cccc-0000000000b1"), null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Yoğun Bakım", 1, new Guid("f0000019-aaaa-bbbb-cccc-0000000000b3"), null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Radyoloji", 1, new Guid("f0000014-aaaa-bbbb-cccc-0000000000ae"), null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Laboratuvar", 1, new Guid("f0000015-aaaa-bbbb-cccc-0000000000af"), null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Eczane", 1, new Guid("f0000013-aaaa-bbbb-cccc-0000000000ad"), null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sterilizasyon (CSSD)", 1, new Guid("f0000016-aaaa-bbbb-cccc-0000000000b0"), null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ortopedi", 1, new Guid("f0000018-aaaa-bbbb-cccc-0000000000b2"), null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anestezi ve Reanimasyon", 1, new Guid("f0000017-aaaa-bbbb-cccc-0000000000b1"), null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Diş Kliniği", 1, new Guid("f0000020-aaaa-bbbb-cccc-0000000000b4"), null }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "DeletedDate", "Status", "SubCategoryName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0f111111-0000-0000-0000-000000000001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Defibrilatörler", null },
                    { new Guid("0f111111-0000-0000-0000-000000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Enjeksiyon ve İğne", null },
                    { new Guid("0f111111-0000-0000-0000-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Otoklav Sarfı", null },
                    { new Guid("0f111111-0000-0000-0000-000000000004"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Mikrobiyoloji Sarfı", null },
                    { new Guid("0f111111-0000-0000-0000-000000000005"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Antibiyotik ve Serumlar", null },
                    { new Guid("0f111111-0000-0000-0000-000000000006"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Röntgen Sarfı", null },
                    { new Guid("0f111111-0000-0000-0000-000000000007"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Alçı ve Atel", null },
                    { new Guid("0f111111-0000-0000-0000-000000000008"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Anestezi Maskeleri", null },
                    { new Guid("0f111111-0000-0000-0000-000000000009"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Ventilatör Sarfı", null },
                    { new Guid("0f111111-0000-0000-0000-000000000010"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Diş Üniti Sarfı", null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AppUserId", "CreatedDate", "DeletedDate", "DepartmentId", "Email", "FirstName", "ImagePath", "LastName", "Status", "TitleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("e1111111-1111-1111-1111-111111111111"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("11111111-1111-1111-1111-111111111111"), "ahmet.yilmaz@hospital.local", "Ahmet", null, "Yılmaz", 1, new Guid("f0000011-aaaa-bbbb-cccc-0000000000ab"), null },
                    { new Guid("e2222222-2222-2222-2222-222222222222"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("22222222-2222-2222-2222-222222222222"), "elif.kara@hospital.local", "Elif", null, "Kara", 1, new Guid("f0000017-aaaa-bbbb-cccc-0000000000b1"), null },
                    { new Guid("e3333333-3333-3333-3333-333333333333"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("33333333-3333-3333-3333-333333333333"), "mehmet.demir@hospital.local", "Mehmet", null, "Demir", 1, new Guid("f0000019-aaaa-bbbb-cccc-0000000000b3"), null },
                    { new Guid("e4444444-4444-4444-4444-444444444444"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("44444444-4444-4444-4444-444444444444"), "zeynep.sahin@hospital.local", "Zeynep", null, "Şahin", 1, new Guid("f0000014-aaaa-bbbb-cccc-0000000000ae"), null },
                    { new Guid("e5555555-5555-5555-5555-555555555555"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("55555555-5555-5555-5555-555555555555"), "can.yildiz@hospital.local", "Can", null, "Yıldız", 1, new Guid("f0000015-aaaa-bbbb-cccc-0000000000af"), null },
                    { new Guid("e6666666-6666-6666-6666-666666666666"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("66666666-6666-6666-6666-666666666666"), "ayse.akin@hospital.local", "Ayşe", null, "Akın", 1, new Guid("f0000013-aaaa-bbbb-cccc-0000000000ad"), null },
                    { new Guid("e7777777-7777-7777-7777-777777777777"), new Guid("00000000-0000-0000-0000-00000000000a"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("77777777-7777-7777-7777-777777777777"), "kerem.acar@hospital.local", "Kerem", null, "Acar", 1, new Guid("f0000016-aaaa-bbbb-cccc-0000000000b0"), null },
                    { new Guid("e8888888-8888-8888-8888-888888888888"), new Guid("00000000-0000-0000-0000-00000000000b"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("88888888-8888-8888-8888-888888888888"), "derya.uslu@hospital.local", "Derya", null, "Uslu", 1, new Guid("f0000018-aaaa-bbbb-cccc-0000000000b2"), null },
                    { new Guid("e9999999-9999-9999-9999-999999999999"), new Guid("00000000-0000-0000-0000-00000000000c"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("99999999-9999-9999-9999-999999999999"), "burak.keskin@hospital.local", "Burak", null, "Keskin", 1, new Guid("f0000017-aaaa-bbbb-cccc-0000000000b1"), null },
                    { new Guid("eaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("00000000-0000-0000-0000-00000000000d"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "selin.koral@hospital.local", "Selin", null, "Koral", 1, new Guid("f0000020-aaaa-bbbb-cccc-0000000000b4"), null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "ImagePath", "ProductName", "Status", "StockAmount", "SubCategoryId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Defibrilatör Cihazı (D-100)", 1, 3.0, new Guid("0f111111-0000-0000-0000-000000000001"), null },
                    { new Guid("22222222-aaaa-bbbb-cccc-222222222222"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Tek Kullanımlık Enjektör 5 ml", 1, 500.0, new Guid("0f111111-0000-0000-0000-000000000002"), null },
                    { new Guid("33333333-aaaa-bbbb-cccc-333333333333"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Otoklav Poşeti 200x300 mm", 1, 200.0, new Guid("0f111111-0000-0000-0000-000000000003"), null },
                    { new Guid("44444444-aaaa-bbbb-cccc-444444444444"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Biyokimya Reaktifi Seti", 1, 25.0, new Guid("0f111111-0000-0000-0000-000000000004"), null },
                    { new Guid("55555555-aaaa-bbbb-cccc-555555555555"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "IV Serum Seti", 1, 120.0, new Guid("0f111111-0000-0000-0000-000000000005"), null },
                    { new Guid("66666666-aaaa-bbbb-cccc-666666666666"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Kurşun Önlük (Yetişkin)", 1, 10.0, new Guid("0f111111-0000-0000-0000-000000000006"), null },
                    { new Guid("77777777-aaaa-bbbb-cccc-777777777777"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ortopedik Kol Ateli", 1, 40.0, new Guid("0f111111-0000-0000-0000-000000000007"), null },
                    { new Guid("88888888-aaaa-bbbb-cccc-888888888888"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Anestezi Maskesi (Medium)", 1, 60.0, new Guid("0f111111-0000-0000-0000-000000000008"), null },
                    { new Guid("99999999-aaaa-bbbb-cccc-999999999999"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Ventilatör Devresi (Yetişkin)", 1, 30.0, new Guid("0f111111-0000-0000-0000-000000000009"), null },
                    { new Guid("aaaaaaaa-aaaa-bbbb-cccc-aaaaaaaaaaaa"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Diş Hekimliği El Aleti Seti", 1, 15.0, new Guid("0f111111-0000-0000-0000-000000000010"), null }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Amount", "AppUserId", "CommissionNote", "CreatedDate", "DeletedDate", "DepartmentId", "EmployeeId", "EmployeeId1", "IsApproved", "ProductFeatures", "ProductFeaturesFilePath", "ProductId", "ProductId1", "RequestDate", "SpecialProductName", "Status", "TitleId", "TitleId1", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), 1.0m, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Acil servis için", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("11111111-1111-1111-1111-111111111111"), new Guid("e1111111-1111-1111-1111-111111111111"), null, true, "Bifazik, AED modu", null, new Guid("11111111-aaaa-bbbb-cccc-111111111111"), null, new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Defibrilatör", 1, new Guid("f0000011-aaaa-bbbb-cccc-0000000000ab"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), 200.0m, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Ameliyathane stok takviyesi", new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("22222222-2222-2222-2222-222222222222"), new Guid("e2222222-2222-2222-2222-222222222222"), null, true, "Steril, Luer lock", null, new Guid("22222222-aaaa-bbbb-cccc-222222222222"), null, new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enjektör 5 ml", 1, new Guid("f0000017-aaaa-bbbb-cccc-0000000000b1"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), 100.0m, new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "CSSD için", new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("33333333-3333-3333-3333-333333333333"), new Guid("e3333333-3333-3333-3333-333333333333"), null, false, "200x300 mm", null, new Guid("33333333-aaaa-bbbb-cccc-333333333333"), null, new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Otoklav Poşeti", 1, new Guid("f0000019-aaaa-bbbb-cccc-0000000000b3"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), 5.0m, new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Laboratuvar paneli", new DateTime(2025, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("44444444-4444-4444-4444-444444444444"), new Guid("e4444444-4444-4444-4444-444444444444"), null, true, "Reaktif seti", null, new Guid("44444444-aaaa-bbbb-cccc-444444444444"), null, new DateTime(2025, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Biyokimya Reaktifi", 1, new Guid("f0000014-aaaa-bbbb-cccc-0000000000ae"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), 80.0m, new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Servisler için", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("55555555-5555-5555-5555-555555555555"), new Guid("e5555555-5555-5555-5555-555555555555"), null, true, "Hava filtresi, steril", null, new Guid("55555555-aaaa-bbbb-cccc-555555555555"), null, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "IV Serum Seti", 1, new Guid("f0000015-aaaa-bbbb-cccc-0000000000af"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000006"), 2.0m, new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "Radyoloji güvenliği", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("66666666-6666-6666-6666-666666666666"), new Guid("e6666666-6666-6666-6666-666666666666"), null, true, "0.5 mm Pb eşdeğeri", null, new Guid("66666666-aaaa-bbbb-cccc-666666666666"), null, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kurşun Önlük", 1, new Guid("f0000013-aaaa-bbbb-cccc-0000000000ad"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000007"), 20.0m, new Guid("00000000-0000-0000-0000-00000000000a"), "Ortopedi stoğu", new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("77777777-7777-7777-7777-777777777777"), new Guid("e7777777-7777-7777-7777-777777777777"), null, false, "Ayarlanabilir", null, new Guid("77777777-aaaa-bbbb-cccc-777777777777"), null, new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ortopedik Kol Ateli", 1, new Guid("f0000016-aaaa-bbbb-cccc-0000000000b0"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000008"), 30.0m, new Guid("00000000-0000-0000-0000-00000000000b"), "Ameliyathane için", new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("88888888-8888-8888-8888-888888888888"), new Guid("e8888888-8888-8888-8888-888888888888"), null, true, "Latex-free", null, new Guid("88888888-aaaa-bbbb-cccc-888888888888"), null, new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anestezi Maskesi", 1, new Guid("f0000018-aaaa-bbbb-cccc-0000000000b2"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000009"), 12.0m, new Guid("00000000-0000-0000-0000-00000000000c"), "Yoğun bakım devriyesi", new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("99999999-9999-9999-9999-999999999999"), new Guid("e9999999-9999-9999-9999-999999999999"), null, true, "Yetişkin, steril", null, new Guid("99999999-aaaa-bbbb-cccc-999999999999"), null, new DateTime(2025, 7, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ventilatör Devresi", 1, new Guid("f0000017-aaaa-bbbb-cccc-0000000000b1"), null, null },
                    { new Guid("10000000-0000-0000-0000-000000000010"), 4.0m, new Guid("00000000-0000-0000-0000-00000000000d"), "Diş kliniği", new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("eaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, false, "Steril set", null, new Guid("aaaaaaaa-aaaa-bbbb-cccc-aaaaaaaaaaaa"), null, new DateTime(2025, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Diş El Aleti Seti", 1, new Guid("f0000020-aaaa-bbbb-cccc-0000000000b4"), null, null }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "AmountToPay", "CreatedDate", "DeletedDate", "EmployeeId", "InvoiceNumber", "IsPaid", "PaymentDate", "RequestId", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 42000.0, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("e1111111-1111-1111-1111-111111111111"), "INV-3001", true, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000001"), 1, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 9500.5, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("e2222222-2222-2222-2222-222222222222"), "INV-3002", false, new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000002"), 1, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 7200.75, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("e3333333-3333-3333-3333-333333333333"), "INV-3003", true, new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000003"), 2, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 16850.900000000001, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("e4444444-4444-4444-4444-444444444444"), "INV-3004", false, new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000004"), 1, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 11000.0, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("e5555555-5555-5555-5555-555555555555"), "INV-3005", true, new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000005"), 1, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), 54000.0, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("e6666666-6666-6666-6666-666666666666"), "INV-3006", true, new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000006"), 1, null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), 7800.0, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("e7777777-7777-7777-7777-777777777777"), "INV-3007", false, new DateTime(2025, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000007"), 1, null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), 9300.0, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("e8888888-8888-8888-8888-888888888888"), "INV-3008", true, new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000008"), 1, null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), 25600.0, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("e9999999-9999-9999-9999-999999999999"), "INV-3009", true, new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000009"), 1, null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 12400.0, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("eaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "INV-3010", false, new DateTime(2025, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-000000000010"), 1, null }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "DeletedDate", "DepartmentId", "EmployeeId", "GeneralStockAmount", "ProductId", "RequestId", "Status", "StockInAmount", "StockOutAmount", "SubCategoryId", "UpdatedDate", "WaybillNumber", "WaybillPrice" },
                values: new object[,]
                {
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("11111111-1111-1111-1111-111111111111"), new Guid("e1111111-1111-1111-1111-111111111111"), 2.0, new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new Guid("10000000-0000-0000-0000-000000000001"), 1, 3.0, 1.0, new Guid("0f111111-0000-0000-0000-000000000001"), null, "WB-2001", "42000" },
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("22222222-2222-2222-2222-222222222222"), new Guid("e2222222-2222-2222-2222-222222222222"), 150.0, new Guid("22222222-aaaa-bbbb-cccc-222222222222"), new Guid("10000000-0000-0000-0000-000000000002"), 1, 200.0, 50.0, new Guid("0f111111-0000-0000-0000-000000000002"), null, "WB-2002", "9500.5" },
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("33333333-3333-3333-3333-333333333333"), new Guid("e3333333-3333-3333-3333-333333333333"), 100.0, new Guid("33333333-aaaa-bbbb-cccc-333333333333"), new Guid("10000000-0000-0000-0000-000000000003"), 1, 120.0, 20.0, new Guid("0f111111-0000-0000-0000-000000000003"), null, "WB-2003", "7200.75" },
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1004"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("44444444-4444-4444-4444-444444444444"), new Guid("e4444444-4444-4444-4444-444444444444"), 15.0, new Guid("44444444-aaaa-bbbb-cccc-444444444444"), new Guid("10000000-0000-0000-0000-000000000004"), 1, 20.0, 5.0, new Guid("0f111111-0000-0000-0000-000000000004"), null, "WB-2004", "16850.9" },
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1005"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("55555555-5555-5555-5555-555555555555"), new Guid("e5555555-5555-5555-5555-555555555555"), 90.0, new Guid("55555555-aaaa-bbbb-cccc-555555555555"), new Guid("10000000-0000-0000-0000-000000000005"), 1, 120.0, 30.0, new Guid("0f111111-0000-0000-0000-000000000005"), null, "WB-2005", "11000" },
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1006"), new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("66666666-6666-6666-6666-666666666666"), new Guid("e6666666-6666-6666-6666-666666666666"), 1.0, new Guid("66666666-aaaa-bbbb-cccc-666666666666"), new Guid("10000000-0000-0000-0000-000000000006"), 1, 2.0, 1.0, new Guid("0f111111-0000-0000-0000-000000000006"), null, "WB-2006", "54000" },
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1007"), new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("77777777-7777-7777-7777-777777777777"), new Guid("e7777777-7777-7777-7777-777777777777"), 20.0, new Guid("77777777-aaaa-bbbb-cccc-777777777777"), new Guid("10000000-0000-0000-0000-000000000007"), 1, 25.0, 5.0, new Guid("0f111111-0000-0000-0000-000000000007"), null, "WB-2007", "7800" },
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1008"), new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("88888888-8888-8888-8888-888888888888"), new Guid("e8888888-8888-8888-8888-888888888888"), 30.0, new Guid("88888888-aaaa-bbbb-cccc-888888888888"), new Guid("10000000-0000-0000-0000-000000000008"), 1, 40.0, 10.0, new Guid("0f111111-0000-0000-0000-000000000008"), null, "WB-2008", "9300" },
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1009"), new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("99999999-9999-9999-9999-999999999999"), new Guid("e9999999-9999-9999-9999-999999999999"), 16.0, new Guid("99999999-aaaa-bbbb-cccc-999999999999"), new Guid("10000000-0000-0000-0000-000000000009"), 1, 20.0, 4.0, new Guid("0f111111-0000-0000-0000-000000000009"), null, "WB-2009", "25600" },
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1010"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("eaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 9.0, new Guid("aaaaaaaa-aaaa-bbbb-cccc-aaaaaaaaaaaa"), new Guid("10000000-0000-0000-0000-000000000010"), 1, 10.0, 1.0, new Guid("0f111111-0000-0000-0000-000000000010"), null, "WB-2010", "12400" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_TitleId",
                table: "Departments",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TitleId",
                table: "Employees",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_EmployeeId",
                table: "Payments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_RequestId",
                table: "Payments",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_RequestId",
                table: "Purchases",
                column: "RequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_EmployeeId",
                table: "Requests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_EmployeeId1",
                table: "Requests",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ProductId",
                table: "Requests",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ProductId1",
                table: "Requests",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TitleId",
                table: "Requests",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_TitleId1",
                table: "Requests",
                column: "TitleId1");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CategoryId",
                table: "Warehouses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_DepartmentId",
                table: "Warehouses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_EmployeeId",
                table: "Warehouses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_ProductId",
                table: "Warehouses",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_RequestId",
                table: "Warehouses",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_SubCategoryId",
                table: "Warehouses",
                column: "SubCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Titles");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
