using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroApp.Data.Recipes.Models;

[Table("Recipe")]
public class Recipe : Model
{
    [MaxLength(80), Required]
    public required string Title { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    [MaxLength(120)]
    public string? Source { get; set; }
    public required IList<Keyword> Keywords { get; set; } = [];
    public required IList<Measurement> Durations { get; set; } = [];
    public required IList<Step> Steps { get; set; } = [];
    public required IList<Ingredient> Ingredients { get; set; } = [];
    public Measurement? Temperature { get; set; }
    public Range? Servings { get; set; } 
}