using System.ComponentModel.DataAnnotations;

namespace MicroApp.Data.Recipes.Models;

public class Step : Model
{
    [MaxLength(500)]
    public required string Description { get; set; }
    [Required, Range(0, int.MaxValue)]
    public required int Order { get; set; }

    public override string Key => Description;

    public override string ToString()
    {
        return $"{Order}: {Description}";
    }
}