﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250408083607_UniqueBuild")]
    partial class UniqueBuild
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Attachment", b =>
                {
                    b.Property<Guid>("IdAttachment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("IdAttachment");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("FileSource")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FileSource");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FileType");

                    b.Property<Guid>("IdAttendance")
                        .HasColumnType("uuid")
                        .HasColumnName("IdAttendance");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uuid")
                        .HasColumnName("IdUser");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("IdAttachment");

                    b.HasIndex("IdAttendance");

                    b.HasIndex("IdUser");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Domain.Attendance", b =>
                {
                    b.Property<Guid>("IdAttendance")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("IdAttendance");

                    b.Property<Guid>("ApprovedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("ApprovedBy");

                    b.Property<DateTimeOffset>("CheckIn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CheckIn");

                    b.Property<DateTimeOffset?>("CheckOut")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CheckOut");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Date");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uuid")
                        .HasColumnName("IdUser");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Notes");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("IdAttendance");

                    b.HasIndex("IdUser");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("Domain.Division", b =>
                {
                    b.Property<Guid>("IdDivision")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("IdDivision");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("DivisionName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DivisionName");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("IdDivision");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("Domain.Leave", b =>
                {
                    b.Property<Guid>("IdLeaves")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("IdLeaves");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uuid")
                        .HasColumnName("IdUser");

                    b.Property<DateTime>("LeaveExpiry")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("LeaveExpiry");

                    b.Property<int>("RemainingLeave")
                        .HasColumnType("integer")
                        .HasColumnName("RemainingLeave");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("IdLeaves");

                    b.HasIndex("IdUser")
                        .IsUnique();

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("Domain.LeaveTransaction", b =>
                {
                    b.Property<Guid>("IdTransaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("IdTransaction");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("EndDate");

                    b.Property<Guid>("IdDivision")
                        .HasColumnType("uuid")
                        .HasColumnName("IdDivision");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uuid")
                        .HasColumnName("IdUser");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("RequestDate");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("StartDate");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("Status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("IdTransaction");

                    b.HasIndex("IdDivision");

                    b.HasIndex("IdUser");

                    b.ToTable("LeaveTransactions");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<Guid>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("IdUser");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Email");

                    b.Property<Guid>("IdDivision")
                        .HasColumnType("uuid")
                        .HasColumnName("IdDivision");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Role");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Status");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("IdUser");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("IdDivision");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Attachment", b =>
                {
                    b.HasOne("Domain.Attendance", "Attendance")
                        .WithMany("Attachments")
                        .HasForeignKey("IdAttendance")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("Attachments")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attendance");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Attendance", b =>
                {
                    b.HasOne("Domain.User", "User")
                        .WithMany("Attendances")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Leave", b =>
                {
                    b.HasOne("Domain.User", "Users")
                        .WithMany("Leaves")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.LeaveTransaction", b =>
                {
                    b.HasOne("Domain.Division", "Division")
                        .WithMany("LeaveTransactions")
                        .HasForeignKey("IdDivision")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("LeaveTransactions")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.HasOne("Domain.Division", "Division")
                        .WithMany("Users")
                        .HasForeignKey("IdDivision")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Division");
                });

            modelBuilder.Entity("Domain.Attendance", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Domain.Division", b =>
                {
                    b.Navigation("LeaveTransactions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("Attachments");

                    b.Navigation("Attendances");

                    b.Navigation("LeaveTransactions");

                    b.Navigation("Leaves");
                });
#pragma warning restore 612, 618
        }
    }
}
