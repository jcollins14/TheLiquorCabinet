using Microsoft.EntityFrameworkCore.Migrations;

namespace TheLiquorCabinet.Migrations
{
    public partial class Mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrinkDb",
                columns: table => new
                {
                    IdDrink = table.Column<string>(nullable: false),
                    StrDrink = table.Column<string>(nullable: true),
                    StrIngredient1 = table.Column<string>(nullable: true),
                    StrIngredient2 = table.Column<string>(nullable: true),
                    StrIngredient3 = table.Column<string>(nullable: true),
                    StrIngredient4 = table.Column<string>(nullable: true),
                    StrIngredient5 = table.Column<string>(nullable: true),
                    StrIngredient6 = table.Column<string>(nullable: true),
                    StrIngredient7 = table.Column<string>(nullable: true),
                    StrIngredient8 = table.Column<string>(nullable: true),
                    StrIngredient9 = table.Column<string>(nullable: true),
                    StrIngredient10 = table.Column<string>(nullable: true),
                    StrIngredient11 = table.Column<string>(nullable: true),
                    StrIngredient12 = table.Column<string>(nullable: true),
                    StrIngredient13 = table.Column<string>(nullable: true),
                    StrIngredient14 = table.Column<string>(nullable: true),
                    StrIngredient15 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkDb", x => x.IdDrink);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkDb");
        }
    }
}
