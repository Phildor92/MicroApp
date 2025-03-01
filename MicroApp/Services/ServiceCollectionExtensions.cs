using System;
using System.IO;
using MicroApp.Data.Recipes.Context;
using MicroApp.Lib.Configuration;
using MicroApp.ViewModels;
using MicroApp.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

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
        collection.AddScoped<RecipeDisplayViewModel>();
        collection.AddScoped<RecipeDisplayView>();
        collection.AddScoped<RecipeEditViewModel>();
        collection.AddScoped<RecipeEditView>();
        collection.AddScoped<MainViewModel>();
        collection.AddScoped<MainView>();
        collection.AddSingleton<IConfigService, ConfigService>();
        collection.AddDbContext<RecipeDbContext>();
    }
}