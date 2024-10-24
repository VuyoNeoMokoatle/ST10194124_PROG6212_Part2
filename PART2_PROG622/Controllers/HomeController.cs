using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using PART2_PROG622.Models;

namespace PART2_PROG622.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Home page action
        public IActionResult Index()
        {
            return View();
        }

        // Privacy policy page action
        public IActionResult Privacy()
        {
            return View();
        }

        // Redirect to Submit action in ClaimController
        public IActionResult Submit()
        {
            return RedirectToAction("Submit", "Claim"); // Redirects to ClaimController's Submit action
        }

        // Redirect to ManageClaims action in ClaimController
        public IActionResult ManageClaim()
        {
            return RedirectToAction("ManageClaims", "Claim"); // Redirects to ManageClaims action in ClaimController
        }

        // Optional: Handle CreateFile (if there is a FileCreationController)
        public IActionResult CreateFile()
        {
            return RedirectToAction("CreateFile", "FileCreation"); // Adjust controller name as needed
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}




