using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using MicroApp.Data.Recipes.Models;

namespace MicroApp.Converters;

public class ListToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IList models)
            return value;

        return string.Join(", ", models.Cast<Model>());
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}