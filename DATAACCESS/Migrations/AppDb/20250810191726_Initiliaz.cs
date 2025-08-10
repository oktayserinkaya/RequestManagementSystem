using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DATAACCESS.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class Initiliaz : Migration
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
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Elektronik", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Ofis Malzemeleri", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Temizlik Ürünleri", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Mobilya", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Titles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Status", "TitleName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("f0000011-aaaa-bbbb-cccc-0000000000ab"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Finans Uzmanı", null },
                    { new Guid("f0000012-aaaa-bbbb-cccc-0000000000ac"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Kalite Kontrol Sorumlusu", null },
                    { new Guid("f0000013-aaaa-bbbb-cccc-0000000000ad"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Ar-Ge Mühendisi", null },
                    { new Guid("f0000014-aaaa-bbbb-cccc-0000000000ae"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Depo Görevlisi", null }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "DepartmentName", "Status", "TitleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bilgi Teknolojileri", 1, new Guid("f0000011-aaaa-bbbb-cccc-0000000000ab"), null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Satın Alma", 1, new Guid("f0000012-aaaa-bbbb-cccc-0000000000ac"), null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Muhasebe", 1, new Guid("f0000013-aaaa-bbbb-cccc-0000000000ad"), null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "İnsan Kaynakları", 1, new Guid("f0000014-aaaa-bbbb-cccc-0000000000ae"), null }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "DeletedDate", "Status", "SubCategoryName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0f111111-0000-0000-0000-0000000000ab"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Tabletler", null },
                    { new Guid("0f111111-0000-0000-0000-0000000000ac"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Web Kameraları", null },
                    { new Guid("0f111111-0000-0000-0000-0000000000ad"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Switch & Hub", null },
                    { new Guid("0f111111-0000-0000-0000-0000000000ae"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Antivirüs Yazılımları", null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "AppUserId", "CreatedDate", "DeletedDate", "DepartmentId", "Email", "FirstName", "ImagePath", "LastName", "Status", "TitleId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("11111111-1111-1111-1111-111111111111"), "ahmet.yilmaz@example.com", "Ahmet", null, "Yılmaz", 1, new Guid("f0000011-aaaa-bbbb-cccc-0000000000ab"), null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("22222222-2222-2222-2222-222222222222"), "elif.kara@example.com", "Elif", null, "Kara", 1, new Guid("f0000012-aaaa-bbbb-cccc-0000000000ac"), null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("33333333-3333-3333-3333-333333333333"), "mehmet.demir@example.com", "Mehmet", null, "Demir", 1, new Guid("f0000013-aaaa-bbbb-cccc-0000000000ad"), null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("44444444-4444-4444-4444-444444444444"), "zeynep.sahin@example.com", "Zeynep", null, "Şahin", 1, new Guid("f0000014-aaaa-bbbb-cccc-0000000000ae"), null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "ImagePath", "ProductName", "Status", "StockAmount", "SubCategoryId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Canon EOS 2000D Kamera", 1, 8.0, new Guid("0f111111-0000-0000-0000-0000000000ab"), null },
                    { new Guid("22222222-aaaa-bbbb-cccc-222222222222"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Apple Magic Keyboard", 1, 20.0, new Guid("0f111111-0000-0000-0000-0000000000ac"), null },
                    { new Guid("33333333-aaaa-bbbb-cccc-333333333333"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Acer Nitro 5 Gaming Laptop", 1, 5.0, new Guid("0f111111-0000-0000-0000-0000000000ad"), null },
                    { new Guid("44444444-aaaa-bbbb-cccc-444444444444"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "JBL Bluetooth Hoparlör", 1, 50.0, new Guid("0f111111-0000-0000-0000-0000000000ae"), null }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Amount", "AppUserId", "CommissionNote", "CreatedDate", "DeletedDate", "DepartmentId", "EmployeeId", "EmployeeId1", "IsApproved", "ProductFeatures", "ProductFeaturesFilePath", "ProductId", "ProductId1", "RequestDate", "SpecialProductName", "Status", "TitleId", "TitleId1", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-0000000000ab"), 1.0m, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Toplantı odası için", new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), null, false, "Full HD, HDMI destekli", null, new Guid("11111111-aaaa-bbbb-cccc-111111111111"), null, new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Projeksiyon Cihazı", 1, new Guid("f0000011-aaaa-bbbb-cccc-0000000000ab"), null, null },
                    { new Guid("10000000-0000-0000-0000-0000000000ac"), 2.0m, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Bilgi İşlem birimi için", new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("22222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222222"), null, true, "Full HD, HDMI destekli", null, new Guid("22222222-aaaa-bbbb-cccc-222222222222"), null, new DateTime(2025, 6, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Projeksiyon Cihazı", 1, new Guid("f0000012-aaaa-bbbb-cccc-0000000000ac"), null, null },
                    { new Guid("10000000-0000-0000-0000-0000000000ad"), 1.0m, new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Yönetici kullanımı", new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("33333333-3333-3333-3333-333333333333"), new Guid("33333333-3333-3333-3333-333333333333"), null, true, "Full HD, HDMI destekli", null, new Guid("33333333-aaaa-bbbb-cccc-333333333333"), null, new DateTime(2025, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full HD, HDMI destekli", 1, new Guid("f0000013-aaaa-bbbb-cccc-0000000000ad"), null, null },
                    { new Guid("10000000-0000-0000-0000-0000000000ae"), 4.0m, new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Etkinlik alanı için ses sistemi", new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("44444444-4444-4444-4444-444444444444"), new Guid("44444444-4444-4444-4444-444444444444"), null, false, "Full HD, HDMI destekli", null, new Guid("44444444-aaaa-bbbb-cccc-444444444444"), null, new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full HD, HDMI destekli", 1, new Guid("f0000014-aaaa-bbbb-cccc-0000000000ae"), null, null }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "AmountToPay", "CreatedDate", "DeletedDate", "EmployeeId", "InvoiceNumber", "IsPaid", "PaymentDate", "RequestId", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 2200.0, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("11111111-1111-1111-1111-111111111111"), "INV-2001", true, new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-0000000000ab"), 1, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 1450.5, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("22222222-2222-2222-2222-222222222222"), "INV-2002", false, new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-0000000000ac"), 3, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 3100.75, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("33333333-3333-3333-3333-333333333333"), "INV-2003", true, new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-0000000000ad"), 2, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 860.89999999999998, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("44444444-4444-4444-4444-444444444444"), "INV-2004", false, new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("10000000-0000-0000-0000-0000000000ae"), 1, null }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "DeletedDate", "DepartmentId", "EmployeeId", "GeneralStockAmount", "ProductId", "RequestId", "Status", "StockInAmount", "StockOutAmount", "SubCategoryId", "UpdatedDate", "WaybillNumber", "WaybillPrice" },
                values: new object[,]
                {
                    { new Guid("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1001"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), 5.0, new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new Guid("10000000-0000-0000-0000-0000000000ab"), 1, 8.0, 3.0, new Guid("0f111111-0000-0000-0000-0000000000ab"), null, "WB-1007", "18400" },
                    { new Guid("94d41b56-9634-49b3-abc5-75fce7f41002"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("22222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222222"), 5.0, new Guid("22222222-aaaa-bbbb-cccc-222222222222"), new Guid("10000000-0000-0000-0000-0000000000ac"), 1, 6.0, 1.0, new Guid("0f111111-0000-0000-0000-0000000000ac"), null, "WB-1008", "9000" },
                    { new Guid("a7074ea5-3c5d-45c7-9872-4de0a2f21004"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("44444444-4444-4444-4444-444444444444"), new Guid("44444444-4444-4444-4444-444444444444"), 8.0, new Guid("44444444-aaaa-bbbb-cccc-444444444444"), new Guid("10000000-0000-0000-0000-0000000000ae"), 1, 10.0, 2.0, new Guid("0f111111-0000-0000-0000-0000000000ae"), null, "WB-1010", "10400" },
                    { new Guid("e8911d5e-9ef6-465a-988c-bc6bcbb11003"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("33333333-3333-3333-3333-333333333333"), new Guid("33333333-3333-3333-3333-333333333333"), 5.0, new Guid("33333333-aaaa-bbbb-cccc-333333333333"), new Guid("10000000-0000-0000-0000-0000000000ad"), 1, 5.0, 0.0, new Guid("0f111111-0000-0000-0000-0000000000ad"), null, "WB-1009", "6700" }
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
