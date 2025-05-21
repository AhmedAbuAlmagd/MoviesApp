using Microsoft.Extensions.FileProviders;
using MoviesApp.Core.Interfaces;
using MoviesApp.Core.Services;
using MoviesApp.Data.Interfaces;
using MoviesApp.Data.Models.Context;
using MoviesApp.Domain.Repositories.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MoviesContext _context;

        public ICategoryRepository CategoryRepository { get; }

        public IMovieRepository MovieRepository { get; }

        public IReviewRepository ReviewRepository { get; }
        public IMovieCategoriesRepository MovieCategoriesRepository { get; }

        public IImageService ImageService { get; }

 
        public UnitOfWork(MoviesContext context)
        {
           _context = context;
            MovieRepository = new MovieRepository(context);
            ReviewRepository = new ReviewRepository(context);
            CategoryRepository = new CategoryRepository(context);
            MovieCategoriesRepository = new MovieCategoriesRepository(context);

            var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            ImageService = new ImageService(fileProvider);
        }
    }
}
