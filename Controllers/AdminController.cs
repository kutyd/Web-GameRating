using GameRating.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Authorization;
namespace GameRating.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly HomeContext _context;

        public AdminController(HomeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return RedirectToAction("Games");

        }
      
        public IActionResult Games()
        {
            // tüm game ları dönücez 
            return View(_context.Oyunlar.ToList());

        }
         public IActionResult CreateGame()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return View(game);
            }

            if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];

                    if (file != null && file.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                            game.Banner = Convert.ToBase64String(stream.ToArray());
                        }
                    }
                }
            _context.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction("Games", "Admin");
        }

         public async Task<IActionResult> DeleteGame(int? id)
        {

            var game = await _context.Oyunlar.FindAsync(id);

            _context.Oyunlar.Remove(game);

            await _context.SaveChangesAsync();

            return RedirectToAction("Games","Admin");

        }

          public async Task<IActionResult> EditGame(int? id)
        {
            var game = await _context.Oyunlar.FindAsync(id);

            var model = new EditGameViewModel
            {
                Title = game.Title,
                Detail = game.Detail,
                Banner = game.Banner
            };

            return View(model);
        }


         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGame(int id, EditGameViewModel model)
        {

            if (ModelState.IsValid)
            {
                var game = await _context.Oyunlar.FindAsync(id);
                
                game.Title = model.Title;
                game.Detail = model.Detail;

                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];

                    if (file != null && file.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                            game.Banner = Convert.ToBase64String(stream.ToArray());
                        }

                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction("Games", "Admin");
            }
            
             return View(model);

        }

         public IActionResult Users()
        {
            // tüm kullanıcıları  dönücez 
            return View(_context.Kullanicilar.ToList());
        }

        public IActionResult CreateUser()
        {
            return View();
        }      

        [HttpPost]
         public async Task<IActionResult> CreateUser(SignInViewModel createdUser)
        {

            if (ModelState.IsValid)
            {
                if (_context.Kullanicilar.Any(u => u.Username.ToLower() == createdUser.Username.ToLower()))
                {
                    ModelState.AddModelError("Username", "Username already exists");
                    return View(createdUser);
                }
                User user = new User()
                {
                    Username = createdUser.Username,
                    Password = createdUser.Password
                };

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Users", "Admin");

            }
            return View(createdUser);
        }

        
        [HttpGet]
        public async Task<IActionResult> DeleteUser(Guid? id)
        {

            Guid USERID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var kullanici = await _context.Kullanicilar.SingleOrDefaultAsync(u => u.Id == id);

          
            if (kullanici.Id == USERID)
            {
                _context.Kullanicilar.Remove(kullanici);
                await _context.SaveChangesAsync();

                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Game");

            }

            _context.Kullanicilar.Remove(kullanici);
            await _context.SaveChangesAsync();

            return RedirectToAction("Users","Admin");

        }

          [HttpGet]
        public async Task<IActionResult> Updateuser(Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("Users", "Admin");

            }

            var kullanici = await _context.Kullanicilar.SingleOrDefaultAsync(u => u.Id == id);

            var model = new EditUserViewModel
            {
                Username = kullanici.Username,
                Role = kullanici.Role,
            };

            return View(model);
        }

        public async Task<IActionResult> UpdateUser(Guid id, EditUserViewModel changeUserInfo)
        {
            if (!ModelState.IsValid)
            {
                  return View(changeUserInfo);
               
            }
                var kullanici = await _context.Kullanicilar.SingleOrDefaultAsync(u => u.Id == id);

                kullanici.Username = changeUserInfo.Username;
                kullanici.Role = changeUserInfo.Role;
              
                await _context.SaveChangesAsync();

                return RedirectToAction("Users","Admin");
        }


         public IActionResult GameComments()
        {
            // tüm yorumları  dönücez 

            return View(_context.Yorumlar.Include(c => c.User).Include(c => c.Game).ToList());

        }
        public async Task<IActionResult> DeleteComment(int? id) {


            var comment = await _context.Yorumlar.FindAsync(id);

            _context.Yorumlar.Remove(comment);

            await _context.SaveChangesAsync();

            return RedirectToAction("GameComments", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> EditComment(int? id)
        {
         
            var comment = await _context.Yorumlar.FindAsync(id);
            
            var model = new EditGameCommentViewModel
            {
                Content = comment.Content,
                Rating = comment.Rating
            };
            
            return View(model);
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> EditComment(int id, EditGameCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var comment = await _context.Yorumlar.FindAsync(id);
           
            comment.Content = model.Content;
            comment.Rating = model.Rating ;

            await _context.SaveChangesAsync();

           return RedirectToAction("GameComments", "Admin");

        }

    }
}