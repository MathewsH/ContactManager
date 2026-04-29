using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Garantir que Contacto e Email são únicos
        modelBuilder.Entity<Contact>()
            .HasIndex(c => c.ContactPhone)
            .IsUnique();

        modelBuilder.Entity<Contact>()
            .HasIndex(c => c.Email)
            .IsUnique();

        // Filtro Global: Sempre que pesquisarmos contactos, o EF Core ignora os que têm IsDeleted == true
        modelBuilder.Entity<Contact>()
            .HasQueryFilter(c => !c.IsDeleted);
    }
}