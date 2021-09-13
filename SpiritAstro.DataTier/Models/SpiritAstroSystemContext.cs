using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SpiritAstro.DataTier.Models
{
    public partial class SpiritAstroSystemContext : DbContext
    {
        public SpiritAstroSystemContext()
        {
        }

        public SpiritAstroSystemContext(DbContextOptions<SpiritAstroSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<SubCategoryPost> SubCategoryPosts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserPost> UserPosts { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:bacnvse141019.database.windows.net,1433;Initial Catalog=SpiritAstroSystem;Persist Security Info=False;User ID=nhatnhanchiha;Password=SE1416password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Categories_pk")
                    .IsClustered(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Invoices_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.DeletedAt, "idx_Invoices_deleted_at");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.InvoiceCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Invoices_Users_Id_fk_2");

                entity.HasOne(d => d.Professor)
                    .WithMany(p => p.InvoiceProfessors)
                    .HasForeignKey(d => d.ProfessorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Invoices_Users_Id_fk");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Posts_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.DeletedAt, "idx_Posts_deleted_at");

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.IsApprove).HasComment("0 là chua\n1 là ok");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(1024);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Roles_pk")
                    .IsClustered(false);

                entity.Property(e => e.Id).HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("SubCategories_pk")
                    .IsClustered(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubCategories_Categories_Id_fk");
            });

            modelBuilder.Entity<SubCategoryPost>(entity =>
            {
                entity.HasKey(e => new { e.SubCategoryId, e.PostId })
                    .HasName("SubCategoryPosts_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.SubCategoryPosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubCategoryPosts_Posts_Id_fk");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.SubCategoryPosts)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubCategoryPosts_SubCategories_Id_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Users_pk")
                    .IsClustered(false);

                entity.HasIndex(e => e.PhoneNumber, "Users_PhoneNumber_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.DeletedAt, "idx_Users_deleted_at");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DayOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Gender).HasComment("0 là nam (m?c d?nh)\n1 là n?");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<UserPost>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PostId })
                    .HasName("UserPosts_pk")
                    .IsClustered(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.UserPosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserPosts_Posts_Id_fk");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPosts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserPosts_Users_Id_fk");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("UserRoles_pk")
                    .IsClustered(false);

                entity.Property(e => e.RoleId).HasMaxLength(255);

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
