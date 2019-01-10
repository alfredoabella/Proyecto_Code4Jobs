using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BBKBootCamp.Data.Migrations
{
    public partial class solicitante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Solicitante",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CorreoElectronico = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    SexoTipo = table.Column<string>(nullable: true),
                    Pregunta1 = table.Column<string>(nullable: true),
                    Pregunta2 = table.Column<string>(nullable: true),
                    Pregunta3 = table.Column<string>(nullable: true),
                    Pregunta4 = table.Column<string>(nullable: true),
                    Pregunta5 = table.Column<string>(nullable: true),
                    Pregunta6 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solicitante", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Solicitante");
        }
    }
}
