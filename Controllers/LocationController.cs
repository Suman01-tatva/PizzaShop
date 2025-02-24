using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;

namespace PizzaShop.Controllers;

public class LocationController : Controller
{
    private readonly PizzashopContext _context;

    public LocationController(PizzashopContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        var model = new Location
        {
            Countries = _context.Countries.ToList(),
            States = new List<State>(),
            Cities = new List<City>()
        };
        return View(model);
    }

    [HttpGet]
    public JsonResult GetCountries()
    {
        var countries = _context.Countries.ToList();
        return Json(countries);
    }

    [HttpGet]
    public JsonResult GetStates(int countryId)
    {
        var states = _context.States.Where(s => s.CountryId == countryId).ToList();
        return Json(states);
    }

    [HttpGet]
    public JsonResult GetCities(int stateId)
    {
        var cities = _context.Cities.Where(c => c.StateId == stateId).ToList();
        return Json(cities);
    }
}
