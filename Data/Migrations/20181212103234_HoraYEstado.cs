using Microsoft.EntityFrameworkCore.Migrations;

namespace BBKBootCamp.Data.Migrations
{
    public partial class HoraYEstado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "HoraDisponible",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "HoraDisponible");
        }
    }
}
