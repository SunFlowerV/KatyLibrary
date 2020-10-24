using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KatyLibrary.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public List<GenreBook> GenreBooks { get; set; }
        public Genre ()
        {
            GenreBooks = new List<GenreBook>();
        }
    }
}
