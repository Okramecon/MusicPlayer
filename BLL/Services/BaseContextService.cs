using MusicPlayer.DAL;

namespace MusicPlayer.API.Services
{
    public class BaseContextService
    {
        protected AppDbContext _context;

        public BaseContextService(AppDbContext context)
        {
            _context = context;
        }
    }
}
