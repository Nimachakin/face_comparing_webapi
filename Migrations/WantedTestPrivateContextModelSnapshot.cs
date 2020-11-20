﻿// <auto-generated />
using System;
using FaceRecognitionApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FaceRecognitionApi.Migrations
{
    [DbContext(typeof(WantedTestPrivateContext))]
    partial class WantedTestPrivateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FaceRecognitionApi.Models.AspNetRole", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasMaxLength(128);

                    b.Property<string>("Description")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.AspNetUser", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasMaxLength(128);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("text")
                        .IsUnicode(false);

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Initials")
                        .HasColumnType("text")
                        .IsUnicode(false);

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LockoutEndDateUtc")
                        .HasColumnType("date");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("RequestedRoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.AspNetUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.AspNetUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("character varying(128)")
                        .HasMaxLength(128);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasMaxLength(128);

                    b.HasKey("LoginProvider", "ProviderKey", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.AspNetUserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasMaxLength(128);

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid")
                        .HasMaxLength(128);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasColumnType("text")
                        .IsUnicode(false);

                    b.Property<bool>("Gender")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("PhotoFaceId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PhotoProfileId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("RemoveDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("current_date");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.PhotoFace", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .IsUnicode(false);

                    b.Property<string>("Mime")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.Property<Guid>("RowId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("current_date");

                    b.HasKey("PhotoId");

                    b.HasIndex("RowId")
                        .IsUnique()
                        .HasName("UQ__Photo__FFEE743086D66C32");

                    b.ToTable("PhotoFaces");
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.PhotoFaceDescriptor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Descriptor")
                        .HasColumnType("text")
                        .IsUnicode(false);

                    b.Property<Guid>("PhotoFaceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PhotoFaceId");

                    b.ToTable("PhotoFaceDescriptors");
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.PhotoProfile", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .IsUnicode(false);

                    b.Property<string>("Mime")
                        .HasColumnType("character varying(20)")
                        .HasMaxLength(20)
                        .IsUnicode(false);

                    b.Property<byte[]>("Photo")
                        .HasColumnType("bytea");

                    b.Property<Guid>("RowId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("current_date");

                    b.HasKey("PhotoId");

                    b.HasIndex("RowId")
                        .IsUnique()
                        .HasName("UQ_PK_PhotoProfile");

                    b.ToTable("PhotoProfiles");
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.AspNetUserClaim", b =>
                {
                    b.HasOne("FaceRecognitionApi.Models.AspNetUser", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.AspNetUserLogin", b =>
                {
                    b.HasOne("FaceRecognitionApi.Models.AspNetUser", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.AspNetUserRole", b =>
                {
                    b.HasOne("FaceRecognitionApi.Models.AspNetRole", "Role")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FaceRecognitionApi.Models.AspNetUser", "User")
                        .WithMany("AspNetUserRoles")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FaceRecognitionApi.Models.PhotoFaceDescriptor", b =>
                {
                    b.HasOne("FaceRecognitionApi.Models.PhotoFace", "PhotoFace")
                        .WithMany()
                        .HasForeignKey("PhotoFaceId")
                        .HasConstraintName("FK_PhotoFaceDescriptors_PhotoFace")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
