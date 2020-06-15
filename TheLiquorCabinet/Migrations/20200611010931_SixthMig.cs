using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheLiquorCabinet.Migrations
{
    public partial class SixthMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cabinet_Users_UserId",
                table: "Cabinet");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorites__UserID__571DF1D5",
                table: "Favorites");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "IngredOnHand");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

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

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK__Users__1788CCACD3079F3C",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK__Cabinet__UserID__5441852A",
                table: "Cabinet",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Favorites__UserID__571DF1D5",
                table: "Favorites",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Cabinet__UserID__5441852A",
                table: "Cabinet");

            migrationBuilder.DropForeignKey(
                name: "FK__Favorites__UserID__571DF1D5",
                table: "Favorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Users__1788CCACD3079F3C",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Users");

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

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "IngredOnHand",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACD3079F3C", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DrinkId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites1_Users1_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredOnHand_UserID",
                table: "IngredOnHand",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cabinet_Users_UserId",
                table: "Cabinet",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Favorites__UserID__571DF1D5",
                table: "Favorites",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
