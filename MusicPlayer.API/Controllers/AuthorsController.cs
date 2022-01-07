using Microsoft.AspNetCore.Mvc;
using MusicPlayer.BLL.Services;
using MusicPlayer.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _service;
        public AuthorsController(AuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<AuthorModel>> Get()
        {
            return await _service.ListAsync();
        }

        [HttpGet("{id}:int")]
        public async Task<AuthorModel> Get(int id)
        {
            return await _service.GetAsync(id);
        }

        [HttpPost]
        public async Task<AuthorModel> Post(AddAuthorModel model)
        {
            return await _service.AddAsync(model);
        }

        [HttpPut]
        public async Task<AuthorModel> Put(AuthorModel model)
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
