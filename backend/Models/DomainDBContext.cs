using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class DomainDBContext : DbContext{

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Product> Products { get; set; }

    public DomainDBContext(DbContextOptions<DomainDBContext> options) : base(options) 
    { 
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ticket>(
            entity => {
                entity.HasKey(e => e.TicketId);
                entity.HasOne(e => e.Produto).WithMany(p => p.Tickets).HasForeignKey(e => e.ProdutoId);
            }
        );

        modelBuilder.Entity<Product>(
            entity => {
                entity.HasKey(e => e.ProductId);
                entity.HasIndex(e => e.Nome).IsUnique();
            }
        );
        
    }

}