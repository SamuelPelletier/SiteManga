using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SiteManga.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Editor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(nullable: false),
                    AddressShippingSteelName = table.Column<string>(nullable: true),
                    AddressShippingPostalCode = table.Column<string>(nullable: true),
                    AddressShippingCountry = table.Column<string>(nullable: true),
                    AddressShippingCity = table.Column<string>(nullable: true),
                    AddressShippingOption1 = table.Column<string>(nullable: true),
                    AddressShippingOption2 = table.Column<string>(nullable: true),
                    AddressInvoiceSteelName = table.Column<string>(nullable: true),
                    AddressInvoiceCountry = table.Column<string>(nullable: true),
                    AddressInvoiceCity = table.Column<string>(nullable: true),
                    AddressInvoicePostalCode = table.Column<string>(nullable: true),
                    AddressInvoiceOption1 = table.Column<string>(nullable: true),
                    AddressInvoiceOption2 = table.Column<string>(nullable: true),
                    TotalWeight = table.Column<double>(nullable: false),
                    ShippingTax = table.Column<double>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manga",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: true),
                    LongDescription = table.Column<string>(nullable: true),
                    Height = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    Color = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    NumberOfPages = table.Column<int>(nullable: false),
                    EditorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manga_Editor_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Editor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: false),
                    mangaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Manga_mangaId",
                        column: x => x.mangaId,
                        principalTable: "Manga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MangaOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(nullable: false),
                    MangaId = table.Column<int>(nullable: true),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MangaOrders_Manga_MangaId",
                        column: x => x.MangaId,
                        principalTable: "Manga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MangaOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_mangaId",
                table: "Image",
                column: "mangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Manga_EditorId",
                table: "Manga",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaOrders_MangaId",
                table: "MangaOrders",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaOrders_OrderId",
                table: "MangaOrders",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "MangaOrders");

            migrationBuilder.DropTable(
                name: "Manga");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Editor");
        }
    }
}
