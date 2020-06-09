using Microsoft.EntityFrameworkCore.Migrations;

namespace CocktailDbTools.Migrations
{
    public partial class cocktailtestDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrinkDb",
                columns: table => new
                {
                    idDrink = table.Column<string>(nullable: false),
                    strDrink = table.Column<string>(nullable: true),
                    strIngredient1 = table.Column<string>(nullable: true),
                    strIngredient2 = table.Column<string>(nullable: true),
                    strIngredient3 = table.Column<string>(nullable: true),
                    strIngredient4 = table.Column<string>(nullable: true),
                    strIngredient5 = table.Column<string>(nullable: true),
                    strIngredient6 = table.Column<string>(nullable: true),
                    strIngredient7 = table.Column<string>(nullable: true),
                    strIngredient8 = table.Column<string>(nullable: true),
                    strIngredient9 = table.Column<string>(nullable: true),
                    strIngredient10 = table.Column<string>(nullable: true),
                    strIngredient11 = table.Column<string>(nullable: true),
                    strIngredient12 = table.Column<string>(nullable: true),
                    strIngredient13 = table.Column<string>(nullable: true),
                    strIngredient14 = table.Column<string>(nullable: true),
                    strIngredient15 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkDb", x => x.idDrink);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkDb");
        }
    }
}
