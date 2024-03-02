﻿// <auto-generated />
using System;
using ICS.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ICS.DAL.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("ICS.DAL.ActivityEntity", b =>
                {
                    b.Property<int>("activityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("activityTypeTag")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("end")
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("room")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("start")
                        .HasColumnType("TEXT");

                    b.Property<int>("subjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("activityId");

                    b.HasIndex("subjectId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("ICS.DAL.RatingEntity", b =>
                {
                    b.Property<int>("ratingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("activityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("points")
                        .HasColumnType("INTEGER");

                    b.Property<int>("studentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ratingId");

                    b.HasIndex("activityId")
                        .IsUnique();

                    b.HasIndex("studentId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("ICS.DAL.StudentEntity", b =>
                {
                    b.Property<int>("studentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("fotoURL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("studentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ICS.DAL.SubjectEntity", b =>
                {
                    b.Property<int>("subjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("abbreviation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("subjectId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("StudentEntitySubjectEntity", b =>
                {
                    b.Property<int>("studentsstudentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("subjectssubjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("studentsstudentId", "subjectssubjectId");

                    b.HasIndex("subjectssubjectId");

                    b.ToTable("StudentSubject", (string)null);
                });

            modelBuilder.Entity("ICS.DAL.ActivityEntity", b =>
                {
                    b.HasOne("ICS.DAL.SubjectEntity", "subject")
                        .WithMany("activity")
                        .HasForeignKey("subjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("subject");
                });

            modelBuilder.Entity("ICS.DAL.RatingEntity", b =>
                {
                    b.HasOne("ICS.DAL.ActivityEntity", "activity")
                        .WithOne("rating")
                        .HasForeignKey("ICS.DAL.RatingEntity", "activityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICS.DAL.StudentEntity", "student")
                        .WithMany()
                        .HasForeignKey("studentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("activity");

                    b.Navigation("student");
                });

            modelBuilder.Entity("StudentEntitySubjectEntity", b =>
                {
                    b.HasOne("ICS.DAL.StudentEntity", null)
                        .WithMany()
                        .HasForeignKey("studentsstudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ICS.DAL.SubjectEntity", null)
                        .WithMany()
                        .HasForeignKey("subjectssubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ICS.DAL.ActivityEntity", b =>
                {
                    b.Navigation("rating");
                });

            modelBuilder.Entity("ICS.DAL.SubjectEntity", b =>
                {
                    b.Navigation("activity");
                });
#pragma warning restore 612, 618
        }
    }
}
