using System;
using Avalonia;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace MicroApp.Services;

public static class ResourceHostExtensions
{
    public static IServiceProvider GetServiceProvider(this IResourceHost control)
    {
        return (IServiceProvider?)Application.Current.FindResource(typeof(IServiceProvider)) ??
               throw new Exception("Expected service provider missing");
    }

    public static T CreateInstance<T>(this IResourceHost control)
    {
        return ActivatorUtilities.CreateInstance<T>(control.GetServiceProvider());
    }
}