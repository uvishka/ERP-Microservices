﻿// <auto-generated />
using System;
using ERP.TimeTable.DataService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ERP.TimeTable.DataService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("ERP.TimeTable.Core.Entities.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("ERP.TimeTable.Core.Entities.LectureHall", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Capacity")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("HallName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LectureHalls");
                });

            modelBuilder.Entity("ERP.TimeTable.Core.Entities.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("ERP.TimeTable.Core.Entities.TimeSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("DayId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("LectureHallId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModuleId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.HasIndex("LectureHallId");

                    b.HasIndex("ModuleId");

                    b.ToTable("TimeSlots");
                });

            modelBuilder.Entity("ERP.TimeTable.Core.Entities.TimeSlot", b =>
                {
                    b.HasOne("ERP.TimeTable.Core.Entities.Day", "Day")
                        .WithMany("TimeSlots")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_TimeSlots_Day");

                    b.HasOne("ERP.TimeTable.Core.Entities.LectureHall", "LectureHall")
                        .WithMany("TimeSlots")
                        .HasForeignKey("LectureHallId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_TimeSlots_LectureHall");

                    b.HasOne("ERP.TimeTable.Core.Entities.Module", "Module")
                        .WithMany("TimeSlots")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("FK_TimeSlots_Module");

                    b.Navigation("Day");

                    b.Navigation("LectureHall");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("ERP.TimeTable.Core.Entities.Day", b =>
                {
                    b.Navigation("TimeSlots");
                });

            modelBuilder.Entity("ERP.TimeTable.Core.Entities.LectureHall", b =>
                {
                    b.Navigation("TimeSlots");
                });

            modelBuilder.Entity("ERP.TimeTable.Core.Entities.Module", b =>
                {
                    b.Navigation("TimeSlots");
                });
#pragma warning restore 612, 618
        }
    }
}