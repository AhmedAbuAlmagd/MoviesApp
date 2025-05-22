using MoviesApp.Core.DTO;
using MoviesApp.Data.Models;
using AutoMapper;
using MoviesApp.Core.Helpers;


namespace MoviesApp.Core.MapConfigs
{
    public class MappingConfig : Profile
    {   
        public MappingConfig()
        {
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();

            CreateMap<Movie, MovieDTO>()
            .ForMember(dest => dest.Categories,
                       opt => opt.MapFrom(src => src.MovieCategories.Select(mc => mc.Category.Name).ToList()))
            .ForMember(dest => dest.Poster,
                      opt => opt.MapFrom(src =>
                      string.IsNullOrEmpty(src.Poster)
                       ? null
                       : $"{URLHelper.BaseUrl}/{src.Poster.Replace("\\", "/")}"))
            .ForMember(dest => dest.Trailer,
                      opt => opt.MapFrom(src =>
                      string.IsNullOrEmpty(src.Trailer)
                       ? null
                       : $"{URLHelper.BaseUrl}/{src.Trailer.Replace("\\", "/")}"));




            CreateMap<MovieDTO, Movie>()
                .ForMember(dest => dest.MovieCategories, opt => opt.Ignore());


            CreateMap<AddMovieDTO, Movie>()
                .ForMember(dest => dest.MovieCategories,op => op.Ignore())
                .ForMember(dest => dest.Poster,op => op.Ignore())
                .ForMember(dest => dest.Trailer ,op => op.Ignore())
                .ReverseMap();


            CreateMap<UpdateMovieDTO, Movie>()
               .ForMember(dest => dest.MovieCategories, op => op.Ignore())
               .ForMember(dest => dest.Poster, op => op.Ignore())
               .ForMember(dest => dest.Trailer, op => op.Ignore())
               .ReverseMap();

            CreateMap<Review, ReviewDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<AddReviewDTO, Review>();
            CreateMap<UpdateReviewDTO, Review>();



        }
    }
}
