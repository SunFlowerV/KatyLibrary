using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using KatyLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace KatyLibrary.Controllers
{
    
    public class HomeController : Controller
    {
        public LibraryContext db;
        public UserManager<IdentityUser> _userManager;
        public UserContentContext db2;
        public HomeController(LibraryContext context, UserManager<IdentityUser> userManager, UserContentContext context2)
        {
            db = context;
            _userManager = userManager;
            db2 = context2;
        }
        public IActionResult Index()
        {
            return View(db.Books.Include(u => u.Author).ToList());
        }
        public IActionResult Author()
        {
            
            return View(db.Authors.ToList());
        }
        public IActionResult AuthorInfo(int? id)
        {
            if(id != null)
            {
                Author author = db.Authors.FirstOrDefault(u => u.AuthorId == id);
                if(author != null)
                {
                    return View(db.Authors.FirstOrDefault(u => u.AuthorId == id));
                }
            }
            return NotFound();
        }
        public IActionResult BookInfo(int? id)
        {
            if(id != null)
            {
                Book book = db.Books.FirstOrDefault(u => u.BookId == id);
                if(book != null)
                {
                    return View(db.Books.Include(u => u.Author).Include(u => u.GenreBooks).ThenInclude(p => p.Genre).FirstOrDefault(u => u.BookId == id));
                }
            }
            return NotFound();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult GenreLink()
        {
            return PartialView(db.Genres.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Bye(int id)
        {
            UserBook userBook = new UserBook { UserId = _userManager.GetUserId(User), BookId = id };
            db2.UserBooks.Add(userBook);
            await db2.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
