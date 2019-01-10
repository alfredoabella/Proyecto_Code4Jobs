using Microsoft.EntityFrameworkCore.Migrations;

namespace BBKBootCamp.Data.Migrations
{
    public partial class ProcesoSolicitante : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Proceso",
                table: "Solicitante",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Proceso",
                table: "Solicitante");
        }
    }
}
