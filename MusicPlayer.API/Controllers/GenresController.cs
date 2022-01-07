using Microsoft.AspNetCore.Mvc;
using MusicPlayer.BLL.Services;
using MusicPlayer.Models.Models.GenreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GenreService _service;
        public GenresController(GenreService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<List<GetGenreModel>> GetAll()
        {
            return await _service.ListAsync();
        }

        [HttpPost("")]
        public async Task<GetGenreModel> Add(AddGenreModel model)
        {
            return await _service.AddAsync(model);
        }

        [HttpPut("")]
        public async Task<GetGenreModel> Edit(EditGenreModel model)
        {
            return await _service.EditAsync(model);
        }

        [HttpDelete("{id:int}")]
        public async Task<int> GetAllGenres(int id)
        {
            return await _service.DeleteAsync(id);
        }
    }
}
