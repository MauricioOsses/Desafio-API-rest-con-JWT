using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DesafioEncodeBDDO.Models;

public partial class DesafioBackEndBddoContext : DbContext
{
    public DesafioBackEndBddoContext()
    {
    }

    public DesafioBackEndBddoContext(DbContextOptions<DesafioBackEndBddoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DataUser> DataUser { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DataUser>(entity =>
        {
            entity.HasKey(e => e.IdDataUser);

            entity.ToTable("DataUser");

            entity.Property(e => e.IdDataUser).ValueGeneratedNever();
            entity.Property(e => e.Dni).HasColumnName("DNI");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("User");

            entity.Property(e => e.IdUser).ValueGeneratedNever();
            entity.Property(e => e.NameUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
