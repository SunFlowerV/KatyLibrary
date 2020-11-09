using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using KatyLibrary.Models;

namespace KatyLibrary.Controllers
{
    
    public class HomeController : Controller
    {
        public LibraryContext db;
        public HomeController(LibraryContext context)
        {
            db = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View();
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
    }
}
