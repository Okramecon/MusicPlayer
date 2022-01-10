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
            var playlists = await _context.Playlists
                .Include(x => x.Tracks)
                .ThenInclude(x => x.Track)
                .ToListAsync();
            return _mapper.Map<List<GetPlaylistModel>>(playlists);
        }

        public async Task<GetPlaylistModel> GetAsync(int id)
        {
            var playlist = await GetByIdAsync(id);
            return _mapper.Map<GetPlaylistModel>(playlist);
        }

        public async Task<TrackModel> AddAsync(int playlistId, int trackId)
        {
            var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.Id == playlistId);
            var track = await _context.Tracks.FirstOrDefaultAsync(x => x.Id == trackId);

            if (playlist != default && track != default)
            {
                var playlistTrack = new PlaylistTrack()
                {
                    PlaylistId = playlistId,
                    TrackId = trackId,
                };
            
                await _context.PlaylistTracks.AddAsync(playlistTrack);
            }
            await _context.SaveChangesAsync();

            return _mapper.Map<GetTrackModel>(track);
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

        public async Task<int> DeleteAsync(int playlistId, int trackId)
        {
            var playlistTrack = await _context.PlaylistTracks.FirstOrDefaultAsync(x => x.PlaylistId == playlistId && x.TrackId == trackId);
            if (playlistTrack == default)
            {
                return 0;
            }
            _context.PlaylistTracks.Remove(playlistTrack);
            await _context.SaveChangesAsync();
            return trackId;
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
            return await _context.Playlists
                .Include(t => t.Tracks)
                .ThenInclude(x => x.Track)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
