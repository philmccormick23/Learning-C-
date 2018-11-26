using Microsoft.EntityFrameworkCore;
 
namespace ProductsCategories.Models
{
    public class YourContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products {get; set;} 
        public DbSet<Association> Associations {get; set;}
        

        
    }
}