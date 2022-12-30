using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GameRating.Models;

namespace GameRating.Controllers;

public class UserController : Controller
{

    private readonly HomeContext _context;
        //  pass hashlerken sona ekstra strıng eklıyoruz bunu sıtelerde dırek sıfreyı cozemesın dıye appsettıngsda tanımladıgımız stryı alıcaz ve passi hashlicez
    private readonly IConfiguration _configuration;

    public UserController(HomeContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

    public IActionResult SignIn()
    {
        return View();
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

                // string md5Salt = _configuration.GetValue<string>("AppSettings:MD5SaltStr");

                // string saltedPassword = registerUser.Password + md5Salt;

                // string hashedPassword = EncryptProvider.Md5(saltedPassword);


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
   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
