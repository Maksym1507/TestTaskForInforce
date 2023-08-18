using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestTaskForInforce.Models.Requests;
using TestTaskForInforce.Services.Abstractions;

namespace TestTaskForInforce.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction(nameof(UrlController.ShortUrlsTable), "Url");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var claimsIdentity = await _authService.LoginUserAsync(request.Email!, request.Password!);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Ok(new { Message = "You are log in"});
            }
            catch (Exception ex)
            {

                return BadRequest(new { ex.Message });
            }
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(UrlController.ShortUrlsTable), "Url");
        }
    }
}
