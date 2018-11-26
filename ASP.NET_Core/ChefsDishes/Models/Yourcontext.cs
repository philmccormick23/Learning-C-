using Microsoft.EntityFrameworkCore;
 
namespace ChefsDishes.Models
{
    public class YourContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public YourContext(DbContextOptions<YourContext> options) : base(options) { }
        public DbSet<Chef> Chefs {get;set;}
        public DbSet<Dish> Dishes {get;set;}

        
    }
}