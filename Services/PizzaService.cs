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
    

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauces)
            .AsNoTracking()
            .ToList();
    }

    public Pizza? GetById(int id)
    {
        return _context.Pizzas
            .Include(p => p.Toppings)
            .Include(p => p.Sauces)
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
}