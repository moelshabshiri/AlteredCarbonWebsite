using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KitOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FarmerEmail = table.Column<string>(type: "TEXT", nullable: true),
                    ApprovedBy = table.Column<string>(type: "TEXT", nullable: true),
                    DatetimeOfOrderCreation = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DatetimeOfOrderApproval = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    OrderPoints = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KitOrderHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DatetimeOfHistory = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Comment = table.Column<string>(type: "TEXT", nullable: true),
                    CooperativeUserEmail = table.Column<string>(type: "TEXT", nullable: true),
                    OrderPoints = table.Column<decimal>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    FarmerEmail = table.Column<string>(type: "TEXT", nullable: true),
                    KitOrderId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitOrderHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitOrderHistories_KitOrders_KitOrderId",
                        column: x => x.KitOrderId,
                        principalTable: "KitOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KitOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    TypeValue = table.Column<decimal>(type: "TEXT", nullable: false),
                    OrderItemPoints = table.Column<decimal>(type: "TEXT", nullable: false),
                    Acres = table.Column<decimal>(type: "TEXT", nullable: false),
                    Approved = table.Column<bool>(type: "INTEGER", nullable: false),
                    KitOrderHistoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KitOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KitOrderItems_KitOrderHistories_KitOrderHistoryId",
                        column: x => x.KitOrderHistoryId,
                        principalTable: "KitOrderHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itemHistorys",
                columns: table => new
                {
                    HistoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itemHistorys", x => new { x.HistoryId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_itemHistorys_KitOrderHistories_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "KitOrderHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itemHistorys_KitOrderItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "KitOrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_itemHistorys_ItemId",
                table: "itemHistorys",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_KitOrderHistories_KitOrderId",
                table: "KitOrderHistories",
                column: "KitOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_KitOrderItems_KitOrderHistoryId",
                table: "KitOrderItems",
                column: "KitOrderHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "itemHistorys");

            migrationBuilder.DropTable(
                name: "KitOrderItems");

            migrationBuilder.DropTable(
                name: "KitOrderHistories");

            migrationBuilder.DropTable(
                name: "KitOrders");
        }
    }
}
