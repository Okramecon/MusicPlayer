using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.BLL.Helpers;
using MusicPlayer.BLL.Services;
using MusicPlayer.DAL;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.API.Services
{
    public class TrackService : BaseContextService
    {
        public TrackService(AppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<GetTrackModel>> ListAsync()
        {
            var tracks = await _context.Tracks
                .Include(x => x.Author)
                .ToListAsync();
            return _mapper.Map<List<GetTrackModel>>(tracks);
        }

        public async Task<GetTrackModel> GetAsync(int id)
        {
            var track = await GetByIdAsync(id);
            return _mapper.Map<GetTrackModel>(track);
        }

        public async Task<GetTrackModel> AddAsync(AddTrackModel model, string baseUrl)
        {
            var track = _mapper.Map<Track>(model);
            await UploadFileHelper.UploadMusicFileAsync(model.Upload);
            track.MusicUrl = baseUrl + "Music/" + model.Upload.FileName; 
            await _context.Tracks.AddAsync(track);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetTrackModel>(track);
        }

        public async Task<GetTrackModel> EditAsync(EditTrackModel model)
        {
            var track = await GetByIdAsync(model.Id);
            if (track is null)
                return null;

            _mapper.Map(model, track);
            await _context.SaveChangesAsync();

            return _mapper.Map<GetTrackModel>(track);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var track = await GetByIdAsync(id);
            if (track is null)
                return 0;

            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
            return id;
        }

        public async Task<Track> GetByIdAsync(int id)
        {
            return await _context.Tracks
                .Include(t => t.Author)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}
