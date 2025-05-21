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
    public class ReviewRepository : GenericRepository<Review> , IReviewRepository
    {
        public ReviewRepository(MoviesContext context) : base(context)
        {
            
        }
    }
}
