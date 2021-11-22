using ImagesApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace ImagesApi.Data;
public class ImageDbContext:DbContext
{
   public DbSet<Image> Images { get; set; }
    
    public ImageDbContext(DbContextOptions options)
        : base(options) { }
}