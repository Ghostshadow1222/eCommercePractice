using eCommercePractice.Data;
using eCommercePractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommercePractice.Controllers;

public class MemberController : Controller
{
    private readonly ProductDBContext _context;

    public MemberController(ProductDBContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel reg)
    {
        if (ModelState.IsValid)
        {
            // Map ViewModel to Member model tracked by DB
            Member newMember = new()
            {
                Username = reg.Username,
                Email = reg.Email,
                Password = reg.Password,
                DateOfBirth = reg.DateOfBirth,
            };

            _context.Members.Add(newMember);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        return View(reg);
    }
}
