using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movie_Theater_Model.Models;


namespace Movie_Theater_Model;

public partial class MovieTheatherContext : IdentityDbContext<UserLogins>
{
    private readonly IConfiguration _config;
   

    public MovieTheatherContext(DbContextOptions<MovieTheatherContext> options, IConfiguration config)
        : base(options)
        
    {
        _config = config;
    }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<ScreenTime> ScreenTimes { get; set; }

    public virtual DbSet<Theather> Theathers { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("production"));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.Property(e => e.MovieId).HasColumnName("MovieID");
            entity.Property(e => e.Director)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("director");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genre");
            entity.Property(e => e.ReleaseYear).HasColumnName("release_year");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<ScreenTime>(entity =>
        {
            
            entity.HasKey(e => e.ScreeningId);

            entity.ToTable("ScreenTime");

            entity.Property(e => e.MovieId).HasColumnName("Movie_ID");
            entity.Property(e => e.ScreenTime1)
                .HasPrecision(2)
                .HasColumnName("Screen_time");
            entity.Property(e => e.ScreeningId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Screening_ID");
            entity.Property(e => e.TheatherId).HasColumnName("Theather_ID");

            entity.HasOne(d => d.Movie).WithMany()
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScreenTime_Movies");

            entity.HasOne(d => d.Theather).WithMany()
                .HasForeignKey(d => d.TheatherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScreenTime_Theather");
        });

        modelBuilder.Entity<Theather>(entity =>
        {
            entity.ToTable("Theather");

            entity.Property(e => e.TheatherId).HasColumnName("Theather_ID");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SeatCapacity).HasColumnName("seat_capacity");
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("state");
            entity.Property(e => e.Zipcode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("zipcode");
        });


        modelBuilder.Entity<IdentityUserLogin<string>>()
            .HasKey(login => login.UserId);


        modelBuilder.Entity<IdentityUserRole<string>>()
        .HasKey(ur => new { ur.UserId, ur.RoleId });

        modelBuilder.Entity<IdentityUserToken<string>>()
        .HasKey(token => new { token.UserId, token.LoginProvider, token.Name });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
