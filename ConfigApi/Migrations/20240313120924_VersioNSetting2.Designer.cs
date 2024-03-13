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
    [Migration("20240313120924_VersioNSetting2")]
    partial class VersioNSetting2
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

                    b.Property<int?>("SettingId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("SettingId");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("ConfigApi.Model.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("ConfigApi.Model.ServiceVersion", b =>
                {
                    b.HasOne("ConfigApi.Model.Service", "ServiceObj")
                        .WithMany("Versions")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConfigApi.Model.Setting", "SettingObj")
                        .WithMany()
                        .HasForeignKey("SettingId");

                    b.Navigation("ServiceObj");

                    b.Navigation("SettingObj");
                });

            modelBuilder.Entity("ConfigApi.Model.Setting", b =>
                {
                    b.HasOne("ConfigApi.Model.Service", "ServiceObj")
                        .WithMany("Settings")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ServiceObj");
                });

            modelBuilder.Entity("ConfigApi.Model.Service", b =>
                {
                    b.Navigation("Settings");

                    b.Navigation("Versions");
                });
#pragma warning restore 612, 618
        }
    }
}