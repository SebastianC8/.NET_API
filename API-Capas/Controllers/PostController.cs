using Core.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data.DTO;

namespace API_Capas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostCore _postCore;

        public PostController(IPostCore postCore)
        {
            _postCore = postCore;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDTO>> Get() =>
            await _postCore.Get();
    }
}
