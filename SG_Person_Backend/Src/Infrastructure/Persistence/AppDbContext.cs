using Microsoft.EntityFrameworkCore;
using SG_Person_Backend.Src.Domain.Entities;

namespace SG_Person_Backend.Src.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<User> User { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.HasIndex(u => u.Username)
                    .IsUnique();
            });


            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.Property(p => p.BirthDate)
                    .IsRequired()
                    .HasColumnType("date");

                entity.Property(u => u.BirthPlace)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Nationality)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Cpf)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(u => u.Cpf)
                    .IsUnique();
            });

            // Configuração para CPF único
            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Cpf)
                .IsUnique();
        }
    }
}
