using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.DAL;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Models.Models.GenreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.BLL.Services
{
    public class GenreService : BaseContextService
    {
        public GenreService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<List<GetGenreModel>> ListAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            return _mapper.Map<List<GetGenreModel>>(genres);
        }

        public async Task<GetGenreModel> AddAsync(AddGenreModel model)
        {
            var genre = _mapper.Map<Genre>(model);
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetGenreModel>(genre);
        }

        public async Task<GetGenreModel> EditAsync(EditGenreModel model)
        {
            var genre = await GetByIdAsync(model.Id);
            if (genre is null)
                return null;

            _mapper.Map(model, genre);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetGenreModel>(genre);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var genre = await GetByIdAsync(id);
            if (genre is null)
                return 0;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

    }
}
