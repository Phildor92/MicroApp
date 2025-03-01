using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Squash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Range",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MinAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxAmount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Range", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Descriptor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    IngredientId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    IngredientQuantityId = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomQuantity = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keyword",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Measurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Source = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    TemperatureId = table.Column<int>(type: "INTEGER", nullable: true),
                    ServingsId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Measurement_TemperatureId",
                        column: x => x.TemperatureId,
                        principalTable: "Measurement",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recipe_Range_ServingsId",
                        column: x => x.ServingsId,
                        principalTable: "Range",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    RecipeId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Step_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Descriptor_IngredientId",
                table: "Descriptor",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_IngredientQuantityId",
                table: "Ingredient",
                column: "IngredientQuantityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_RecipeId",
                table: "Ingredient",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyword_RecipeId",
                table: "Keyword",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurement_RecipeId",
                table: "Measurement",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_ServingsId",
                table: "Recipe",
                column: "ServingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_TemperatureId",
                table: "Recipe",
                column: "TemperatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_RecipeId",
                table: "Step",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Descriptor_Ingredient_IngredientId",
                table: "Descriptor",
                column: "IngredientId",
                principalTable: "Ingredient",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Measurement_IngredientQuantityId",
                table: "Ingredient",
                column: "IngredientQuantityId",
                principalTable: "Measurement",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Recipe_RecipeId",
                table: "Ingredient",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Keyword_Recipe_RecipeId",
                table: "Keyword",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Measurement_Recipe_RecipeId",
                table: "Measurement",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Measurement_TemperatureId",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "Descriptor");

            migrationBuilder.DropTable(
                name: "Keyword");

            migrationBuilder.DropTable(
                name: "Step");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "Measurement");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Range");
        }
    }
}
