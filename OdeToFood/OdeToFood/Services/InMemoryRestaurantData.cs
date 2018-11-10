using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
              new Restaurant {Id=1, Name="burger House"},
              new Restaurant {Id=2, Name="souvlaki"},
              new Restaurant {Id=3, Name="pizza"}
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _restaurants.Max(x => x.Id) + 1;
            _restaurants.Add(newRestaurant);

            return newRestaurant;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }
    }
}
