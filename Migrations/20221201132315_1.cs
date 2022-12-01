using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalePortal.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateTable(
                name: "CommodityOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    CommodityOwnerId = table.Column<int>(type: "int", nullable: true),
                    CommodityId = table.Column<int>(type: "int", nullable: true),
                    ApprovedByOwner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommodityOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommodityOrders_commodities_CommodityId",
                        column: x => x.CommodityId,
                        principalTable: "commodities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommodityOrders_Users_CommodityOwnerId",
                        column: x => x.CommodityOwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommodityOrders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });



            migrationBuilder.CreateIndex(
                name: "IX_CommodityOrders_CommodityId",
                table: "CommodityOrders",
                column: "CommodityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommodityOrders_CommodityOwnerId",
                table: "CommodityOrders",
                column: "CommodityOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CommodityOrders_CustomerId",
                table: "CommodityOrders",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "CommodityOrders");

        }
    }
}
