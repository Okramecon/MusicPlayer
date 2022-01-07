using AutoMapper;
using MusicPlayer.BLL.Services;
using MusicPlayer.DAL;

namespace MusicPlayer.API.Services
{
    public class TrackService : BaseContextService
    {
        public TrackService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
