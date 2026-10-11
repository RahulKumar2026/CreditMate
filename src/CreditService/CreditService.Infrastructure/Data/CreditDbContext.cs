using System;
using System.Collections.Generic;
using CreditService.Domain.Entites;
using CreditService.Domain.Entities.Models;

using Microsoft.EntityFrameworkCore;

namespace CreditService.Infrastructure.Data;

public partial class CreditDbContext : DbContext
{
    public CreditDbContext(DbContextOptions<CreditDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<credit_account> credit_accounts { get; set; }

    public virtual DbSet<credit_card> credit_cards { get; set; }

    public virtual DbSet<interest_rate> interest_rates { get; set; }

    public virtual DbSet<loan> loans { get; set; }

    public virtual DbSet<vw_credit_account_summary> vw_credit_account_summaries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<credit_account>(entity =>
        {
            entity.HasKey(e => e.id).HasName("credit_accounts_pkey");

            entity.Property(e => e.account_name).HasMaxLength(150);
            entity.Property(e => e.account_type).HasMaxLength(50);
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.credit_limit).HasPrecision(18, 2);
            entity.Property(e => e.currency_code)
                .HasMaxLength(3)
                .HasDefaultValueSql("'INR'::bpchar")
                .IsFixedLength();
            entity.Property(e => e.current_balance).HasPrecision(18, 2);
            entity.Property(e => e.status)
                .HasMaxLength(30)
                .HasDefaultValueSql("'ACTIVE'::character varying");
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");
            entity.Property(e => e.version).HasDefaultValue(1L);
        });

        modelBuilder.Entity<credit_card>(entity =>
        {
            entity.HasKey(e => e.id).HasName("credit_cards_pkey");

            entity.HasIndex(e => e.credit_account_id, "uq_credit_cards_account").IsUnique();

            entity.Property(e => e.card_name).HasMaxLength(100);
            entity.Property(e => e.card_network).HasMaxLength(30);
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.issuer_name).HasMaxLength(100);
            entity.Property(e => e.last_four_digits)
                .HasMaxLength(4)
                .IsFixedLength();
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.credit_account).WithOne(p => p.credit_card)
                .HasForeignKey<credit_card>(d => d.credit_account_id)
                .HasConstraintName("fk_credit_cards_credit_account");
        });

        modelBuilder.Entity<interest_rate>(entity =>
        {
            entity.HasKey(e => e.id).HasName("interest_rates_pkey");

            entity.Property(e => e.annual_rate).HasPrecision(7, 4);
            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.rate_type).HasMaxLength(30);
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.credit_account).WithMany(p => p.interest_rates)
                .HasForeignKey(d => d.credit_account_id)
                .HasConstraintName("fk_interest_rates_credit_account");
        });

        modelBuilder.Entity<loan>(entity =>
        {
            entity.HasKey(e => e.id).HasName("loans_pkey");

            entity.Property(e => e.created_at).HasDefaultValueSql("now()");
            entity.Property(e => e.loan_name).HasMaxLength(150);
            entity.Property(e => e.outstanding_amount).HasPrecision(18, 2);
            entity.Property(e => e.principal_amount).HasPrecision(18, 2);
            entity.Property(e => e.updated_at).HasDefaultValueSql("now()");

            entity.HasOne(d => d.credit_account).WithMany(p => p.loans)
                .HasForeignKey(d => d.credit_account_id)
                .HasConstraintName("fk_loans_credit_account");
        });

        modelBuilder.Entity<vw_credit_account_summary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_credit_account_summary");

            entity.Property(e => e.account_name).HasMaxLength(150);
            entity.Property(e => e.account_type).HasMaxLength(50);
            entity.Property(e => e.credit_limit).HasPrecision(18, 2);
            entity.Property(e => e.currency_code)
                .HasMaxLength(3)
                .IsFixedLength();
            entity.Property(e => e.current_balance).HasPrecision(18, 2);
            entity.Property(e => e.status).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
