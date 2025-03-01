using System.Collections.ObjectModel;
using MicroApp.Data.Recipes.Abstractions;
using MicroApp.Data.Recipes.Context;
using MicroApp.Data.Recipes.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroApp.Data.Recipes.Repositories;

public class RecipeRepository : IRepository<Recipe>
{
    private readonly RecipeDbContext _dbContext = new();

    public ObservableCollection<Recipe> GetAllModels()
    {
        _dbContext.Recipes
            .Include(x => x.Ingredients)
                .ThenInclude(x => x.IngredientQuantity)
            .Include(x => x.Servings)
            .Include(x => x.Temperature)
            .Include(x=> x.Keywords)
            .Include(x=> x.Durations)
            .Include(x => x.Steps).Load(); 
        return _dbContext.Recipes.Local.ToObservableCollection();
    }

    public Recipe? GetModelById(int id)
    {
        _dbContext.Recipes.Load(); 
        return _dbContext.Recipes.Local.ToObservableCollection().FirstOrDefault(x => x.Id == id);
    }

    public void AddModel(Recipe model)
    {
        _dbContext.Recipes.Add(model);
        _dbContext.SaveChanges();
    }

    public void UpdateModel(Recipe model)
    {
        _dbContext.Recipes.Update(model);
        _dbContext.SaveChanges();
    }

    public void DeleteModel(Recipe model)
    {
        _dbContext.Recipes.Remove(model);
        _dbContext.SaveChanges();
    }
}