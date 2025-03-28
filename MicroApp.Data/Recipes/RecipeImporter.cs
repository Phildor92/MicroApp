using System.Text.Json;
using MicroApp.Data.Recipes.Models;

namespace MicroApp.Data.Recipes;

public class RecipeImporter
{
    public static List<Recipe> ImportRecipes(string json)
    {
        return JsonSerializer.Deserialize<List<Recipe>>(json) ?? [];
    }

    public static string ExportRecipes(List<Recipe> recipes)
    {
        return JsonSerializer.Serialize(recipes);
    }
}