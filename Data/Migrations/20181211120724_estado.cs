using Microsoft.EntityFrameworkCore.Migrations;

namespace BBKBootCamp.Data.Migrations
{
    public partial class estado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "TablaDeEntrevista",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "TablaDeEntrevista");
        }
    }
}
