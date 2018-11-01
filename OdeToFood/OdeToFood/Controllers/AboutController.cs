using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    [Route("company/[controller]/[action]")]
    public class AboutController
    {
        //[Route("")]
        public string Phone()
        {
            return "+0030 6944313322";
        }
        //[Route("[action]")]
        public string Address()
        {
            return "Greece";
        }
    }
}
