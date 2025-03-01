using System.ComponentModel.DataAnnotations;

namespace MicroApp.Data.Recipes.Models;

public class Range : Model
{
    [Range(1, int.MaxValue)]
    public int MinAmount { get; set; }
    [Range(1, int.MaxValue)]
    public int MaxAmount { get; set; }

    public override string ToString()
    {
        return $"{MinAmount}-{MaxAmount}";
    }
}