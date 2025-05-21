using MoviesApp.Core.Interfaces;
using MoviesApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Data.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IMovieRepository MovieRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public IImageService ImageService { get; }
        public IMovieCategoriesRepository MovieCategoriesRepository { get; }

    }
}
