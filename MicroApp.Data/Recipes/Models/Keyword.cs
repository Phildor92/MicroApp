using System.ComponentModel.DataAnnotations;

namespace MicroApp.Data.Recipes.Models;

public class Keyword : Model
{
    [MaxLength(120)]
    public required string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}