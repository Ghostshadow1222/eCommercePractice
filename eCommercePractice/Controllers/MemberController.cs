using Microsoft.AspNetCore.Mvc;

namespace eCommercePractice.Controllers;

public class MemberController : Controller
{
    public IActionResult Register()
    {
        return View();
    }
}
