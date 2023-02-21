using grocery_api.Models;
using grocery_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace grocery_api.Controllers;

[ApiController]
// It's implied that the Pizza parameter will be found in the request body since this is an ApiController
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IPizzaService _pizzaService;

    public PizzaController(IPizzaService pizzaService)
    {
        _pizzaService = pizzaService;
    }

    // GET all pizzas
    [HttpGet]
    public ActionResult<IEnumerable<Pizza>> GetAll()
    {
        var pizzas = _pizzaService.GetAll();
        if (pizzas == null)
        {
            return NotFound();
        }
        return Ok(pizzas);
    }

    // GET single pizza by ID
    [HttpGet("{id:int}")]
    public ActionResult<Pizza> GetById(int id)
    {
        var pizza = _pizzaService.GetById(id);

        if(pizza == null)
            return NotFound();

        return pizza;
    }

    // POST action
    [HttpPost]
    public IActionResult Create(Pizza pizza)
    {            
        _pizzaService.Create(pizza);
        return CreatedAtAction(nameof(GetById), new { id = pizza.Id }, pizza);
    }

    // PUT action
    [HttpPut("{id}")]
    public IActionResult Update(int id, Pizza pizza)
    {
        if (id != pizza.Id)
            return BadRequest();
           
        var existingPizza = _pizzaService.GetById(id);
        if(existingPizza is null)
            return NotFound();
   
        _pizzaService.Update(pizza);           
   
        return NoContent();
    }

    // DELETE action
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pizza = _pizzaService.GetById(id);
   
        if (pizza is null)
            return NotFound();
       
        _pizzaService.Delete(id);
   
        return NoContent();
    }
}
