using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KatyLibrary.Models
{
    public class UserBook
    {
        public string UserId { get; set; }
        public int BookId { get; set; }
        
    }
}
