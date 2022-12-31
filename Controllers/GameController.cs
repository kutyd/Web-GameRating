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
            var game = await _context.Oyunlar
                .FirstOrDefaultAsync(m => m.GameId == id);
                

            if (game == null)
            {
                return RedirectToAction("Index","Home");

            }

            return View(game);
        }

      
    }
}
