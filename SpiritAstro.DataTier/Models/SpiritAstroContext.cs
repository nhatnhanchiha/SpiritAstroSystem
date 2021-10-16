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

        public virtual DbSet<Astrologer> Astrologers { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<CasbinRule> CasbinRules { get; set; }
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
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
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

                entity.ToTable("Astrologer");

                entity.HasIndex(e => e.DeletedAt, "IDx_astrologers_deleted_at");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ImageUrl).HasMaxLength(450);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Astrologer)
                    .HasForeignKey<Astrologer>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Astrologer_User_Id_fk");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Booking_pk")
                    .IsClustered(false);

                entity.ToTable("Booking");

                entity.HasOne(d => d.Astrologer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Booking_Astrologer_Id_fk");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Booking_Customer_Id_fk");
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

                entity.ToTable("Category");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Customers_pk")
                    .IsClustered(false);

                entity.ToTable("Customer");

                entity.HasIndex(e => e.DeletedAt, "IDx_customers_deleted_at");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UrlImage).IsUnicode(false);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Customer_User_Id_fk");
            });

            modelBuilder.Entity<CustomerZodiac>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("CustomerZodiacs_pk")
                    .IsClustered(false);

                entity.ToTable("CustomerZodiac");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerZodiacs)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerZodiac_Customer_Id_fk");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.CustomerZodiacs)
                    .HasForeignKey(d => d.ZodiacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerZodiac_Zodiac_Id_fk");
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

                entity.Property(e => e.UrlImage).IsUnicode(false);

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.FamousPeople)
                    .HasForeignKey(d => d.ZodiacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FamousPerson_Zodiac_Id_fk");
            });

            modelBuilder.Entity<Field>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Fields_pk")
                    .IsClustered(false);

                entity.ToTable("Field");

                entity.HasIndex(e => e.DeletedAt, "IDx_fields_deleted_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.PriceTable)
                    .WithMany(p => p.Fields)
                    .HasForeignKey(d => d.PriceTableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Field_PriceTable_Id_fk");
            });

            modelBuilder.Entity<FieldDetail>(entity =>
            {
                entity.HasKey(e => new { e.AstrologerId, e.FieldId })
                    .HasName("FieldDetails_pk")
                    .IsClustered(false);

                entity.ToTable("FieldDetail");

                entity.HasOne(d => d.Astrologer)
                    .WithMany(p => p.FieldDetails)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FieldDetail_Astrologer_Id_fk");

                entity.HasOne(d => d.Field)
                    .WithMany(p => p.FieldDetails)
                    .HasForeignKey(d => d.FieldId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FieldDetail_Field_Id_fk");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.HasKey(e => new { e.AstrologerId, e.CustomerId })
                    .HasName("Follows_pk")
                    .IsClustered(false);

                entity.ToTable("Follow");

                entity.HasOne(d => d.Astrologer)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follow_Astrologer_Id_fk");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Follow_Customer_Id_fk");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Payments_pk")
                    .IsClustered(false);

                entity.ToTable("Payment");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Payment_Booking_Id_fk");
            });

            modelBuilder.Entity<Planet>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Planets_pk")
                    .IsClustered(false);

                entity.ToTable("Planet");

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

                entity.ToTable("Post");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.ImageUrl).HasMaxLength(450);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Astrologer)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AstrologerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Post_Astrologer_Id_fk");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Post_Category_Id_fk");
            });

            modelBuilder.Entity<PostPlanet>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.PlanetId })
                    .HasName("PostPlanets_pk")
                    .IsClustered(false);

                entity.ToTable("PostPlanet");

                entity.HasOne(d => d.Planet)
                    .WithMany(p => p.PostPlanets)
                    .HasForeignKey(d => d.PlanetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostPlanet_Planet_Id_fk");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostPlanets)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostPlanet_Post_Id_fk");
            });

            modelBuilder.Entity<PostZodiac>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.ZodiacId })
                    .HasName("PostZodiacs_pk")
                    .IsClustered(false);

                entity.ToTable("PostZodiac");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostZodiacs)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostZodiac_Post_Id_fk");

                entity.HasOne(d => d.Zodiac)
                    .WithMany(p => p.PostZodiacs)
                    .HasForeignKey(d => d.ZodiacId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PostZodiac_Zodiac_Id_fk");
            });

            modelBuilder.Entity<PriceTable>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PriceTables_pk")
                    .IsClustered(false);

                entity.ToTable("PriceTable");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Roles_pk")
                    .IsClustered(false);

                entity.ToTable("Role");

                entity.Property(e => e.Id).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => new { e.PaymentId, e.WalletId })
                    .HasName("Transactions_pk")
                    .IsClustered(false);

                entity.ToTable("Transaction");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Transaction_Payment_Id_fk");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.WalletId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Transaction_Wallet_Id_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Users_pk")
                    .IsClustered(false);

                entity.ToTable("User");

                entity.HasIndex(e => e.DeletedAt, "IDx_users_deleted_at");

                entity.HasIndex(e => e.Uid, "Users_Uid_uindex")
                    .IsUnique();

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("UserRoles_pk")
                    .IsClustered(false);

                entity.ToTable("UserRole");

                entity.Property(e => e.RoleId).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRole_Role_Id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRole_User_Id_fk");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Wallets_pk")
                    .IsClustered(false);

                entity.ToTable("Wallet");
            });

            modelBuilder.Entity<Zodiac>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Zodiacs_pk")
                    .IsClustered(false);

                entity.ToTable("Zodiac");

                entity.Property(e => e.Date)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UrlImage).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
