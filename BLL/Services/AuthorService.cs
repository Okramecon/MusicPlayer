using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.DAL;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.BLL.Services
{
    public class AuthorService : BaseContextService
    {
        public AuthorService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }


        public async Task<List<AuthorModel>> ListAsync()
        {
            var authors = await _context.Authors.ToListAsync();
            return _mapper.Map<List<AuthorModel>>(authors);
        }

        public async Task<AuthorModel> GetAsync(int id)
        {
            var author = await GetByIdAsync(id);
            return _mapper.Map<AuthorModel>(author);
        }

        public async Task<AuthorModel> AddAsync(AddAuthorModel model)
        {
            var author = _mapper.Map<Author>(model);
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorModel>(author);
        }

        public async Task<AuthorModel> EditAsync(AuthorModel model)
        {
            var author = await GetByIdAsync(model.Id);
            if (author is null)
                return null;

            _mapper.Map(model, author);
            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorModel>(author);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var author = await GetByIdAsync(id);
            if (author is null)
                return 0;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Author> GetByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
