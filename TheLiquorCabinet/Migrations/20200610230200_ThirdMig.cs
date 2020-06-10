using Microsoft.EntityFrameworkCore.Migrations;

namespace TheLiquorCabinet.Migrations
{
    public partial class ThirdMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Favorites__UserI__571DF1D5",
                table: "Favorites");

            migrationBuilder.AddForeignKey(
                name: "FK__Favorites__UserID__571DF1D5",
                table: "Favorites",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Favorites__UserID__571DF1D5",
                table: "Favorites");

            migrationBuilder.AddForeignKey(
                name: "FK__Favorites__UserI__571DF1D5",
                table: "Favorites",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
