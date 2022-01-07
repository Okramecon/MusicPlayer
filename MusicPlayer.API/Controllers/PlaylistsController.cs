using Microsoft.AspNetCore.Mvc;
using MusicPlayer.BLL.Services;
using MusicPlayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly PlaylistService _service;

        public PlaylistsController(PlaylistService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<GetPlaylistModel>> Get()
        {
            return await _service.ListAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetPlaylistModel> Get(int id)
        {
            return await _service.GetAsync(id);
        }

        [HttpPost]
        public async Task<GetPlaylistModel> Post(AddPlaylistModel model)
        {
            return await _service.AddAsync(model);
        }

        [HttpPut("{id}")]
        public async Task<GetPlaylistModel> Put(EditPlaylistModel model)
        {
            return await _service.EditAsync(model);
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
