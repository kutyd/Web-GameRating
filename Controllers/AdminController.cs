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

        // public IActionResult Comments()
        // {

        //     var comments = _context.Comments.
        //     Include(c => c.Blog).
        //     Include(c => c.User).
        //     ToList();

        //     return View(comments);

        // }
        // public IActionResult Users()
        // {

        //     var users = _context.Users.ToList();

        //     return View(users);

        // }

        public IActionResult Games()
        {
            // tüm game ları dönücez 

            // var blogs = _context.Blogs.Include(b => b.User).ToList();
            return View(_context.Oyunlar.ToList());

        }




    }
}