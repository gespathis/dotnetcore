using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData, IGreeter greeter
            )
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        public IActionResult Index()
        {
            var rest = new HomeIndexViewModel();
            rest.Restaurants = _restaurantData.GetAll();
            rest.CurrentMessage = _greeter.GetMessageOfTheDay();
            // return new ObjectResult(rest);
            // return Content("Hello from home !!!");
            return View(rest);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditModel restaurant)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();

                newRestaurant.Name = restaurant.Name;
                newRestaurant.Cuisine = restaurant.Cuisine;

                newRestaurant = _restaurantData.Add(newRestaurant);

                return RedirectToAction(nameof(Details), new { Id = newRestaurant.Id });
            }
            else
            {
                return View();
            }
        }
    }
}
