using System;
using System.IO;
using MicroApp.Areas.Home.Views;
using MicroApp.Areas.RecipeApp.ViewModels;
using MicroApp.Data.Recipes.Context;
using MicroApp.Lib.Configuration;
using MicroApp.Areas.RecipeApp.ViewModels;
using MicroApp.Areas.RecipeApp.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using MainViewModel = MicroApp.Areas.Home.ViewModels.MainViewModel;
using RecipeDisplayViewModel = MicroApp.Areas.RecipeApp.ViewModels.RecipeDisplayViewModel;

namespace MicroApp.Services;

public static class ServiceCollectionExtensions
{
    public static void AddCommonServices(this IServiceCollection collection)
    {
        var localAppDataFolder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(localAppDataFolder);
        var dataPath = Path.Join(path, "MicroApp"); //TODO: move to desktop-only?
        collection.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSerilog(new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(Path.Join(dataPath, "app.log"), rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                    .CreateLogger());
            loggingBuilder.AddConsole();
        });

        collection.AddSingleton<ViewLocator>();
        collection.AddViews();
        collection.AddViewModels();
        collection.AddSingleton<IConfigService, ConfigService>();
        collection.AddDbContext<RecipeDbContext>();
    }

    private static void AddViews(this IServiceCollection collection)
    {
        var types = typeof(MainView).Assembly.ExportedTypes;
        foreach (var type in types)
        {
            if (type.Name.EndsWith("View") && !type.IsAbstract)
            {
                collection.AddScoped(type);
            }
        }
    }
    
    private static void AddViewModels(this IServiceCollection collection)
    {
        var types = typeof(MainViewModel).Assembly.ExportedTypes;
        foreach (var type in types)
        {
            if (type.Name.EndsWith("ViewModel") && !type.IsAbstract)
            {
                collection.AddScoped(type);
            }
        }
    }
}