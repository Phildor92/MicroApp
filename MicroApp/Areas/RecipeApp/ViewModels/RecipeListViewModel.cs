using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MicroApp.Areas.Home.ViewModels;
using MicroApp.Data.Recipes.Models;
using MicroApp.Data.Recipes.Repositories;
using MicroApp.Lib.Logging;
using Microsoft.Extensions.Logging;

namespace MicroApp.Areas.RecipeApp.ViewModels;

public class RecipeListViewModel : ViewModel
{
    public ICommand EditRecipeCommand { get; set; }
    public ICommand ViewRecipeCommand { get; set; }
    public ICommand ImportRecipesCommand { get; set; }
    
    private readonly RecipeRepository _recipeRepository = new();
    private ObservableCollection<Recipe> _recipes;
    private ILogger<RecipeListViewModel> _logger;

    public ObservableCollection<Recipe> Recipes
    {
        get => _recipes;
        set => SetProperty(ref _recipes, value);
    }


    public RecipeListViewModel(ILogger<RecipeListViewModel> logger)
    {
        _logger = logger;
        _recipes = new(_recipeRepository.GetAllModels());
        
        EditRecipeCommand = new RelayCommand<int>(r => { WeakReferenceMessenger.Default.Send(new ViewModelChangedMessage(typeof(RecipeEditViewModel), r)); });
        ViewRecipeCommand = new RelayCommand<int>(r => { WeakReferenceMessenger.Default.Send(new ViewModelChangedMessage(typeof(RecipeDisplayViewModel), r)); });
        ImportRecipesCommand = new AsyncRelayCommand(ExecuteImportRecipes);
    }

    public RecipeListViewModel()
    {
        
    }

    private async Task ExecuteImportRecipes(CancellationToken token)
    {
        try
        {
            var file = await DoOpenFilePickerAsync();
            if (file is null) return;

            await using var readStream = await file.OpenReadAsync();
            using var reader = new StreamReader(readStream);
            var result = await reader.ReadToEndAsync(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _logger.Error(e.ToString());
        }
    }

    //TODO: move to service
    private async Task<IStorageFile?> DoOpenFilePickerAsync()
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop || desktop.MainWindow?.StorageProvider is not { } provider)
        {
            // if(TopLevel.GetTopLevel(Application.Current) is TopLevel topLevel)
            //     provider = topLevel.StorageProvider;
            //
            // else
            //      return null;
            return null;
        }
        
        var files = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open Text File",
            AllowMultiple = false,
        });

        return files[0];
    }
}