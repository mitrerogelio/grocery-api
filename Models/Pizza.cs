using System.ComponentModel.DataAnnotations;
namespace grocery_api.Models;

public class Pizza
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    public bool IsGlutenFree { get; set; }
    public ICollection<Sauce>? Sauces { get; init; }
    public ICollection<Topping>? Toppings { get; init; }
}