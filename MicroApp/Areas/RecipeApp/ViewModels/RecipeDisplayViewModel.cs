using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroApp.Areas.Home.ViewModels;
using MicroApp.Data.Recipes.Models;
using MicroApp.Data.Recipes.Repositories;
using MicroApp.Lib.Logging;
using Microsoft.Extensions.Logging;

namespace MicroApp.Areas.RecipeApp.ViewModels;

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
    public ICommand BackCommand { get; set; }

    public RecipeDisplayViewModel(ILogger<RecipeDisplayViewModel> logger)
    {
        _logger = logger;
        
        var recipeIdRequest = WeakReferenceMessenger.Default.Send<ViewModelRequestIdMessage>();
        
        if(!recipeIdRequest.HasReceivedResponse)
            return;
        
        if(recipeIdRequest.Response == 0)
           return;
        
        _recipe = _recipeRepository.GetModelById(recipeIdRequest.Response) ?? new()
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
        BackCommand = new RelayCommand( () => WeakReferenceMessenger.Default.Send(new ViewModelChangedMessage(typeof(RecipeListViewModel), 0)));
    }

    public RecipeDisplayViewModel()
    {
        
    }
}