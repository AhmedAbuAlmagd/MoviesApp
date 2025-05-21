using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<Review>? Reviews { get; set; } = new List<Review>();
    }
}
