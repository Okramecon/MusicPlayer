using AutoMapper;
using MusicPlayer.DAL;

namespace MusicPlayer.API.Services
{
    public class BaseContextService
    {
        protected AppDbContext _context;
        protected IMapper _mapper;

        public BaseContextService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
