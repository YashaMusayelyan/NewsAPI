using Microsoft.EntityFrameworkCore;
using News.DAL.Entities;

namespace News.DAL
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) : base(options)
        {

        }

        public DbSet<NewsEntity> News { get; set; }
    }
}
