using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SecondProject.DbModel.Models;

#nullable disable

namespace SecondProject.DbModel.Models
{
    public partial class secdbContext : DbContext
    {
        public bool IgnoreFilter { get; set; }
        public secdbContext()
        {
        }

        public secdbContext(DbContextOptions<secdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SecView> SecViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Server=localhost;port=3306;user=root;password=1997;database=secdb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("item");

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "ItemId_UserId_idx");

                entity.Property(e => e.Id).HasColumnType("int unsigned");


                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Image)
                   .IsRequired()
                   .HasColumnType("varchar(255)")
                   .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate)
                   .HasColumnType("timestamp")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedDate)
                   .HasColumnType("timestamp")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Archived).HasColumnType("tinyint");

                entity.Property(e => e.IsRead).HasColumnType("tinyint");


                entity.Property(e => e.UserId).HasColumnType("int unsigned");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_cID");

                entity.Property(e => e.CreatorId).HasColumnType("int unsigned");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("item_uId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "Email_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "Id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int unsigned");

                entity.Property(e => e.ConfirmPassword)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Image)
                   .IsRequired()
                   .HasColumnType("varchar(255)")
                   .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate)
                   .HasColumnType("timestamp")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedDate)
                   .HasColumnType("timestamp")
                   .ValueGeneratedOnAddOrUpdate()
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsAdmin).HasColumnType("tinyint");

                entity.Property(e => e.Archived).HasColumnType("tinyint");
            });

            modelBuilder.Entity<SecView>(entity =>
            {
                entity.ToTable("secview");

                entity.HasNoKey();

                entity.Property(e => e.UserEmail).HasColumnType("varchar(255)");

                entity.Property(e => e.ItemId).HasColumnType("int unsigned");
                entity.Property(e => e.UserId).HasColumnType("int unsigned");



                entity.Property(e => e.UserFirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UserLastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ItemTitle)
                    .IsRequired()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ItemContent)
                    .IsRequired()
                    .HasColumnType("TEXT")
                    .HasDefaultValueSql("''");


            });

            modelBuilder.Entity<Item>().HasQueryFilter(a => !a.Archived || IgnoreFilter);
            modelBuilder.Entity<User>().HasQueryFilter(a => !a.Archived || IgnoreFilter);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
