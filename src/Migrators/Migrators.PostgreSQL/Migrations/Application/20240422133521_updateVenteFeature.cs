using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class updateVenteFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventes_Products_ProductId",
                schema: "IzeEntities",
                table: "Ventes");

            migrationBuilder.DropIndex(
                name: "IX_Ventes_ProductId",
                schema: "IzeEntities",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "IzeEntities",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "Quantite",
                schema: "IzeEntities",
                table: "Ventes");

            migrationBuilder.CreateTable(
                name: "VenteProduits",
                schema: "IzeEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantite = table.Column<int>(type: "integer", nullable: false),
                    Prix = table.Column<decimal>(type: "numeric", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    VenteId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenteProduits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenteProduits_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VenteProduits_Ventes_VenteId",
                        column: x => x.VenteId,
                        principalSchema: "IzeEntities",
                        principalTable: "Ventes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VenteProduits_ProductId",
                schema: "IzeEntities",
                table: "VenteProduits",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_VenteProduits_VenteId",
                schema: "IzeEntities",
                table: "VenteProduits",
                column: "VenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VenteProduits",
                schema: "IzeEntities");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                schema: "IzeEntities",
                table: "Ventes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Quantite",
                schema: "IzeEntities",
                table: "Ventes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ventes_ProductId",
                schema: "IzeEntities",
                table: "Ventes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventes_Products_ProductId",
                schema: "IzeEntities",
                table: "Ventes",
                column: "ProductId",
                principalSchema: "Catalog",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
