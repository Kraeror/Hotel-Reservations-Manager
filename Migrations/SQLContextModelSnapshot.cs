﻿// <auto-generated />
using System;
using Hotel_Reservations_Manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hotel_Reservations_Manager.Migrations
{
    [DbContext(typeof(SQLContext))]
    partial class SQLContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hotel_Reservations_Manager.Models.ClientsModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("familyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(21)");

                    b.Property<bool>("isAdoult")
                        .HasColumnType("bit");

                    b.Property<bool>("isBusy")
                        .HasColumnType("bit");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("ID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Hotel_Reservations_Manager.Models.ReservationsModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AccommodationDate")
                        .HasColumnType("DateTime2");

                    b.Property<string>("ClientsInTheRoom")
                        .HasColumnType("varchar(64)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(12,2)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("DateTime2");

                    b.Property<string>("ReservatedBy")
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int>("foreignID")
                        .HasColumnType("int");

                    b.Property<bool>("isAllInclusive")
                        .HasColumnType("bit");

                    b.Property<bool>("isBreakfastIncluded")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Hotel_Reservations_Manager.Models.RoomsModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AdoultPrice")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<decimal>("KidsPrice")
                        .HasColumnType("decimal(5,2)");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(64)");

                    b.Property<bool>("isFree")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Hotel_Reservations_Manager.Models.UsersModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("DateTime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("IDNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(51)");

                    b.Property<string>("familyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(21)");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(13)");

                    b.Property<DateTime?>("releaseDate")
                        .HasColumnType("DateTime2");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
