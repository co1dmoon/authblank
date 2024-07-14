using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebApplication1.Models
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role {
        User = 0,
        Admin = 1,
    };

    public class User
    {
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public List<Post> Posts { get; set; } = new List<Post> { };
        public Role Roles { get; set; } = Role.User;

        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.HasIndex(u => u.Username).IsUnique();
            }
        }
    };

    public class UserCredentials
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
