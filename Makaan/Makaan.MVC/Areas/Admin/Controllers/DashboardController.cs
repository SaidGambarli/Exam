using Makaan.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Makaan.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class DashboardController : Controller
    {
        public IActionResult Index() { return View(); }
    }
}


