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
    [Migration("20200530043128_profile_matching_config")]
    partial class profile_matching_config
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

                    b.Property<string>("Changed")
                        .HasColumnName("changed")
                        .HasMaxLength(100);

                    b.Property<int>("Component")
                        .HasColumnName("component");

                    b.Property<string>("CreatedDate")
                        .HasColumnName("createddate")
                        .HasMaxLength(100);

                    b.Property<string>("CreatedTime")
                        .HasColumnName("createdtime")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnName("description");

                    b.Property<int>("Hardware")
                        .HasColumnName("hardware");

                    b.Property<string>("KeyWords")
                        .HasColumnName("keywords");

                    b.Property<int>("Priority")
                        .HasColumnName("priority");

                    b.Property<int>("Product")
                        .HasColumnName("product");

                    b.Property<string>("Resolution")
                        .HasColumnName("resolution")
                        .HasMaxLength(100);

                    b.Property<int>("Severity")
                        .HasColumnName("severity");

                    b.Property<string>("Source")
                        .HasColumnName("source")
                        .HasMaxLength(100);

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

            modelBuilder.Entity("DataAccess.Models.BugDeveloperMapping", b =>
                {
                    b.Property<int>("bug_developer_mapping_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("bug_id");

                    b.Property<int>("developer_id");

                    b.HasKey("bug_developer_mapping_id");

                    b.ToTable("bug_developer_mappings");
                });

            modelBuilder.Entity("DataAccess.Models.Component", b =>
                {
                    b.Property<int>("ComponentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("component_id");

                    b.Property<string>("ComponentName")
                        .HasColumnName("component_name")
                        .HasMaxLength(255);

                    b.Property<string>("DevelopersWorked")
                        .HasColumnName("developers_worked");

                    b.HasKey("ComponentId");

                    b.ToTable("components");
                });

            modelBuilder.Entity("DataAccess.Models.ComponentDeveloperMapping", b =>
                {
                    b.Property<int>("component_developer_mapping_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("component_id");

                    b.Property<int>("developer_id");

                    b.HasKey("component_developer_mapping_id");

                    b.ToTable("ComponentDeveloperMappings");
                });

            modelBuilder.Entity("DataAccess.Models.Developer", b =>
                {
                    b.Property<int>("DeveloperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("developer_id");

                    b.Property<string>("DeveloperName")
                        .HasColumnName("developer_name")
                        .HasMaxLength(255);

                    b.Property<int?>("DeveloperTypeId")
                        .HasColumnName("developer_type_id");

                    b.Property<string>("PreviousWorks")
                        .HasColumnName("previousworks");

                    b.Property<string>("Skills")
                        .HasColumnName("skills");

                    b.Property<int?>("current_workload");

                    b.HasKey("DeveloperId");

                    b.ToTable("developer");
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

            modelBuilder.Entity("DataAccess.Models.Hardware", b =>
                {
                    b.Property<int>("hardware_id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("hardware_name")
                        .HasMaxLength(255);

                    b.HasKey("hardware_id");

                    b.ToTable("hardwares");
                });

            modelBuilder.Entity("DataAccess.Models.HardwareDeveloperMapping", b =>
                {
                    b.Property<int>("hardware_developer_mapping_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("developer_id");

                    b.Property<int>("hardware_id");

                    b.HasKey("hardware_developer_mapping_id");

                    b.ToTable("HardwareDeveloperMappings");
                });

            modelBuilder.Entity("DataAccess.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("product_id");

                    b.Property<string>("DevelopersWorked")
                        .HasColumnName("developers_worked");

                    b.Property<string>("ProductName")
                        .HasColumnName("product_name")
                        .HasMaxLength(255);

                    b.HasKey("ProductId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("DataAccess.Models.ProductDeveloperMapping", b =>
                {
                    b.Property<int>("product_developer_mapping_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("developer_id");

                    b.Property<int>("product_id");

                    b.HasKey("product_developer_mapping_id");

                    b.ToTable("ProductDeveloperMappings");
                });

            modelBuilder.Entity("DataAccess.Models.ProfileMatchConfig", b =>
                {
                    b.Property<int>("profile_match_config_id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("no_of_fd");

                    b.Property<int>("no_of_ned");

                    b.Property<int>("no_of_sed");

                    b.Property<string>("severity")
                        .HasMaxLength(50);

                    b.HasKey("profile_match_config_id");

                    b.ToTable("profile_match_config");
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
