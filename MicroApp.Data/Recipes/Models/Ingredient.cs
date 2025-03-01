using System.ComponentModel.DataAnnotations;

namespace MicroApp.Data.Recipes.Models;

public class Ingredient : Model
{
    [MaxLength(120), Required]
    public required string Name { get; set; }
    public required List<Descriptor> Descriptors { get; set; }
    public Measurement? IngredientQuantity { get; set; } = new();
    [MaxLength(120)]
    public string? CustomQuantity { get; set; }

    public override string ToString()
    {
        // try
        // {
        //     var quantity = IngredientQuantity == null ? CustomQuantity : IngredientQuantity.ToString();
        //     var descriptors = Descriptors.Count == 0 ? string.Empty : $" - {string.Join(", ", Descriptors)}";
        //     return $"{quantity} {Name}{descriptors}";
        // }
        // catch (Exception e)
        // {
        //     File.WriteAllText("C:\\log\\error.txt", e.ToString());
        // }

        return Name;
        //return $"{(IngredientQuantity == null ? CustomQuantity : IngredientQuantity)}{Name}{(Descriptors.Count == 0 ? string.Empty : $" - {string.Join(", ", Descriptors)}")}";
    }
}