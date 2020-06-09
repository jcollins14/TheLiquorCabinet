using Microsoft.EntityFrameworkCore.Migrations;

namespace TheLiquorCabinet.Migrations
{
    public partial class SecondMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cabinet__UserID__5165187F",
                table: "Cabinet");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Users_UserID",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorites__UserI__4F7CD00D",
                table: "Favorites1");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Favorites1",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Favorites1",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DrinkID",
                table: "Favorites1",
                newName: "DrinkId");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites1_UserID",
                table: "Favorites1",
                newName: "IX_Favorites1_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Cabinet",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "IngredID",
                table: "Cabinet",
                newName: "IngredId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Cabinet",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Cabinet_UserID",
                table: "Cabinet",
                newName: "IX_Cabinet_UserId");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Favorites1",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DrinkId",
                table: "Favorites1",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cabinet",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IngredId",
                table: "Cabinet",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Favorites1",
                table: "Favorites1",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cabinet",
                table: "Cabinet",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "IngredOnHand",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    IngredID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredOnHand", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Cabinet__UserID__5441852A",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredOnHand_UserID",
                table: "IngredOnHand",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabinet_Users1_UserId",
                table: "Cabinet",
                column: "UserId",
                principalTable: "Users1",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Favorites__UserI__571DF1D5",
                table: "Favorites",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites1_Users1_UserId",
                table: "Favorites1",
                column: "UserId",
                principalTable: "Users1",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabinet_Users1_UserId",
                table: "Cabinet");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorites__UserI__571DF1D5",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites1_Users1_UserId",
                table: "Favorites1");

            migrationBuilder.DropTable(
                name: "IngredOnHand");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Favorites1",
                table: "Favorites1");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cabinet",
                table: "Cabinet");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Favorites1",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "DrinkId",
                table: "Favorites1",
                newName: "DrinkID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Favorites1",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Favorites1_UserId",
                table: "Favorites1",
                newName: "IX_Favorites1_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cabinet",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "IngredId",
                table: "Cabinet",
                newName: "IngredID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cabinet",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cabinet_UserId",
                table: "Cabinet",
                newName: "IX_Cabinet_UserID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Favorites1",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DrinkID",
                table: "Favorites1",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "Cabinet",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "IngredID",
                table: "Cabinet",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ABV = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAlcoholic = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ingredient_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_UserID",
                table: "Ingredient",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK__Cabinet__UserID__5165187F",
                table: "Cabinet",
                column: "UserID",
                principalTable: "Users1",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Users_UserID",
                table: "Favorites",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Favorites__UserI__4F7CD00D",
                table: "Favorites1",
                column: "UserID",
                principalTable: "Users1",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
