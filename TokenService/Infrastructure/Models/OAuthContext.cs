﻿using System;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Models
{
    public partial class OAuthContext : DbContext, ITokenServiceDbContext
    {
        public OAuthContext()
        {
        }

        public OAuthContext(DbContextOptions<OAuthContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Authorize> Authorize { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Authorize>().ToTable("Authorize");
            modelBuilder.Entity<User>().ToTable("User");
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
