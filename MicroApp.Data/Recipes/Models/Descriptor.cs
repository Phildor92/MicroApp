using System.ComponentModel.DataAnnotations;

namespace MicroApp.Data.Recipes.Models;

public class Descriptor : Model
{
    [MaxLength(80)]
    public required string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}