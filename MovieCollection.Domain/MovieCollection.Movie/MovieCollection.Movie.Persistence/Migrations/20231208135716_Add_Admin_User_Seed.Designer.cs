﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieCollection.Movie.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieCollection.Movie.Persistence.Migrations
{
    [DbContext(typeof(MovieDataContext))]
    [Migration("20231208135716_Add_Admin_User_Seed")]
    partial class Add_Admin_User_Seed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MovieCollection.Movie.Domain.Entities.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Collection");
                });

            modelBuilder.Entity("MovieCollection.Movie.Domain.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("MovieCollection.Movie.Domain.Entities.MovieCollection", b =>
                {
                    b.Property<int>("CollectionId")
                        .HasColumnType("integer");

                    b.Property<int>("MovieId")
                        .HasColumnType("integer");

                    b.Property<int>("CollectionId1")
                        .HasColumnType("integer");

                    b.HasKey("CollectionId", "MovieId");

                    b.HasIndex("CollectionId1");

                    b.ToTable("MovieCollection");
                });

            modelBuilder.Entity("MovieCollection.Movie.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2023, 12, 8, 14, 57, 16, 631, DateTimeKind.Local).AddTicks(9316),
                            Email = "test@mail.com",
                            FirstName = "Super",
                            HashedPassword = "AQAAAAEAACcQAAAAEAap7bv4XkwO9GMc9E19yA5qcnHJYwttBDlZmUODzn/h2Bx6DQOl5VMOg09am5cAWA==",
                            IsActive = false,
                            LastName = "Admin",
                            UserRole = 1
                        });
                });

            modelBuilder.Entity("MovieCollection.Movie.Domain.Entities.Collection", b =>
                {
                    b.HasOne("MovieCollection.Movie.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieCollection.Movie.Domain.Entities.MovieCollection", b =>
                {
                    b.HasOne("MovieCollection.Movie.Domain.Entities.Movie", "Movie")
                        .WithMany("MovieCollection")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieCollection.Movie.Domain.Entities.Collection", "Collection")
                        .WithMany("MovieCollection")
                        .HasForeignKey("CollectionId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Collection");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieCollection.Movie.Domain.Entities.Collection", b =>
                {
                    b.Navigation("MovieCollection");
                });

            modelBuilder.Entity("MovieCollection.Movie.Domain.Entities.Movie", b =>
                {
                    b.Navigation("MovieCollection");
                });
#pragma warning restore 612, 618
        }
    }
}
