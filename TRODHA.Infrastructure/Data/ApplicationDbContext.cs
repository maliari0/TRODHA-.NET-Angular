using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRODHA.Core.Models;

namespace TRODHA.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ImportanceLevel> ImportanceLevels { get; set; } = null!;
        public DbSet<Recommendation> Recommendations { get; set; } = null!;
        public DbSet<Goal> Goals { get; set; } = null!;
        public DbSet<GoalStatus> GoalStatuses { get; set; } = null!;
        public DbSet<UserNote> UserNotes { get; set; } = null!;
        public DbSet<NoteImage> NoteImages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Users
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
                entity.Property(e => e.PasswordHash).HasMaxLength(255).IsRequired();
                entity.Property(e => e.PasswordSalt).HasMaxLength(100).IsRequired();
                entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");
            });

            // Configure ImportanceLevels
            modelBuilder.Entity<ImportanceLevel>(entity =>
            {
                entity.HasKey(e => e.LevelId);
                entity.HasIndex(e => e.LevelName).IsUnique();

                entity.Property(e => e.LevelName).HasMaxLength(20).IsRequired();
            });

            // Configure Recommendations
            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.HasKey(e => e.RecommendationId);

                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Recommendations)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired(false);
            });

            // Configure Goals
            modelBuilder.Entity<Goal>(entity =>
            {
                entity.HasKey(e => e.GoalId);

                entity.Property(e => e.Title).HasMaxLength(100).IsRequired();
                entity.Property(e => e.PeriodUnit).HasMaxLength(20).IsRequired();
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Goals)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ImportanceLevel)
                      .WithMany(i => i.Goals)
                      .HasForeignKey(e => e.ImportanceLevelId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasCheckConstraint("CK_Goals_PeriodUnit", "PeriodUnit IN ('günde', 'haftada', 'ayda', 'yılda')");
                entity.HasCheckConstraint("CK_Goals_Frequency", "Frequency BETWEEN 1 AND 6");
            });

            // Configure GoalStatuses
            modelBuilder.Entity<GoalStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.Property(e => e.IsCompleted).HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Goal)
                      .WithMany(g => g.Statuses)
                      .HasForeignKey(e => e.GoalId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.User)
                      .WithMany(u => u.GoalStatuses)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure UserNotes
            modelBuilder.Entity<UserNote>(entity =>
            {
                entity.HasKey(e => e.NoteId);

                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Notes)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure NoteImages
            modelBuilder.Entity<NoteImage>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.ImagePath).HasMaxLength(255).IsRequired();
                entity.Property(e => e.ImageType).HasMaxLength(20).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Note)
                      .WithMany(n => n.Images)
                      .HasForeignKey(e => e.NoteId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasCheckConstraint("CK_NoteImages_ImageType", "ImageType IN ('jpeg', 'png', 'gif', 'bmp', 'tiff')");
            });

            // Seed ImportanceLevels
            modelBuilder.Entity<ImportanceLevel>().HasData(
                new ImportanceLevel { LevelId = 1, LevelName = "düşük" },
                new ImportanceLevel { LevelId = 2, LevelName = "orta" },
                new ImportanceLevel { LevelId = 3, LevelName = "yüksek" }
            );
        }
    }
}

