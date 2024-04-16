using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CapaDatos.Models;

public partial class DasignoContext : DbContext
{
    public readonly IConfiguration _configuration;
    public DasignoContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DasignoContext(DbContextOptions<DasignoContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<LogUpdate> LogUpdates { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CadenaSQL"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LogUpdate>(entity =>
        {
            entity.ToTable("Log_Update");

            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.ToTable("Persona");

            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
