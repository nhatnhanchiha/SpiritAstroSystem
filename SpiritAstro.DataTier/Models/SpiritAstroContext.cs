﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class SpiritAstroContext : DbContext
    {
        public SpiritAstroContext()
        {
        }

        public SpiritAstroContext(DbContextOptions<SpiritAstroContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Astrologer> Astrologers { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerZodiac> CustomerZodiacs { get; set; }
        public virtual DbSet<FamousPerson> FamousPeople { get; set; }
        public virtual DbSet<Field> Fields { get; set; }
        public virtual DbSet<FieldDetail> FieldDetails { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostPlanet> PostPlanets { get; set; }
        public virtual DbSet<PostZodiac> PostZodiacs { get; set; }
        public virtual DbSet<PriceTable> PriceTables { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<Zodiac> Zodiacs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=54.169.107.173,1433;Initial Catalog=SpiritAstro;User ID=sa;Password=SE1416MyPassword!;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Astrologer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Astrologers_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.DeletedAt, "UIDx_astrologers_deleted_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Bookings_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Astrologer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Bookings_Astrologers_Id_fk");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Bookings_Customers_Id_fk");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Categories_pk")
                    .IsClustered(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Customers_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.DeletedAt, "UIDx_customers_deleted_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CustomerZodiac>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("CustomerZodiacs_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerZodiacs)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerZodiacs_Customers_Id_fk");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.CustomerZodiacs)
                    .HasForeignKey(d => d.ZodiacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerZodiacs_Zodiacs_Id_fk");
            });

            modelBuilder.Entity<FamousPerson>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("FamousPerson_pk")
                    .IsClustered(false);

                entity.ToTable("FamousPerson");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.FamousPeople)
                    .HasForeignKey(d => d.ZodiacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FamousPerson_Zodiacs_Id_fk");
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Fields_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.DeletedAt, "IDx_fields_deleted_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.PriceTable)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.PriceTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fields_PriceTables_Id_fk");
            });

            modelBuilder.Entity<FieldDetail>(entity =>
            {
                entity.HasKey(e => new { e.AstrologerId, e.FieldId })
                    .HasName("FieldDetails_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Astrologer)
                    .WithMany(p => p.FieldDetails)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FieldDetails_Astrologers_Id_fk");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.FieldDetails)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FieldDetails_Fields_Id_fk");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => new { e.AstrologerId, e.CustomerId })
                    .HasName("Follows_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Astrologer)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follows_Astrologers_Id_fk");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follows_Customers_Id_fk");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Payments_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Payments_Bookings_Id_fk");
            });

            modelBuilder.Entity<Planet>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Planets_pk")
                    .IsClustered(false);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Posts_pk")
                    .IsClustered(false);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Astrologer)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Posts_Astrologers_Id_fk");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Posts_Categories_Id_fk");
            });

            modelBuilder.Entity<PostPlanet>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.PlanetId })
                    .HasName("PostPlanets_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Planet)
                    .WithMany(p => p.PostPlanets)
                    .HasForeignKey(d => d.PlanetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostPlanets_Planets_Id_fk");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostPlanets)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostPlanets_Posts_Id_fk");
            });

            modelBuilder.Entity<PostZodiac>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.ZodiacId })
                    .HasName("PostZodiacs_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostZodiacs)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostZodiacs_Posts_Id_fk");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.PostZodiacs)
                    .HasForeignKey(d => d.ZodiacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostZodiacs_Zodiacs_Id_fk");
            });

            modelBuilder.Entity<PriceTable>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PriceTables_pk")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => new { e.PaymentId, e.WalletId })
                    .HasName("Transactions_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Transactions_Payments_Id_fk");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WalletId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Transactions_Wallets_Id_fk");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Wallets_pk")
                    .IsClustered(false);
            });

            modelBuilder.Entity<Zodiac>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Zodiacs_pk")
                    .IsClustered(false);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
