using AutoMapper;
using Makaan.MVC.Context;
using Makaan.MVC.Models;
using Makaan.MVC.ViewModels.DepartmentVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Makaan.MVC.Areas.Admin.Controllers;

[Area("Admin")]

public class DepartmentController(MakaanDbContext _context,IMapper _mapper) : Controller
{
    public async Task<IActionResult> Index() 
    {
        var department = await _context.Departments.ToListAsync();
        return  View(department);
    }
    public IActionResult Create()
    { 
        return View(); 
    }
    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateVM vm)
    {
        var department = await _context.Departments.Where(x => x.Name == vm.Name).FirstOrDefaultAsync();
        if (department is not null) return BadRequest();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        department = _mapper.Map<Department>(vm);
        await _context.Departments.AddAsync(department);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int? id) 
    {
        if (id is null) return BadRequest();
        var department = await _context.Departments.Where(x=>x.Id==id).FirstOrDefaultAsync();
        DepartmentUpdateVM vm = _mapper.Map<DepartmentUpdateVM>(department);
        return View(vm); 
    }
    [HttpPost]
    public async Task<IActionResult> Update(DepartmentUpdateVM vm , int? id) 
    { 
        if (id is null) return BadRequest();
        var department = await _context.Departments.Where(x=>x.Id==id).FirstOrDefaultAsync();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }
        department = _mapper.Map(vm, department);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id) 
    { 
        if (id is null) return BadRequest();
        var department = await _context.Departments.FindAsync(id);
        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
