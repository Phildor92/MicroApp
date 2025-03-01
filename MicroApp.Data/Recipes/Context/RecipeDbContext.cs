using MicroApp.Data.Recipes.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroApp.Data.Recipes.Context;

public class RecipeDbContext : DbContext
{
    public DbSet<Recipe> Recipes { get; set; }

    public string DbPath { get; }

    public RecipeDbContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "MicroApp", "recipes.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}