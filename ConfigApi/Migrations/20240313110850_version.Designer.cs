﻿// <auto-generated />
using System;
using ConfigApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConfigApi.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240313110850_version")]
    partial class version
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("ConfigApi.Model.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ConfigApi.Model.ServiceVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<byte>("ServiceConfiguration")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("ConfigApi.Model.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServiceVersionId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServiceVersionId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("ConfigApi.Model.ServiceVersion", b =>
                {
                    b.HasOne("ConfigApi.Model.Service", "ServiceObj")
                        .WithMany("Versions")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceObj");
                });

            modelBuilder.Entity("ConfigApi.Model.Setting", b =>
                {
                    b.HasOne("ConfigApi.Model.ServiceVersion", "ServiceVersionObj")
                        .WithMany("Settings")
                        .HasForeignKey("ServiceVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceVersionObj");
                });

            modelBuilder.Entity("ConfigApi.Model.Service", b =>
                {
                    b.Navigation("Versions");
                });

            modelBuilder.Entity("ConfigApi.Model.ServiceVersion", b =>
                {
                    b.Navigation("Settings");
                });
#pragma warning restore 612, 618
        }
    }
}