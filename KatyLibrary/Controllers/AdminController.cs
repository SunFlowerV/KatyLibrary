using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KatyLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KatyLibrary.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private LibraryContext db;
        private IWebHostEnvironment webHostEnvironment;
        public AdminController(LibraryContext context, IWebHostEnvironment appEnviroment)
        {
            db = context;
            webHostEnvironment = appEnviroment;
        }
        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author author, IFormFile uploadedFile)
        {
            if(uploadedFile != null)
            {
                string path = "/Files/Foto/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                author.Foto = path;
            }
            
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
        public async Task<IActionResult> AddBook(Book book, IFormFile uploadedFile)
        {
            if(uploadedFile != null)
            {
                string path = "/Files/Cover/" + uploadedFile.FileName;
                using(var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                book.BookCover = path;
            }
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
         public IActionResult AdminR()
        {
            return View();
        }
    }
}
