using System.Collections.Generic;
using grocery_api.Models;

namespace grocery_api.Services;

public interface IPizzaService
{
    List<Pizza> GetAll();
    Pizza? Get(int id);
    void Add(Pizza pizza);
    void Delete(int id);
    void Update(Pizza pizza);
}
public class PizzaService : IPizzaService
{
    private static List<Pizza> Pizzas { get; }
    private static int nextId = 3;

    static PizzaService()
    {
        Pizzas = new List<Pizza>
        {
            new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
            new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true },
            new Pizza { Id = 3, Name = "Margherita", IsGlutenFree = false },
            new Pizza { Id = 4, Name = "Pepperoni", IsGlutenFree = false },
            new Pizza { Id = 5, Name = "Hawaiian", IsGlutenFree = false },
            new Pizza { Id = 6, Name = "Meat Lovers", IsGlutenFree = false },
            new Pizza { Id = 7, Name = "BBQ Chicken", IsGlutenFree = false },
            new Pizza { Id = 8, Name = "Mushroom and Olive", IsGlutenFree = true },
            new Pizza { Id = 9, Name = "Spinach and Feta", IsGlutenFree = true }
        };
    }

    public List<Pizza> GetAll() => Pizzas;

    public Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    public void Add(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.Add(pizza);
    }

    public void Delete(int id)
    {
        var pizza = Get(id);
        if(pizza is null)
            return;

        Pizzas.Remove(pizza);
    }

    public void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if(index == -1)
            return;

        Pizzas[index] = pizza;
    }
}