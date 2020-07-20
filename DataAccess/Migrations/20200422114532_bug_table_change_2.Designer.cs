﻿// <auto-generated />
using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EmployeeManagementCore.Migrations
{
    [DbContext(typeof(BugTriageContext))]
    [Migration("20200422114532_bug_table_change_2")]
    partial class bug_table_change_2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("DataAccess.Models.AssignedDevelopers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BugId");

                    b.Property<int>("DeveloperId");

                    b.HasKey("Id");

                    b.ToTable("AssignedDevelopers");
                });

            modelBuilder.Entity("DataAccess.Models.Bug", b =>
                {
                    b.Property<int>("BugId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Assignee")
                        .HasColumnName("assignee");

                    b.Property<string>("BugTitle")
                        .HasColumnName("bugtitle")
                        .HasMaxLength(255);

                    b.Property<string>("Changed")
                        .HasColumnName("changed")
                        .HasMaxLength(100);

                    b.Property<string>("Component")
                        .HasColumnName("component");

                    b.Property<string>("CreatedDate")
                        .HasColumnName("createddate")
                        .HasMaxLength(100);

                    b.Property<string>("CreatedTime")
                        .HasColumnName("createdtime")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<string>("Hardware")
                        .HasColumnName("hardware")
                        .HasMaxLength(255);

                    b.Property<string>("KeyWords")
                        .HasColumnName("keywords");

                    b.Property<int>("Priority")
                        .HasColumnName("priority");

                    b.Property<string>("Product")
                        .HasColumnName("product");

                    b.Property<string>("Resolution")
                        .HasColumnName("resolution")
                        .HasMaxLength(100);

                    b.Property<int>("Severity")
                        .HasColumnName("severity");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasMaxLength(255);

                    b.Property<string>("Summary")
                        .HasColumnName("summary");

                    b.Property<int>("id");

                    b.Property<decimal>("version")
                        .HasColumnType("decimal(6,2)");

                    b.HasKey("BugId");

                    b.ToTable("bugs");
                });

            modelBuilder.Entity("DataAccess.Models.Developer", b =>
                {
                    b.Property<int>("DeveloperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("developer_id");

                    b.Property<string>("Components");

                    b.Property<string>("DeveloperName")
                        .HasColumnName("developer_name")
                        .HasMaxLength(255);

                    b.Property<int?>("DeveloperTypeId")
                        .HasColumnName("developer_type_id");

                    b.Property<string>("Platforms");

                    b.Property<string>("Products");

                    b.Property<string>("Skills");

                    b.HasKey("DeveloperId");

                    b.ToTable("Developer");
                });

            modelBuilder.Entity("DataAccess.Models.DeveloperTypes", b =>
                {
                    b.Property<int>("DeveloperTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("developer_type_id");

                    b.Property<string>("DeveloperTypeName")
                        .HasColumnName("developer_type_name")
                        .HasMaxLength(255);

                    b.HasKey("DeveloperTypeId");

                    b.ToTable("developer_types");
                });

            modelBuilder.Entity("DataAccess.Models.Priority", b =>
                {
                    b.Property<int>("PriorityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PriorityName");

                    b.HasKey("PriorityId");

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("DataAccess.Models.Severity", b =>
                {
                    b.Property<int>("SeverityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SeverityName");

                    b.HasKey("SeverityId");

                    b.ToTable("Severities");
                });

            modelBuilder.Entity("DataAccess.Models.Skills", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("skill_id");

                    b.Property<string>("Developers");

                    b.Property<string>("Skill")
                        .HasColumnName("skill")
                        .HasMaxLength(255);

                    b.HasKey("SkillId");

                    b.ToTable("skills");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
