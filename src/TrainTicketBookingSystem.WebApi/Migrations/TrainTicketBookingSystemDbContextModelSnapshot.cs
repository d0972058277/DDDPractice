﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrainTicketBookingSystem.WebApi;

#nullable disable

namespace TrainTicketBookingSystem.WebApi.Migrations
{
    [DbContext(typeof(TrainTicketBookingSystemDbContext))]
    partial class TrainTicketBookingSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TrainTicketBookingSystem.Domain.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<Guid>("TrainId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Ticket", (string)null);
                });

            modelBuilder.Entity("TrainTicketBookingSystem.Domain.Models.Train", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Train", (string)null);
                });

            modelBuilder.Entity("TrainTicketBookingSystem.Domain.Models.Ticket", b =>
                {
                    b.OwnsOne("TrainTicketBookingSystem.Domain.Models.Date", "Date", b1 =>
                        {
                            b1.Property<Guid>("TicketId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("Date");

                            b1.HasKey("TicketId");

                            b1.ToTable("Ticket");

                            b1.WithOwner()
                                .HasForeignKey("TicketId");
                        });

                    b.OwnsOne("TrainTicketBookingSystem.Domain.Models.Location", "From", b1 =>
                        {
                            b1.Property<Guid>("TicketId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("From");

                            b1.HasKey("TicketId");

                            b1.ToTable("Ticket");

                            b1.WithOwner()
                                .HasForeignKey("TicketId");
                        });

                    b.OwnsOne("TrainTicketBookingSystem.Domain.Models.Location", "To", b1 =>
                        {
                            b1.Property<Guid>("TicketId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("To");

                            b1.HasKey("TicketId");

                            b1.ToTable("Ticket");

                            b1.WithOwner()
                                .HasForeignKey("TicketId");
                        });

                    b.Navigation("Date")
                        .IsRequired();

                    b.Navigation("From")
                        .IsRequired();

                    b.Navigation("To")
                        .IsRequired();
                });

            modelBuilder.Entity("TrainTicketBookingSystem.Domain.Models.Train", b =>
                {
                    b.OwnsOne("TrainTicketBookingSystem.Domain.Models.Date", "Date", b1 =>
                        {
                            b1.Property<Guid>("TrainId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("Date");

                            b1.HasKey("TrainId");

                            b1.ToTable("Train");

                            b1.WithOwner()
                                .HasForeignKey("TrainId");
                        });

                    b.OwnsMany("TrainTicketBookingSystem.Domain.Models.Location", "Locations", b1 =>
                        {
                            b1.Property<long>("_id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint");

                            MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b1.Property<long>("_id"));

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("longtext")
                                .HasColumnName("Name");

                            b1.Property<Guid>("TrainId")
                                .HasColumnType("char(36)");

                            b1.HasKey("_id");

                            b1.HasIndex("TrainId");

                            b1.ToTable("TrainLocation", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("TrainId");
                        });

                    b.Navigation("Date")
                        .IsRequired();

                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
