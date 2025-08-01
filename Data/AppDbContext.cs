﻿using Cashcontrol.API.Models.Bussines;
using Microsoft.EntityFrameworkCore;

namespace Cashcontrol.API.Banco
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tabela de Accounts
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts");
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Type).HasConversion<string>();
                entity.Property(a => a.Email).IsRequired().HasMaxLength(255);
                entity.HasIndex(a => a.Email).IsUnique().HasDatabaseName("IX_Accounts_Email");
                entity.Property(a => a.Balance).HasColumnType("decimal(18,2)");
                entity.Property(a => a.CreatedAt).IsRequired();

                entity.HasMany(a => a.Expenses)
                      .WithOne(e => e.Account!)
                      .HasForeignKey(e => e.AccountId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(a => a.Incomes)
                      .WithOne(i => i.Account!)
                      .HasForeignKey(i => i.AccountId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Tabela de Expenses
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("Expenses");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.Category).HasConversion<string>();
                entity.Property(e => e.Date).IsRequired();
            });

            // Tabela de Incomes
            modelBuilder.Entity<Income>(entity =>
            {
                entity.ToTable("Incomes");
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Description).HasMaxLength(255);
                entity.Property(i => i.Amount).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(i => i.Source).HasConversion<string>();
                entity.Property(i => i.Date).IsRequired();
            });
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Income> Incomes { get; set; }
    }
}

