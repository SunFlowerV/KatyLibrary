using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KatyLibrary.Models
{
    public class Book
    {
        public int BookId { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public List<GenreBook> GenreBooks { get; set; }
        public string BookCover { get; set; }
        public int? YearOfPubliching { get; set; }
        public int? NumberOfPages { get; set; }
        public string Annotation { get; set; }
        public bool IsBestSeller { get; set; }
        public string Text { get; set; }
        public Book ()
        {
            GenreBooks = new List<GenreBook>();
        }
    }
}
