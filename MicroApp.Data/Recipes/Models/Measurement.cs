using System.ComponentModel.DataAnnotations;

namespace MicroApp.Data.Recipes.Models;

public class Measurement : Model
{
    [MaxLength(120)]
    public string? Description { get; set; }
    [MaxLength(50), Required]
    public string Unit { get; set; }
    [Required]
    public int Amount { get; set; }

    public override string ToString()
    {
        return $"{Description}{(string.IsNullOrEmpty(Description) ? string.Empty : ": ")}{Amount} {Unit}";
    }
}