using AutoMapper;
using Makaan.MVC.Context;
using Makaan.MVC.Extensions;
using Makaan.MVC.Models;
using Makaan.MVC.ViewModels.AgentVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Makaan.MVC.Areas.Admin.Controllers;

[Area("Admin")]

public class AgentController(MakaanDbContext _context,IMapper _mapper,IWebHostEnvironment _env ) : Controller
{
    public async Task<IActionResult> Index()
    {
        var agent = await _context.Agents.Include(x=>x.Department).ToListAsync();
        return View(agent);
    }
    public async Task<IActionResult> Create()
    {
        ViewBag.Departments = await _context.Departments.Where(x=>!x.IsDeleted).ToListAsync();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(AgentCreateVM vm)
    {
        if(vm.File != null)
        {
            if (!vm.File.IsValidType("image"))
                ModelState.AddModelError("FileUrl", "File must be an image");
            if (!vm.File.IsValidSize(400))
                ModelState.AddModelError("FileUrl", "File must be less than 400kb");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
            return View(vm);
        }
        if (!await _context.Departments.AnyAsync(x => x.Id == vm.DepartmentId))
        {
            ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
            ModelState.AddModelError("DepartmentId", "Department not found");
            return View();
        }
        var agent = _mapper.Map<Agent>(vm);
        agent.ImageUrl = await vm.File.UploadAsync(_env.WebRootPath, "imgs", "agents");
        await _context.Agents.AddAsync(agent);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int? id)
    {
        if (id is null) return BadRequest();
        var agent = await _context.Agents.Where(x=>x.Id == id).FirstOrDefaultAsync();
        if (agent is null) return NotFound();
        AgentUpdateVM vm = _mapper.Map<AgentUpdateVM>(agent);
        vm.ImageUrl = agent.ImageUrl;
        ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Update(AgentUpdateVM vm,int? id)
    {
        if (id is null) return BadRequest();
        var agent = await _context.Agents.Where(x => x.Id == id).FirstOrDefaultAsync();
        if (agent is null) return NotFound();
        _mapper.Map(vm, agent);
        agent.ImageUrl = await vm.File.UploadAsync(_env.WebRootPath,"imgs","agents");
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id) 
    {
        if (id is null) return BadRequest();
        var agent = await _context.Agents.FindAsync(id);
        if (agent is null) return NotFound();
        _context.Agents.Remove(agent);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index)); 
    }
}
