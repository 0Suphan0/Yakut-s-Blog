using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BlogApp.Data.Concrete.EfCore
{
    public class BlogContext : DbContext
    {
        //bu sınıf tüm veritabanını işaret eder.
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            
        }

        //DbSetlerin her biri veri tabanımdaki tablolarımdır.
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<User> Users => Set<User>();


    }
}
