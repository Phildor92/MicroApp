using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using MicroApp.Areas.Home.Views;
using MicroApp.Data.Recipes.Context;
using MicroApp.Lib.Configuration;
using MicroApp.Areas.RecipeApp.Views;
using MicroApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Devices;
using Serilog;
using MainViewModel = MicroApp.Areas.Home.ViewModels.MainViewModel;

namespace MicroApp;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
        // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
        DisableAvaloniaDataAnnotationValidation();
            
        var collection = new ServiceCollection();
        collection.AddCommonServices();

        _serviceProvider = collection.BuildServiceProvider();
        var dbContext = _serviceProvider.GetRequiredService<RecipeDbContext>();
        var config = _serviceProvider.GetRequiredService<IConfigService>();
        var dataPath = config.GetDataPath(DeviceInfo.Current.Platform.ToString());
        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);
        
        dbContext.Database.Migrate();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.ShutdownRequested += DesktopOnShutdownRequested;
            desktop.MainWindow = new MainWindow
            {
                DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
            };
        }

        Resources[typeof(IServiceProvider)] = _serviceProvider;
        
        DataTemplates.Add(_serviceProvider.GetRequiredService<ViewLocator>());
        base.OnFrameworkInitializationCompleted();
    }

    private void DesktopOnShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        Log.CloseAndFlush();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
    
    
}