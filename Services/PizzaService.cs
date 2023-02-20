using Microsoft.EntityFrameworkCore;
using grocery_api.Models;
using grocery_api.Data;

namespace grocery_api.Services;

public interface IPizzaService
{
    IEnumerable<Pizza> GetAll();
    Pizza? GetById(int id);
    Pizza Create(Pizza newPizza);
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

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauce)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public Pizza Create(Pizza newPizza)
    {
        _context.Pizzas.Add(newPizza);
        _context.SaveChanges();

        return newPizza;
    }

    public void Delete(int id)
    {
        var pizzaToDelete = _context.Pizzas.Find(id);
        if (pizzaToDelete is not null)
        {
            _context.Pizzas.Remove(pizzaToDelete);
            _context.SaveChanges();
        }        
    }

    public void Update(Pizza pizza)
    {
        var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        if(index == -1)
            return;

        Pizzas[index] = pizza;
    }
    
    // Update Sauce
    public void UpdateSauce(int pizzaId, int sauceId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
        var sauceToUpdate = _context.Sauces.Find(sauceId);

        if (pizzaToUpdate is null || sauceToUpdate is null)
        {
            throw new InvalidOperationException("Pizza or sauce does not exist");
        }

        pizzaToUpdate.Sauce = sauceToUpdate;

        _context.SaveChanges();
    }
    
    // Add Topping
    public void AddTopping(int pizzaId, int toppingId)
    {
        var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
        var toppingToAdd = _context.Toppings.Find(toppingId);

        if (pizzaToUpdate is null || toppingToAdd is null)
        {
            throw new InvalidOperationException("Pizza or topping does not exist");
        }

        if(pizzaToUpdate.Toppings is null)
        {
            pizzaToUpdate.Toppings = new List<Topping>();
        }

        pizzaToUpdate.Toppings.Add(toppingToAdd);

        _context.SaveChanges();
    }
}