using AutoMapper;
using Makaan.MVC.Areas.Admin.Controllers;
using Makaan.MVC.Context;
using Makaan.MVC.Models;
using Makaan.MVC.ViewModels.AuthVM;
using Makaan.MVC.Views.Account.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.MVC.Controllers;

public class AccountController(UserManager<AppUser> _userManager,SignInManager<AppUser> _signInManager,IMapper _mapper,RoleManager<IdentityRole> _roleManager) : Controller
{
    private bool isAuthenticated => HttpContext.User.Identity?.IsAuthenticated ?? false;
    public IActionResult Register()
    {
        if (isAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (isAuthenticated) return RedirectToAction("Index", "Home");
        if (!ModelState.IsValid)
            return View();
        var appUser = _mapper.Map<AppUser>(vm);
        var result = await _userManager.CreateAsync(appUser, vm.Password);
        if (!result.Succeeded)
        {
            foreach(var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View();
        }
        await _userManager.AddToRoleAsync(appUser,nameof(Roles.Admin));
        return RedirectToAction("Login");
    }
    public async Task<IActionResult> Method()
    {
        foreach (Roles item in Enum.GetValues(typeof(Roles)))
        {
            await _roleManager.CreateAsync(new IdentityRole() { Name = item.ToString() });
        }
        return Ok();
    }
    public IActionResult Login()
    {
        if (isAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm, string? returnUrl = null)
    {
        if (isAuthenticated) return RedirectToAction("Index", "Home");
        if (!ModelState.IsValid)
            return View();
        AppUser? appUser = null;
        if (vm.EmailOrUserName.Contains('@'))
            appUser = await _userManager.FindByEmailAsync(vm.EmailOrUserName);
        else
            appUser = await _userManager.FindByNameAsync(vm.EmailOrUserName);
        if(appUser is null)
        {
            ModelState.AddModelError("", "Username or Email is wrong!");
            return View();
        }
        var result = await _signInManager.PasswordSignInAsync(appUser, vm.Password, vm.RememberMe, true);
        if(result.IsLockedOut)
        {
            ModelState.AddModelError("", "Wait until" + appUser.LockoutEnd!.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Username or Email is wrong!");   
            return View();
        }
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            if(await _userManager.IsInRoleAsync(appUser, "Admin"))
            {
                return RedirectToAction("Index", new {Controller="Dashboard",Area="Admin" });
            }
            return RedirectToAction("Index", "Home");
        }

        return LocalRedirect(returnUrl);
    }
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
