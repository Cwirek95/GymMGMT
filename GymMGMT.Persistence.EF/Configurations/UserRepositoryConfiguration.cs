﻿using GymMGMT.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymMGMT.Persistence.EF.Configurations
{
    public class UserRepositoryConfiguration : IRepositoryConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(256);
            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(1024);
            builder.Property(x => x.Status)
                .IsRequired()
                .HasDefaultValue(true);
            builder.Property(x => x.RegisteredAt)
                .IsRequired()
                .HasPrecision(0);


            builder.HasOne<Role>(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasOne<Member>(u => u.Member)
                .WithOne(m => m.User)
                .HasForeignKey<Member>(m => m.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Trainer>(u => u.Trainer)
                .WithOne(m => m.User)
                .HasForeignKey<Trainer>(m => m.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}