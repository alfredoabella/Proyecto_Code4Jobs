using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBKBootCamp.Data.Migrations
{
    public partial class casi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TablaDeEntrevista",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntrevistadoraId = table.Column<string>(nullable: true),
                    SolicitanteId = table.Column<int>(nullable: true),
                    HoraDisponibleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaDeEntrevista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablaDeEntrevista_AspNetUsers_EntrevistadoraId",
                        column: x => x.EntrevistadoraId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TablaDeEntrevista_HoraDisponible_HoraDisponibleId",
                        column: x => x.HoraDisponibleId,
                        principalTable: "HoraDisponible",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TablaDeEntrevista_Solicitante_SolicitanteId",
                        column: x => x.SolicitanteId,
                        principalTable: "Solicitante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TablaDeEntrevista_EntrevistadoraId",
                table: "TablaDeEntrevista",
                column: "EntrevistadoraId");

            migrationBuilder.CreateIndex(
                name: "IX_TablaDeEntrevista_HoraDisponibleId",
                table: "TablaDeEntrevista",
                column: "HoraDisponibleId");

            migrationBuilder.CreateIndex(
                name: "IX_TablaDeEntrevista_SolicitanteId",
                table: "TablaDeEntrevista",
                column: "SolicitanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TablaDeEntrevista");
        }
    }
}
