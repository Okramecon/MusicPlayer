using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicPlayer.API.Services;
using MusicPlayer.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly TrackService _service;
        public TracksController(TrackService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<GetTrackModel>> Get()
        {
            return await _service.ListAsync();
        }

        [HttpGet("{id}")]
        public async Task<GetTrackModel> Get(int id)
        {
            return await _service.GetAsync(id);
        }

        [HttpPost]
        public async Task<GetTrackModel> Post(AddTrackModel model)
        {
            return await _service.AddAsync(model);
        }

        [HttpPut]
        public async Task<GetTrackModel> Put(EditTrackModel model)
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
