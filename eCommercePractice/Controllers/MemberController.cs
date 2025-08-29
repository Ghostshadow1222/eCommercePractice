using eCommercePractice.Data;
using eCommercePractice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            // Check if Username or Email already exists
            bool usernameExists = await _context.Members.AnyAsync(m => m.Username == reg.Username);

            if (usernameExists)
            {
                ModelState.AddModelError("Username", "The username is already taken. Please choose a different one.");
            }
            
            bool emailExists = await _context.Members.AnyAsync(m => m.Email == reg.Email);

            if (emailExists)
            {
                ModelState.AddModelError("Email", "The email is already registered. Please use a different email.");
            }

            if (usernameExists || emailExists)
            {
                return View(reg);
            }

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

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (ModelState.IsValid)
        {
            // Check if the UsernameOrEmail and Password matches a record in the database
            var loggedInMember = await _context.Members.Where(m => (m.Username == login.UsernameOrEmail || m.Email == login.UsernameOrEmail) && m.Password == login.Password)
                .Select(m => new {m.Username, m.MemberId}).SingleOrDefaultAsync();

            if (loggedInMember == null)
            {
                ModelState.AddModelError(string.Empty, "Your provided information does not match any in our records");
                return View(login);
            }

            // Log the user in
            HttpContext.Session.SetString("Username", loggedInMember.Username);
            HttpContext.Session.SetInt32("MemberId", loggedInMember.MemberId);

            return RedirectToAction("Index", "Home");
        }
        return View(login);
    }

    public IActionResult Logout()
    {
        // Destory current session
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
