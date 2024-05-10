using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class addClientToVenteFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                schema: "IzeEntities",
                table: "Ventes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventes_ClientId",
                schema: "IzeEntities",
                table: "Ventes",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ventes_Clients_ClientId",
                schema: "IzeEntities",
                table: "Ventes",
                column: "ClientId",
                principalSchema: "IzeEntities",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ventes_Clients_ClientId",
                schema: "IzeEntities",
                table: "Ventes");

            migrationBuilder.DropIndex(
                name: "IX_Ventes_ClientId",
                schema: "IzeEntities",
                table: "Ventes");

            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "IzeEntities",
                table: "Ventes");
        }
    }
}
