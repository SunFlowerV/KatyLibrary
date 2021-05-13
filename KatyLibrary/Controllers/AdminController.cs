using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KatyLibrary.Models;
using KatyLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Genre()
        {
            return View(db.Genres.ToList());
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
            return RedirectToAction("Genre", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> GenreEdit(int? id)
        {
            Genre genre = await db.Genres.FirstOrDefaultAsync(p => p.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        [HttpPost]
        public async Task<IActionResult> GenreEdit(Genre model)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Update(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Genre", "Admin");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> GenreDelete(int? id)
        {
            Genre genre = await db.Genres.FirstOrDefaultAsync(p => p.GenreId == id);
            if (genre != null)
            {
                db.Genres.Remove(genre);
                await db.SaveChangesAsync();
                return RedirectToAction("Genre", "Admin");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Author()
        {
            return View(db.Authors.ToList());
        }

        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(Author author, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
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
            return RedirectToAction("Author", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> AuthorDelete(int? id)
        {
            Author author = await db.Authors.FirstOrDefaultAsync(p => p.AuthorId == id);
            if (author != null)
            {
                if(author.Foto != "/images/NotAvatar.png")
                {
                    if (System.IO.File.Exists(webHostEnvironment.WebRootPath + author.Foto))
                    {
                        System.IO.File.Delete(webHostEnvironment.WebRootPath + author.Foto);
                    }
                }
                
                db.Authors.Remove(author);
                await db.SaveChangesAsync();
                return RedirectToAction("Author", "Admin");
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> AuthorEdit(int? id)
        {
            Author author = await db.Authors.FirstOrDefaultAsync(p => p.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorEdit(Author model, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    if (model.Foto != "/images/NotAvatar.png")
                    {
                        if (System.IO.File.Exists(webHostEnvironment.WebRootPath + model.Foto))
                        {
                            System.IO.File.Delete(webHostEnvironment.WebRootPath + model.Foto);
                        }
                    }
                    string path = "/Files/Foto/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    model.Foto = path;
                }
                db.Authors.Update(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Author", "Admin");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Book()
        {
            return View(db.Books.Include(u => u.Author).ToList());
        }

        [HttpGet]
        public IActionResult AddBook()
        {
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name");
            ViewBag.Authors = new SelectList(db.Authors.ToList(), "AuthorId", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(BookVirwModel model, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                string path = "/Files/Cover/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                model.Book.BookCover = path;
            }
            db.Books.Add(model.Book);
            foreach (var genreId in model.GenreId)
            {
                Genre genre = await db.Genres.FirstOrDefaultAsync(p => p.GenreId == genreId);
                GenreBook genreBook = new GenreBook { Book = model.Book, Genre = genre };
                model.Book.GenreBooks.Add(genreBook);
            }

            await db.SaveChangesAsync();
            return RedirectToAction("Book", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> BookDelete(int? id)
        {
            Book book = await db.Books.FirstOrDefaultAsync(u => u.BookId == id);
            if (book != null)
            {
                if (book.BookCover != "/images/NotCover.png")
                {
                    if (System.IO.File.Exists(webHostEnvironment.WebRootPath + book.BookCover))
                    {
                        System.IO.File.Delete(webHostEnvironment.WebRootPath + book.BookCover);
                    }
                }

                db.Books.Remove(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Book", "Admin");
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> BookEdit(int? id)
        {
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name");
            ViewBag.Authors = new SelectList(db.Authors.ToList(), "AuthorId", "Name");
            Book book = await db.Books.Include(u => u.GenreBooks).FirstOrDefaultAsync(p => p.BookId == id);
            List<Genre> genre = await db.Genres.Include(u => u.GenreBooks).Where(p => p.GenreBooks.Any(o => o.BookId == id)).ToListAsync();
            if (book == null)
            {
                return NotFound();
            }
            List<int> genreId = new List<int>();
            foreach (var gen in genre)
            {
                genreId.Add(gen.GenreId);
            }
            BookVirwModel model = new BookVirwModel { Book = book, GenreId = genreId };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookEdit(BookVirwModel model, IFormFile uploadedFile)
        {
            if (ModelState.IsValid)
            {
                
                if (uploadedFile != null)
                {
                    if (model.Book.BookCover != "/images/NotCover.png")
                    {
                        if (System.IO.File.Exists(webHostEnvironment.WebRootPath + model.Book.BookCover))
                        {
                            System.IO.File.Delete(webHostEnvironment.WebRootPath + model.Book.BookCover);
                        }
                    }
                    string path = "/Files/Foto/" + uploadedFile.FileName;
                    using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }
                    model.Book.BookCover = path;
                }
                
                db.Books.Update(model.Book);
                Book oldBook = await db.Books.Include(p => p.GenreBooks).FirstOrDefaultAsync(u => u.BookId == model.Book.BookId);
                List<GenreBook> oldGenreBooks = oldBook.GenreBooks;
                List<GenreBook> genreBooks = new List<GenreBook>();
                foreach (var genreId in model.GenreId)
                {
                    Genre genre = await db.Genres.FirstOrDefaultAsync(p => p.GenreId == genreId);
                    GenreBook genreBook = new GenreBook { Book = model.Book, Genre = genre };
                    genreBooks.Add(genreBook);
                    
                }
                IEnumerable<GenreBook> addGenreBooks = genreBooks.Except(oldGenreBooks);
                IEnumerable<GenreBook> removeGenreBooks = oldGenreBooks.Except(genreBooks);
                foreach (var rem in removeGenreBooks.ToList())
                {
                    model.Book.GenreBooks.Remove(rem);
                }
                foreach(var add in addGenreBooks.ToList())
                {
                    model.Book.GenreBooks.Add(add);
                }
                await db.SaveChangesAsync();
                return RedirectToAction("Book", "Admin");
            }
            return View(model);
        }
    }
}
