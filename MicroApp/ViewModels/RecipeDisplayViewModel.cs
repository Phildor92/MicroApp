using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MicroApp.Data.Recipes.Models;
using MicroApp.Data.Recipes.Repositories;
using MicroApp.Lib.Logging;
using Microsoft.Extensions.Logging;

namespace MicroApp.ViewModels;

public partial class RecipeDisplayViewModel : ViewModel
{
    [ObservableProperty] private string _pageTitle = "This is the Recipe Page";
    private readonly RecipeRepository _recipeRepository = new();
    private readonly ILogger _logger;
    private Recipe _recipe;

    public Recipe Recipe
    {
        get => _recipe;
        private set => SetProperty(ref _recipe, value);
    }

    public string Keywords
    {
        get => string.Join(", ", Recipe.Keywords.Select(x => x.Name));
    }
    
    public ICommand EditRecipeCommand { get; set; }
    public ICommand NewRecipeCommand { get; set; }

    public RecipeDisplayViewModel(ILogger<RecipeDisplayViewModel> logger)
    {
        _logger = logger;
        _recipe = _recipeRepository.GetAllModels().FirstOrDefault() ?? new()
        {
            Title = "New Recipe",
            Keywords = [],
            Ingredients = [],
            Steps = [],
            Durations = []
        };
        
        _logger.Debug($"Loaded {_recipe.Title}");
        EditRecipeCommand = new RelayCommand(() => { });
        NewRecipeCommand = new RelayCommand(() => { });
    }

    public RecipeDisplayViewModel()
    {
        
    }
}