using Microsoft.EntityFrameworkCore;
using grocery_api.Models;
using grocery_api.Data;

namespace grocery_api.Services;

public interface IPizzaService
{
    IEnumerable<Pizza> GetAll();
    Pizza? GetById(int id);
    void Add(Pizza pizza);
    void Delete(int id);
    void Update(Pizza pizza);
}
public class PizzaService : IPizzaService
{
    private readonly PizzaContext _context;
    public PizzaService(PizzaContext context)
    {
        _context = context;
    }
    private static List<Pizza> Pizzas { get; }
    private static int _nextId = 3;

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

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas
            .AsNoTracking()
            .ToList();
    }

    public Pizza? GetById(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    public void Add(Pizza pizza)
    {
        pizza.Id = _nextId++;
        Pizzas.Add(pizza);
    }

    public void Delete(int id)
    {
        var pizza = GetById(id);
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