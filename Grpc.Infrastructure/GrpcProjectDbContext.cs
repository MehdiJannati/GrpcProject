using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System.Reflection;

namespace Infrastructure
{
    public class GrpcProjectDbContext : DbContext
    {
        public GrpcProjectDbContext() { }
        public GrpcProjectDbContext(DbContextOptions<GrpcProjectDbContext> dbContext) : base(dbContext) { }

        public DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /// we can use of IConfiguration for get value from appsetting.json file instead of write connection string as hard code
                
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=GrpcProject;TrustServerCertificate=True;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}