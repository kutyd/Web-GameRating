using System.Diagnostics;
using GameRating.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace GameRating.Controllers;

public class UserController : Controller
{

    private readonly HomeContext _context;

    public UserController(HomeContext context)
        {
            _context = context;
        }


   
     public IActionResult SignUp()
    {
        return View();
    }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signupInfo)
        {
            if (ModelState.IsValid)
            {
                if (_context.Kullanicilar.Any(u => u.Username.ToLower() == signupInfo.Username.ToLower()))
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(signupInfo);
                }
                User user = new User()
                {
                    Username = signupInfo.Username,
                    Password = signupInfo.Password
                };

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("SignIn", "User");

            }
            return View(signupInfo);
        }
   

        public IActionResult SignIn()
            {
                return View();
            }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signinInfo)
        {
            if (ModelState.IsValid)
            {
              
                User user = _context.Kullanicilar.SingleOrDefault(u => u.Username.ToLower() == signinInfo.Username.ToLower() && u.Password == signinInfo.Password);


                if (user != null)
                {

                    // login işlemi başarılı
                    // session oluştur

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Username ?? string.Empty));
                    claims.Add(new Claim(ClaimTypes.Role, user.Role));

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Game");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is wrong");
                }

            }
            return View(signinInfo);

        }


  public IActionResult Logout()
        {

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Game");
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
