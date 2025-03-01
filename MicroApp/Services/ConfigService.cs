using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Avalonia;
using MicroApp.Services;
using Microsoft.Extensions.Configuration;

namespace MicroApp.Lib.Configuration;

public class ConfigService : IConfigService
{
    private IConfigurationRoot _config;
    public ConfigService()
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("appsettings.json");
        var configBuilder = new ConfigurationBuilder();
        if(stream != null)
            configBuilder.AddJsonStream(stream);
        _config = configBuilder.AddJsonFile("appsettings.json", optional:true).AddEnvironmentVariables().Build();
    }

    public string GetDataPath(string currentPlatform)
    {
        var dataFolder = Environment.SpecialFolder.LocalApplicationData;

        if (currentPlatform == "Android")
        {
            //dataFolder = App
        }
        else if (currentPlatform == "Windows")
        {
        }
        //var dataPath = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(dataFolder);
        var dataPath = _config.GetSection("Settings").Get<Settings>()?.DataPath;
        return Path.Join(path, dataPath ?? "MicroApp");
    }
}

public sealed class Settings
{
    public required string DataPath { get; set; }
}