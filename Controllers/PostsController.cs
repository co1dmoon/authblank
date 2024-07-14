using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {

        private readonly PostsService _postsService;

        public PostController(PostsService postsService) 
        {
            this._postsService = postsService;
        }

        [HttpPost(Name = "CreatePost")]
        async public Task<Post> CreatePost([FromBody] PostBody postBody)
        {
            
        }
    }
}
