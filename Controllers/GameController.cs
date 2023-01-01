using GameRating.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace GameRating.Controllers
{
    public class GameController : Controller
    {
        private readonly HomeContext _context;

        public GameController(HomeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Oyunlar.ToListAsync());
        }

          public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                 return RedirectToAction("Index","Home");

            }
            var game = await _context.Oyunlar.Include(b => b.GameComments)
                .Include(b => b.GameComments).ThenInclude(c => c.User)
                .FirstOrDefaultAsync(m => m.GameId == id);
                

            if (game == null)
            {
                return RedirectToAction("Index","Home");

            }

            return View(game);
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(GameComment comment)
        {
            if (!ModelState.IsValid)
            {
                 return View(comment);
            }

             Guid USERID = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                User kullanici = _context.Kullanicilar.SingleOrDefault(u => u.Id == USERID);

                comment.UserId = kullanici.Id;
                comment.CreatedDate = DateTime.UtcNow;
               
                _context.Add(comment);

                await _context.SaveChangesAsync();
                return RedirectToAction("Detail", "Game", new { id = comment.GameId });

        }
      
    }
}
