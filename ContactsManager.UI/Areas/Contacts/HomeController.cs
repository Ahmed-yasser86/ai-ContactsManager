using Microsoft.AspNetCore.Mvc;

namespace StocksApp2.Areas.Contacts
{
    public class HomeController : Controller
    {
        [Route("Error")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
