namespace WebApplication1.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public required string Title { get; set; } 
        public required string Description { get; set; }
        public required User Author { get; set; }
        public Guid AuthorId {  get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class PostBody
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }

}
