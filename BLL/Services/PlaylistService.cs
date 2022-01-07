using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.DAL;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.BLL.Services
{
    public class PlaylistService : BaseContextService
    {
        public PlaylistService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<GetPlaylistModel>> ListAsync()
        {
            var playlists = await _context.Playlists.ToListAsync();
            return _mapper.Map<List<GetPlaylistModel>>(playlists);
        }

        public async Task<GetPlaylistModel> GetAsync(int id)
        {
            var playlist = await GetByIdAsync(id);
            return _mapper.Map<GetPlaylistModel>(playlist);
        }

        public async Task<GetPlaylistModel> AddAsync(AddPlaylistModel model)
        {
            var playlist = _mapper.Map<Playlist>(model);
            await _context.Playlists.AddAsync(playlist);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetPlaylistModel>(playlist);
        }

        public async Task<GetPlaylistModel> EditAsync(EditPlaylistModel model)
        {
            var playlist = await GetByIdAsync(model.Id);
            if (playlist is null)
                return null;

            _mapper.Map(model, playlist);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetPlaylistModel>(playlist);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var playlist = await GetByIdAsync(id);
            if (playlist is null)
                return 0;

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Playlist> GetByIdAsync(int id)
        {
            return await _context.Playlists.FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
