using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repository.GeneratedModels
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
        {
        }

        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cash> Cashes { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UsersInGroup> UsersInGroups { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=MyDBConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cash>(entity =>
            {
                entity.HasKey(e => e.CashCode)
                    .HasName("Cashes_pkey");

                entity.Property(e => e.CashCode)
                    .HasColumnName("cash_code")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Deadline)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("deadline");

                entity.Property(e => e.Frequency)
                    .HasMaxLength(100)
                    .HasColumnName("frequency");

                entity.Property(e => e.GroupCode).HasColumnName("group_code");

                entity.Property(e => e.GroupDivisionMethod)
                    .HasMaxLength(100)
                    .HasColumnName("group_division_method");

                entity.Property(e => e.GroupGoal)
                    .HasMaxLength(100)
                    .HasColumnName("group_goal");

                entity.Property(e => e.GroupSum).HasColumnName("group_sum");

                entity.Property(e => e.ReminderCall)
                    .HasMaxLength(100)
                    .HasColumnName("reminder_call");

                entity.HasOne(d => d.GroupCodeNavigation)
                    .WithMany(p => p.Cashes)
                    .HasForeignKey(d => d.GroupCode)
                    .HasConstraintName("fk_grpcode_cashes");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.GroupCode)
                    .HasName("Groups_pkey");

                entity.Property(e => e.GroupCode)
                    .HasColumnName("group_code")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.GroupDescription)
                    .HasMaxLength(100)
                    .HasColumnName("group_description ");

                entity.Property(e => e.GroupType)
                    .HasMaxLength(50)
                    .HasColumnName("group_type");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PayCode)
                    .HasName("Payments_pkey");

                entity.Property(e => e.PayCode)
                    .HasColumnName("pay_code")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.CashCode).HasColumnName("cash_code");

                entity.Property(e => e.Confirmation).HasColumnName("confirmation");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("payment_date");

                entity.Property(e => e.PaymentDescription).HasColumnName("payment_description");

                entity.Property(e => e.PaymentWay)
                    .HasMaxLength(100)
                    .HasColumnName("payment_way");

                entity.Property(e => e.SumPaid).HasColumnName("sum_paid");

                entity.Property(e => e.SumToPay).HasColumnName("sum_to_pay");

                entity.Property(e => e.UserCode).HasColumnName("user_code");

                entity.HasOne(d => d.CashCodeNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CashCode)
                    .HasConstraintName("fk_cashcode_payments");

                entity.HasOne(d => d.UserCodeNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserCode)
                    .HasConstraintName("fk_usrcode_payments");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserCode)
                    .HasName("Users_pkey");

                entity.Property(e => e.UserCode)
                    .HasColumnName("user_code")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(20)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(20)
                    .HasColumnName("last_name");

                entity.Property(e => e.UserId)
                    .HasMaxLength(9)
                    .HasColumnName("user_id");

                entity.Property(e => e.UserMail)
                    .HasMaxLength(50)
                    .HasColumnName("user_mail");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(8)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(12)
                    .HasColumnName("user_phone");
            });

            modelBuilder.Entity<UsersInGroup>(entity =>
            {
                entity.HasKey(e => new { e.GroupCode, e.UserCode })
                    .HasName("Users_In_Group_pkey");

                entity.ToTable("Users_In_Group");

                entity.Property(e => e.GroupCode).HasColumnName("group_code");

                entity.Property(e => e.UserCode).HasColumnName("user_code");

                entity.Property(e => e.UserType)
                    .HasMaxLength(20)
                    .HasColumnName("user_type");

                entity.HasOne(d => d.GroupCodeNavigation)
                    .WithMany(p => p.UsersInGroups)
                    .HasForeignKey(d => d.GroupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_grpcode_users_in_group");

                entity.HasOne(d => d.UserCodeNavigation)
                    .WithMany(p => p.UsersInGroups)
                    .HasForeignKey(d => d.UserCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_usrcode_users_in_group");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
