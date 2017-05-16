using Microsoft.EntityFrameworkCore;

namespace APIService.Models
{
    public class OrdersContext : DbContext
    {
        public OrdersContext () : base()
        {

        }
        public OrdersContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasKey(   o => new {   o.AccountId,
                                        o.InstrumentId,
                                        o.TAction,
                                        o.CorrectFlag,
                                        o.CancelFlag } );
        }
    }
}
