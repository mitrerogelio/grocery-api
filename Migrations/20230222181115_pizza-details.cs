using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grocery_api.Migrations
{
    /// <inheritdoc />
    public partial class pizzadetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizzas_Sauces_SauceId",
                table: "Pizzas");

            migrationBuilder.DropIndex(
                name: "IX_Pizzas_SauceId",
                table: "Pizzas");

            migrationBuilder.DropColumn(
                name: "SauceId",
                table: "Pizzas");

            migrationBuilder.RenameColumn(
                name: "calories",
                table: "Toppings",
                newName: "Calories");

            migrationBuilder.AlterColumn<int>(
                name: "Calories",
                table: "Toppings",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "PizzaSauce",
                columns: table => new
                {
                    PizzasId = table.Column<int>(type: "INTEGER", nullable: false),
                    SaucesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaSauce", x => new { x.PizzasId, x.SaucesId });
                    table.ForeignKey(
                        name: "FK_PizzaSauce_Pizzas_PizzasId",
                        column: x => x.PizzasId,
                        principalTable: "Pizzas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PizzaSauce_Sauces_SaucesId",
                        column: x => x.SaucesId,
                        principalTable: "Sauces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaSauce_SaucesId",
                table: "PizzaSauce",
                column: "SaucesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PizzaSauce");

            migrationBuilder.RenameColumn(
                name: "Calories",
                table: "Toppings",
                newName: "calories");

            migrationBuilder.AlterColumn<decimal>(
                name: "calories",
                table: "Toppings",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "SauceId",
                table: "Pizzas",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pizzas_SauceId",
                table: "Pizzas",
                column: "SauceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizzas_Sauces_SauceId",
                table: "Pizzas",
                column: "SauceId",
                principalTable: "Sauces",
                principalColumn: "Id");
        }
    }
}
