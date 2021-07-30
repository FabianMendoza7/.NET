using Microsoft.EntityFrameworkCore.Migrations;

namespace Payments.Migrations
{
    public partial class ProductoExistenciaInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExistenciaInicial",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExistenciaInicial",
                table: "Productos");
        }
    }
}
