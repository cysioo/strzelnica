﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZawodyWin.DB;

#nullable disable

namespace ZawodyWin.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230510193953_Scores")]
    partial class Scores
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("ZawodyWin.DataModels.Competition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("NumberOfRounds")
                        .HasColumnType("INTEGER");

                    b.Property<long>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Competition");
                });

            modelBuilder.Entity("ZawodyWin.DataModels.Contestant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClubName")
                        .HasColumnType("TEXT");

                    b.Property<long>("CompetitionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<long>("PersonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Contestant");
                });

            modelBuilder.Entity("ZawodyWin.DataModels.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClubName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("ZawodyWin.DataModels.Referee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("RefereeClass")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RefereeFunction")
                        .HasColumnType("TEXT");

                    b.Property<long>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Referee");
                });

            modelBuilder.Entity("ZawodyWin.DataModels.Score", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("CompetitionId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("ContestantId")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<long>("Round")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Score");
                });

            modelBuilder.Entity("ZawodyWin.DataModels.ShootingClub", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddressLine1")
                        .HasColumnType("TEXT");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("TEXT");

                    b.Property<string>("License")
                        .HasColumnType("TEXT");

                    b.Property<string>("LogoPath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ShootingClub");
                });

            modelBuilder.Entity("ZawodyWin.DataModels.Tournament", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("TEXT");

                    b.Property<long?>("LeadingRefereeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long?>("OrganizerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Place")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tournament");
                });
#pragma warning restore 612, 618
        }
    }
}
