using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project3.Models
{
    public partial class stockContext : DbContext
    {
        public stockContext()
        {
        }

        public stockContext(DbContextOptions<stockContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Pur> Purs { get; set; } = null!;
        public virtual DbSet<Purchase> Purchases { get; set; } = null!;
        public virtual DbSet<Stockist> Stockists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-B2NAR03\\SQLEXPRESS;Database=stock;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK__customer__D837D05F3FAACFAA");

                entity.ToTable("customer");

                entity.Property(e => e.Cid).HasColumnName("cid");

                entity.Property(e => e.Cname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cname");

                entity.Property(e => e.Cpass)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("cpass");

                entity.Property(e => e.Pass)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pass");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK__Product__C5705938FA5D56A4");

                entity.ToTable("Product");

                entity.Property(e => e.Pid).ValueGeneratedNever();

                entity.Property(e => e.Pname)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pur>(entity =>
            {
                entity.HasKey(e => e.Puid)
                    .HasName("PK__pur__AA01FD6335408090");

                entity.ToTable("pur");

                entity.Property(e => e.Puid).ValueGeneratedNever();

                entity.Property(e => e.Cid).HasColumnName("cid");

                entity.Property(e => e.Flag).HasColumnName("flag");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Pname)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.Purs)
                    .HasForeignKey(d => d.Cid)
                    .HasConstraintName("cid");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.Purs)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK__pur__Pname__0C85DE4D");
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK__purchase__C57059384560F802");

                entity.ToTable("purchase");

                entity.Property(e => e.Pid).ValueGeneratedNever();

                entity.Property(e => e.Pname)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.PidNavigation)
                    .WithOne(p => p.Purchase)
                    .HasForeignKey<Purchase>(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__purchase__Pname__09A971A2");
            });

            modelBuilder.Entity<Stockist>(entity =>
            {
                entity.HasKey(e => e.StId)
                    .HasName("PK__tmp_ms_x__C33CEFC2D018170D");

                entity.ToTable("Stockist");

                entity.Property(e => e.Cpwd)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .UseCollation("Latin1_General_BIN2");

                entity.Property(e => e.StName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
