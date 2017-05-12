using Microsoft.EntityFrameworkCore;

namespace APIService.Models
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasKey(   o => new {   o.AccountId,
                                        o.InstrumentId,
                                        o.TAction,
                                        o.CorrectFlag,
                                        o.CancelFlag } );
        }
        
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseMySql(@"Server=localhost;database=ef;uid=root;pwd=123456;");
        */
    }
}
