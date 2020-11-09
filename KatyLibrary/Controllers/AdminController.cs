using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KatyLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace KatyLibrary.Controllers
{
    public class AdminController : Controller
    {
        private LibraryContext db;
        public AdminController(LibraryContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author author)
        {
            db.Authors.Add(author);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AddBook()
        {
            return View();
        }
    }
}
