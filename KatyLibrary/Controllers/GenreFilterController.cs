using KatyLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatyLibrary.Controllers
{
    public class GenreFilterController : Controller
    {
        public LibraryContext db;
        public GenreFilterController(LibraryContext context)
        {
            db = context;
        }
        public IActionResult Index(int? genre)
        {
            IQueryable<Book> books = db.Books.Include(u => u.Author).Include(u => u.GenreBooks).ThenInclude(p => p.Genre);
            if(genre != null && genre != 0)
            {
                
                books = books.Where(p => p.GenreBooks.Any(u => u.Genre.GenreId == genre));
            }
            return View(books.ToList());
        }
    }
}
