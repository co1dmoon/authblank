using Microsoft.Extensions.Hosting;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class PostsService
    {
        public readonly ApplicationDbContext _database;
        
        public PostsService(ApplicationDbContext database)
        {
            this._database = database;
        }

        async public Task<Post> Create(Post post)
        {
            var createdPost = await this._database.Posts.AddAsync(post);
            await this._database.SaveChangesAsync();

            return createdPost.Entity;
        }
    }
}
