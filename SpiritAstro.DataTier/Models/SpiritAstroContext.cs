using System;
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

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<CasbinRule> CasbinRules { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
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
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Wallet> Wallets { get; set; }
        public virtual DbSet<Zodiac> Zodiacs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Bookings_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Astrologer)
                    .WithMany(p => p.BookingAstrologers)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Bookings_Users_Id_fk");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.BookingCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Bookings_Users_Id_fk_2");
            });

            modelBuilder.Entity<CasbinRule>(entity =>
            {
                entity.ToTable("casbin_rule");

                entity.HasIndex(e => e.Ptype, "IX_casbin_rule_ptype");

                entity.HasIndex(e => e.V0, "IX_casbin_rule_v0");

                entity.HasIndex(e => e.V1, "IX_casbin_rule_v1");

                entity.HasIndex(e => e.V2, "IX_casbin_rule_v2");

                entity.HasIndex(e => e.V3, "IX_casbin_rule_v3");

                entity.HasIndex(e => e.V4, "IX_casbin_rule_v4");

                entity.HasIndex(e => e.V5, "IX_casbin_rule_v5");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ptype).HasColumnName("ptype");

                entity.Property(e => e.V0).HasColumnName("v0");

                entity.Property(e => e.V1).HasColumnName("v1");

                entity.Property(e => e.V2).HasColumnName("v2");

                entity.Property(e => e.V3).HasColumnName("v3");

                entity.Property(e => e.V4).HasColumnName("v4");

                entity.Property(e => e.V5).HasColumnName("v5");
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

            modelBuilder.Entity<CustomerZodiac>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("CustomerZodiacs_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerZodiacs)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerZodiacs_Users_Id_fk");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.CustomerZodiacs)
                    .HasForeignKey(d => d.ZodiacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerZodiacs_Zodiacs_Id_fk");
            });

            modelBuilder.Entity<FamousPerson>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FamousPerson");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Zodiac)
                    .WithMany()
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
                    .HasConstraintName("FieldDetails_Users_Id_fk");

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
                    .WithMany(p => p.FollowAstrologers)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follows_Users_Id_fk");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FollowCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follows_Users_Id_fk_2");
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

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Roles_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasMaxLength(30);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Users_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.DeletedAt, "IDx_users_deleted_at");

                entity.HasIndex(e => e.PhoneNumber, "UIDx_users_phone_number")
                    .IsUnique();

                entity.Property(e => e.Gender).HasComment("1 là nam, 0 là n?");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.TimeOfBirth).HasColumnType("datetime");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("UserRoles_pk")
                    .IsClustered(false);

                entity.Property(e => e.RoleId).HasMaxLength(30);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRoles_Roles_Id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRoles_Users_Id_fk");
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
