using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KatyLibrary.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Biography { get; set; }
        public List<Book> Books { get; set; }
        public string Foto { get; set; }
    }
}
