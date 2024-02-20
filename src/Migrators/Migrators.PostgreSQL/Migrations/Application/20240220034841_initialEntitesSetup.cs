using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.PostgreSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class initialEntitesSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "IzeEntities");

            migrationBuilder.CreateTable(
                name: "Agents",
                schema: "IzeEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserCode = table.Column<Guid>(type: "uuid", nullable: false),
                    Prenoms = table.Column<string>(type: "text", nullable: true),
                    Nom = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    TenantId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeChambres",
                schema: "IzeEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Libelle = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    TenantId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeChambres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chambres",
                schema: "IzeEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Capacite = table.Column<int>(type: "integer", nullable: false),
                    Prix = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    ImagePath = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    Disponible = table.Column<bool>(type: "boolean", nullable: false),
                    Climatisee = table.Column<bool>(type: "boolean", nullable: false),
                    PetitDejeunerInclus = table.Column<bool>(type: "boolean", nullable: false),
                    TypeChambreId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chambres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chambres_TypeChambres_TypeChambreId",
                        column: x => x.TypeChambreId,
                        principalSchema: "IzeEntities",
                        principalTable: "TypeChambres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "IzeEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nom = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Prenom = table.Column<string>(type: "text", nullable: true),
                    NomDeJeuneFille = table.Column<string>(type: "text", nullable: true),
                    LieuDeNaissance = table.Column<string>(type: "text", nullable: true),
                    Nationalite = table.Column<string>(type: "text", nullable: true),
                    Profession = table.Column<string>(type: "text", nullable: true),
                    Domicile = table.Column<string>(type: "text", nullable: true),
                    MotifDuVoyage = table.Column<string>(type: "text", nullable: true),
                    VenantDe = table.Column<string>(type: "text", nullable: true),
                    AllantA = table.Column<string>(type: "text", nullable: true),
                    DateArrive = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateDepart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Identite = table.Column<string>(type: "text", nullable: true),
                    DateIdentiteDelivreeLe = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Contact = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    PersonneAPrevenir = table.Column<string>(type: "text", nullable: true),
                    AgentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChambreId = table.Column<Guid>(type: "uuid", nullable: true),
                    TenantId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Agents_AgentId",
                        column: x => x.AgentId,
                        principalSchema: "IzeEntities",
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Chambres_ChambreId",
                        column: x => x.ChambreId,
                        principalSchema: "IzeEntities",
                        principalTable: "Chambres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chambres_TypeChambreId",
                schema: "IzeEntities",
                table: "Chambres",
                column: "TypeChambreId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AgentId",
                schema: "IzeEntities",
                table: "Clients",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ChambreId",
                schema: "IzeEntities",
                table: "Clients",
                column: "ChambreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients",
                schema: "IzeEntities");

            migrationBuilder.DropTable(
                name: "Agents",
                schema: "IzeEntities");

            migrationBuilder.DropTable(
                name: "Chambres",
                schema: "IzeEntities");

            migrationBuilder.DropTable(
                name: "TypeChambres",
                schema: "IzeEntities");
        }
    }
}
