﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movie_Theater_Model;

#nullable disable

namespace Movie_Theater_Model.Migrations
{
    [DbContext(typeof(MovieTheatherContext))]
    [Migration("20240410220315_syncDatabase2")]
    partial class syncDatabase2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Movie_Theater_Model.Models.Logins", b =>
                {
                    b.Property<int>("loginID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LoginID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("loginID"));

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Last Name");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Password");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("username");

                    b.HasKey("loginID");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("Movie_Theater_Model.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("MovieID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"));

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("director");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("genre");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("ReleaseYear")
                        .HasColumnType("date")
                        .HasColumnName("release_year");

                    b.Property<int>("Runtime")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title");

                    b.HasKey("MovieId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Movie_Theater_Model.Models.ScreenTime", b =>
                {
                    b.Property<int>("ScreeningId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Screening_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScreeningId"));

                    b.Property<int>("MovieId")
                        .HasColumnType("int")
                        .HasColumnName("Movie_ID");

                    b.Property<TimeOnly>("ScreenTime1")
                        .HasPrecision(2)
                        .HasColumnType("time(2)")
                        .HasColumnName("Screen_time");

                    b.Property<int>("TheatherId")
                        .HasColumnType("int")
                        .HasColumnName("Theather_ID");

                    b.HasKey("ScreeningId");

                    b.HasIndex("MovieId");

                    b.HasIndex("TheatherId");

                    b.ToTable("ScreenTime", (string)null);
                });

            modelBuilder.Entity("Movie_Theater_Model.Models.Theather", b =>
                {
                    b.Property<int>("TheatherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Theather_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TheatherId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("city");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("SeatCapacity")
                        .HasColumnType("int")
                        .HasColumnName("seat_capacity");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .IsUnicode(false)
                        .HasColumnType("char(2)")
                        .HasColumnName("state")
                        .IsFixedLength();

                    b.Property<string>("Zipcode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .IsUnicode(false)
                        .HasColumnType("char(5)")
                        .HasColumnName("zipcode")
                        .IsFixedLength();

                    b.HasKey("TheatherId");

                    b.ToTable("Theather", (string)null);
                });

            modelBuilder.Entity("Movie_Theater_Model.Models.ScreenTime", b =>
                {
                    b.HasOne("Movie_Theater_Model.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .IsRequired()
                        .HasConstraintName("FK_ScreenTime_Movies");

                    b.HasOne("Movie_Theater_Model.Models.Theather", "Theather")
                        .WithMany()
                        .HasForeignKey("TheatherId")
                        .IsRequired()
                        .HasConstraintName("FK_ScreenTime_Theather");

                    b.Navigation("Movie");

                    b.Navigation("Theather");
                });
#pragma warning restore 612, 618
        }
    }
}
