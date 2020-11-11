using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KatyLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpGet]
        public IActionResult AddBook()
        {
            ViewBag.Authors = new SelectList(db.Authors.ToList(), "AuthorId", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult AddGenreBook()
        {
            ViewBag.Genres = new SelectList(db.Genres.ToList(), "GenreId", "Name");
            ViewBag.Books = new SelectList(db.Books.ToList(), "BookId", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenreBook(GenreBook genreBook)
        {
            Book book = db.Books.FirstOrDefault(u => u.BookId == genreBook.BookId);
            book.GenreBooks.Add(genreBook);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(Genre genre)
        {
            db.Genres.Add(genre);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
