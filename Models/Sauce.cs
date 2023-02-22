using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace grocery_api.Models;

public class Sauce
{
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string? Name { get; set; }
    public bool IsVegan { get; set; }
    [JsonIgnore]
    public ICollection<Pizza>? Pizzas { get; set; }
}