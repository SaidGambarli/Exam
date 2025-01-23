using AutoMapper;
using Makaan.MVC.Context;
using Makaan.MVC.Models;
using Makaan.MVC.ViewModels.AgentVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Makaan.MVC.Controllers
{
    public class HomeController(MakaanDbContext _context,IMapper _mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var agent = await _context.Agents.Include(x => x.Department).ToListAsync();
            return View(agent);
        }
    }
}
