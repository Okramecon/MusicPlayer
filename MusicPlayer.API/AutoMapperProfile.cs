using AutoMapper;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Models.Models;
using MusicPlayer.Models.Models.GenreModels;
using MusicPlayer.Models.ResponseModels;

namespace MusicPlayer.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>()
                .ForMember("RoleName", opt => opt.MapFrom(c => c.Role.Name)).ReverseMap();

            CreateMap<AddGenreModel, Genre>();
            CreateMap<EditGenreModel, Genre>();
            CreateMap<Genre, GetGenreModel>();

            CreateMap<AuthorModel, Author>().ReverseMap();
            CreateMap<AddAuthorModel, Author>();
        }
    }
}
