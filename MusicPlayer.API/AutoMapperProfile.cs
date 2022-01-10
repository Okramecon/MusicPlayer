using AutoMapper;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Models;
using MusicPlayer.Models.Models;
using MusicPlayer.Models.Models.GenreModels;
using MusicPlayer.Models.ResponseModels;
using System.Linq;

namespace MusicPlayer.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account, AccountResponse>()
                .ForMember("RoleName", opt => opt.MapFrom(c => c.Role.Name)).ReverseMap();
            CreateMap<AddAccountModel, Account>();
            CreateMap<Account, GetAccountModel>();

            CreateMap<AddGenreModel, Genre>();
            CreateMap<EditGenreModel, Genre>();
            CreateMap<Genre, GetGenreModel>();

            CreateMap<AuthorModel, Author>().ReverseMap();
            CreateMap<AddAuthorModel, Author>();

            CreateMap<Track, TrackModel>().ReverseMap();
            CreateMap<Track, GetTrackModel>();
            CreateMap<AddTrackModel, Track>();
            CreateMap<EditTrackModel, Track>();

            CreateMap<AddPlaylistModel, Playlist>();
            CreateMap<Playlist, GetPlaylistModel>()
                .ForMember(dest => dest.Tracks, x => x.MapFrom(src => src.Tracks.Select(t => t.Track)));
            CreateMap<EditPlaylistModel, Playlist>();
        }
    }
}
