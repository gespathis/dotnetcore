using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Services;

namespace OdeToFood.Pages
{
    public class GreetingModel : PageModel
    {
        private IGreeter _greeder;

        public string CurrentGreeting { get; set; }

        public GreetingModel(IGreeter greeder)
        {
            _greeder = greeder;
        }
        public void OnGet(string name)
        {
            CurrentGreeting = $"{name} : {_greeder.GetMessageOfTheDay()}";
        }
    }
}