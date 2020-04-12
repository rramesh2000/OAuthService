using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Infrastructure.Models
{
    public partial class SecurityContext : DbContext
    {
        public SecurityContext()
        {
        }

        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#pragma warning disable CS1030 // #warning: 'To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.'
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(local);Database=OAuth;User Id=secureadmin; Password=watershed100;");
#pragma warning restore CS1030 // #warning: 'To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.'
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C47C8E2C4");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.HashPassword).HasMaxLength(500);

                entity.Property(e => e.RefreshToken).HasMaxLength(150);

                entity.Property(e => e.Salt).HasMaxLength(150);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
