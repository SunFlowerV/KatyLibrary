using KatyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatyLibrary.ViewModels
{
    public class BookVirwModel
    {
        public Book Book { get; set; }

        public List<int> GenreId { get; set; }
    }
}
