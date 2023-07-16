using BulkyBook.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace BulkyBook.DateAccess.Data
{
    
    public class ApplactionContext : IdentityDbContext<IdentityUser>
    {
        protected readonly IConfiguration Configuration;
        public ApplactionContext(DbContextOptions<ApplactionContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData( new Category { Id = 1, Name = "mostafa", DisplayOrder= 1 },
                 new Category { Id = 2, Name = "Salem", DisplayOrder = 3 });
           

        modelBuilder.Entity<Product>().HasData(new Product {Id=1,Titel="worldNight",
            Description="this book talk about nothing if you want to west your time look at it",
            Auther="Mostafa Salem"
          , ISBN="w0333334w",Listprice=70,price=65,price50=60,price100=55 ,
            CategoryId=1,ImageUrl=" "
        });
                
        }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
               => optionsBuilder.UseNpgsql(Configuration.GetConnectionString("Default"));
    }
}
