using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroApp.Areas.Home.ViewModels;
using MicroApp.Data.Recipes.Models;
using MicroApp.Data.Recipes.Repositories;
using MicroApp.Lib.Logging;
using Microsoft.Extensions.Logging;

namespace MicroApp.Areas.RecipeApp.ViewModels;

public class RecipeEditViewModel : ViewModel
{
    private readonly RecipeRepository _recipeRepository = new();//TODO: DI
    private Recipe _recipe;
    private readonly ILogger _logger;
    private ObservableCollection<Recipe> _recipes;
    private ObservableCollection<Ingredient> _ingredients;
    private ObservableCollection<Step> _steps;
    private ObservableCollection<Measurement> _durations;
    private ObservableCollection<Keyword> _keywords;
    private Measurement? _temperature;
    private Range? _servings;

    public ObservableCollection<Recipe> Recipes
    {
        get => _recipes;
        set => SetProperty(ref _recipes, value);
    }

    public ObservableCollection<Ingredient> Ingredients
    {
        get => _ingredients;
        set => SetProperty(ref _ingredients, value);
    }

    public ObservableCollection<Step> Steps
    {
        get => _steps;
        set => SetProperty(ref _steps, value);
    }

    public ObservableCollection<Measurement> Durations
    {
        get => _durations;
        set => SetProperty(ref _durations, value);
    }

    public ObservableCollection<Keyword> Keywords
    {
        get => _keywords;
        set => SetProperty(ref _keywords, value);
    }

    public Recipe Recipe
    {
        get => _recipe;
        private set => SetProperty(ref _recipe, value);
    }

    public Measurement? Temperature
    {
        get => _temperature;
        set => SetProperty(ref _temperature, value);
    }

    public Range? Servings
    {
        get => _servings;
        set => SetProperty(ref _servings, value);
    }

    public ICommand AddIngredientCommand { get; set; }
    public ICommand AddStepCommand { get; set; }
    public ICommand AddDurationCommand { get; set; }
    public ICommand AddKeywordCommand { get; set; }
    public ICommand SaveChangesCommand { get; set; }
    public ICommand NewRecipeCommand { get; set; }
    public ICommand AddTemperatureCommand { get; set; }
    public ICommand AddServingsCommand { get; set; }
    public ICommand BackCommand { get; set; }

    public RecipeEditViewModel(ILogger<RecipeEditViewModel> logger)
    {
        _logger = logger;
        Recipes = _recipeRepository.GetAllModels();
        
        var recipeIdRequest = WeakReferenceMessenger.Default.Send<ViewModelRequestIdMessage>();
        
        if(!recipeIdRequest.HasReceivedResponse)
            return;
        
        Recipe = _recipeRepository.GetModelById(recipeIdRequest.Response) ?? new()
        {
            Title = "Title",
            Keywords = [],
            Ingredients = [],
            Steps = [],
            Durations = []
        };
        Ingredients = new(Recipe.Ingredients);
        Steps = new(Recipe.Steps.OrderBy(s => s.Order));
        Durations = new(Recipe.Durations);
        Keywords = new(Recipe.Keywords);
        Temperature = Recipe.Temperature;
        Servings = Recipe.Servings;
        
        AddIngredientCommand = new RelayCommand(() =>
        {
            Ingredients.Add(new(){Name = "", Descriptors = [], IngredientQuantity = new(){Amount = 0, Unit = "", Description = ""}});
        });
        AddStepCommand = new RelayCommand(() =>
        {
            Steps.Add(new(){Description = "", Order = Steps.Count + 1});
        });
        AddDurationCommand = new RelayCommand(() =>
        {
            Durations.Add(new(){Amount = 0, Unit = "", Description = ""});
        });
        AddKeywordCommand = new RelayCommand(() =>
        {
            Keywords.Add(new(){Name = ""});
        });
        AddTemperatureCommand = new RelayCommand(() => { Temperature = new(){Amount = 0, Unit = "", Description = ""}; });
        AddServingsCommand = new RelayCommand(() => { Servings = new() { MinAmount = 2, MaxAmount = 4 }; });
        SaveChangesCommand = new RelayCommand(SaveChanges);
        NewRecipeCommand = new RelayCommand(NewRecipe);
        BackCommand = new RelayCommand( () => WeakReferenceMessenger.Default.Send(new ViewModelChangedMessage(typeof(RecipeListViewModel), 0)));
    }

    public RecipeEditViewModel()
    {
        
    }

    private void NewRecipe()
    {
        Recipe = new()
        {
            Title = "",
            Keywords = [],
            Ingredients = [],
            Steps = [],
            Durations = []
        };
        Ingredients = new(Recipe.Ingredients);
        Steps = new(Recipe.Steps.OrderBy(s => s.Order));
        Durations = new(Recipe.Durations);
        Keywords = new(Recipe.Keywords);
        Temperature = Recipe.Temperature;
        Servings = Recipe.Servings;
        
    }

    public void SaveChanges()
    {
        var validationResults = new List<ValidationResult>();
        if (!Validator.TryValidateObject(Recipe, new(Recipe), validationResults))
        {
            foreach (var validationResult in validationResults)
            {
                _logger.Error(validationResult.ErrorMessage ?? string.Empty);
            }
            return;
        }

        if (Ingredients.Any(ingredient => !Validator.TryValidateObject(ingredient, new(ingredient), validationResults)))
        {
            foreach (var validationResult in validationResults)
            {
                _logger.Error(validationResult.ErrorMessage ?? string.Empty);
            }
            return;
        }
        
        if (Steps.Any(step => !Validator.TryValidateObject(step, new(step), validationResults)))
        {
            foreach (var validationResult in validationResults)
            {
                _logger.Error(validationResult.ErrorMessage ?? string.Empty);
            }
            return;
        }
        
        if (Durations.Any(duration => !Validator.TryValidateObject(duration, new(duration), validationResults)))
        {
            foreach (var validationResult in validationResults)
            {
                _logger.Error(validationResult.ErrorMessage ?? string.Empty);
            }
            return;
        }
        
        foreach (var keyword in Keywords)
        {
            if (!Validator.TryValidateObject(keyword, new(keyword), validationResults))
            {
                foreach (var validationResult in validationResults)
                {
                    _logger.Error(validationResult.ErrorMessage ?? string.Empty);
                }
                return;
            }
        }

        if (Temperature != null && !Validator.TryValidateObject(Temperature, new(Temperature), validationResults))
        {
            foreach (var validationResult in validationResults)
            {
                _logger.Error(validationResult.ErrorMessage ?? string.Empty);
            }
            return;
        }

        if (Servings != null && !Validator.TryValidateObject(Servings, new(Servings), validationResults))
        {
            foreach (var validationResult in validationResults)
            {
                _logger.Error(validationResult.ErrorMessage ?? string.Empty);
            }
            return;
        }
        
        Recipe.Durations = Durations;
        
        //Recipe.Steps = Steps;
        var stepCount = 0;
        foreach (var step in Steps)
        {
            step.Order = stepCount++;
            Recipe.Steps.Add(step);
        }
        
        Recipe.Ingredients = Ingredients;
        Recipe.Keywords = Keywords;
        Recipe.Temperature = Temperature;
        Recipe.Servings = Servings;
        
        if(Recipe.Id == 0)
            _recipeRepository.AddModel(Recipe);
        else
            _recipeRepository.UpdateModel(Recipe);
    }
}