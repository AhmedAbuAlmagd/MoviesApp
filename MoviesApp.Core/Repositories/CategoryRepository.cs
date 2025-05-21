using MoviesApp.Data.Interfaces;
using MoviesApp.Data.Models;
using MoviesApp.Data.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MoviesContext context) : base(context)
        {
        }


    }
}
