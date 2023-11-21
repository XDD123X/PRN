using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Matcher.Models
{
    public partial class VoVoContext : DbContext
    {
        public VoVoContext()
        {
        }

        public VoVoContext(DbContextOptions<VoVoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Ipbanned> Ipbanneds { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<LikeLimit> LikeLimits { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<Setting> Settings { get; set; } = null!;
        public virtual DbSet<Statistic> Statistics { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<UpgradeHistory> UpgradeHistories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserPhoto> UserPhotos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ipbanned>(entity =>
            {
                entity.HasKey(e => e.BannedIp)
                    .HasName("PK__IPBanned__30492EF94852FA31");

                entity.ToTable("IPBanned");

                entity.Property(e => e.BannedIp)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.Property(e => e.LikeId).HasColumnName("LikeID");

                entity.Property(e => e.DateLiked).HasColumnType("date");

                entity.Property(e => e.LikedUserId).HasColumnName("LikedUserID");

                entity.Property(e => e.LikerId).HasColumnName("LikerID");

                entity.HasOne(d => d.LikedUser)
                    .WithMany(p => p.LikeLikedUsers)
                    .HasForeignKey(d => d.LikedUserId)
                    .HasConstraintName("FK__Likes__LikedUser__2F10007B");

                entity.HasOne(d => d.Liker)
                    .WithMany(p => p.LikeLikers)
                    .HasForeignKey(d => d.LikerId)
                    .HasConstraintName("FK__Likes__LikerID__2E1BDC42");
            });

            modelBuilder.Entity<LikeLimit>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__LikeLimi__1788CCACC0CDFAB4");

                entity.ToTable("LikeLimit");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.LikeLimit)
                    .HasForeignKey<LikeLimit>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LikeLimit__UserI__31EC6D26");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.MatchId).HasColumnName("MatchID");

                entity.Property(e => e.DateMatched).HasColumnType("date");

                entity.Property(e => e.MatchedUserId).HasColumnName("MatchedUserID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.MatchedUser)
                    .WithMany(p => p.MatchMatchedUsers)
                    .HasForeignKey(d => d.MatchedUserId)
                    .HasConstraintName("FK__Matches__Matched__35BCFE0A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MatchUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Matches__UserID__34C8D9D1");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.DateSent).HasColumnType("datetime");

                entity.Property(e => e.FromUserId).HasColumnName("FromUserID");

                entity.Property(e => e.MessageText).HasColumnType("text");

                entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.MessageFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .HasConstraintName("FK__Messages__FromUs__38996AB5");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.MessageToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .HasConstraintName("FK__Messages__ToUser__398D8EEE");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(e => e.PlanId).HasColumnName("PlanID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.PlanName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Status).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Report__UserID__403A8C7D");
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");

                entity.Property(e => e.SettingId).HasColumnName("SettingID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')");
            });

            modelBuilder.Entity<Statistic>(entity =>
            {
                entity.ToTable("Statistic");

                entity.Property(e => e.StatisticId).HasColumnName("StatisticID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.SubscriptionId).HasColumnName("SubscriptionID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentStatus)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('PENDING')");

                entity.Property(e => e.PlanId).HasColumnName("PlanID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TotalCost).HasColumnType("money");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Subscript__PlanI__4BAC3F29");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Subscript__UserI__4AB81AF0");
            });

            modelBuilder.Entity<UpgradeHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UpgradeHistory");

                entity.Property(e => e.PlanId).HasColumnName("PlanID");

                entity.Property(e => e.TotalCost).HasColumnType("money");

                entity.Property(e => e.UpgradeDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Plan)
                    .WithMany()
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK__UpgradeHi__PlanI__4F7CD00D");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UpgradeHi__UserI__4E88ABD4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ipaddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("IPAddress")
                    .HasDefaultValueSql("('::1')");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UserType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('FREE')");
            });

            modelBuilder.Entity<UserPhoto>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.PhotoLink)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserPhoto__UserI__2B3F6F97");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
