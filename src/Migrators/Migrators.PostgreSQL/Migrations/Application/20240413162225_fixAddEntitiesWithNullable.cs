using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class fixAddEntitiesWithNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Chambres_ChambreId",
                schema: "IzeEntities",
                table: "Reservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChambreId",
                schema: "IzeEntities",
                table: "Reservations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Chambres_ChambreId",
                schema: "IzeEntities",
                table: "Reservations",
                column: "ChambreId",
                principalSchema: "IzeEntities",
                principalTable: "Chambres",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Chambres_ChambreId",
                schema: "IzeEntities",
                table: "Reservations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChambreId",
                schema: "IzeEntities",
                table: "Reservations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Chambres_ChambreId",
                schema: "IzeEntities",
                table: "Reservations",
                column: "ChambreId",
                principalSchema: "IzeEntities",
                principalTable: "Chambres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
