using System.ComponentModel.DataAnnotations;
namespace PizzaShop.Models;

public class Location
{
    public int SelectedCountryId { get; set; }
    public int SelectedStateId { get; set; }
    public int SelectedCityId { get; set; }
    public IEnumerable<Country> Countries { get; set; }
    public IEnumerable<State> States { get; set; }
    public IEnumerable<City> Cities { get; set; }
}
