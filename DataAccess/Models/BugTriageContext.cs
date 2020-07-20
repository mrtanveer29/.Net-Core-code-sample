using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccess.Models;


namespace DataAccess.Models
{
    public partial class BugTriageContext : IdentityDbContext
    {


        public BugTriageContext(DbContextOptions<BugTriageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Developer> Developer { get; set; }
        public virtual DbSet<DeveloperTypes> DeveloperTypes { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }
     
        public virtual DbSet<Bug> Bugs { get; set; }
        public virtual DbSet<AssignedDevelopers> AssignedDevelopers { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Hardware> Hardwares { get; set; }
        public virtual DbSet<ProductDeveloperMapping> ProductDeveloperMappings { get; set; }
        public virtual DbSet<ComponentDeveloperMapping> ComponentDeveloperMappings { get; set; }
        public virtual DbSet<HardwareDeveloperMapping> HardwareDeveloperMappings { get; set; }
        public virtual DbSet<BugDeveloperMapping> bug_developer_mappings { get; set; }
        public virtual DbSet<ProfileMatchConfig> profile_match_config { get; set; }
        public virtual DbSet<BugExcelInfo> bug_execl_info { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity => {
                entity.ToTable("products");
                entity.Property(p => p.ProductId).HasColumnName("product_id");
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.ProductName).HasColumnName("product_name").HasMaxLength(255);
                entity.Property(p => p.DevelopersWorked).HasColumnName("developers_worked");
            });
            modelBuilder.Entity<Hardware>(entity => {
                entity.ToTable("hardwares");
            });

            modelBuilder.Entity<Component>(entity => {
                entity.ToTable("components");
                entity.HasKey(p => p.ComponentId);
                entity.Property(p => p.ComponentId).HasColumnName("component_id");
                entity.Property(p => p.ComponentName).HasColumnName("component_name").HasMaxLength(255);
                entity.Property(p => p.DevelopersWorked).HasColumnName("developers_worked");
            });
            modelBuilder.Entity<Developer>(entity =>
            {
                entity.Property(e => e.DeveloperId).HasColumnName("developer_id");
                entity.ToTable("developer");
                entity.Property(e => e.DeveloperName)
                    .HasColumnName("developer_name")
                    .HasMaxLength(255);
                entity.Property(e => e.Skills)
                  .HasColumnName("skills");
              
                entity.Property(e => e.PreviousWorks)
               .HasColumnName("previousworks");
              



                entity.Property(e => e.DeveloperTypeId).HasColumnName("developer_type_id");

            });

            modelBuilder.Entity<DeveloperTypes>(entity =>
            {
                entity.HasKey(e => e.DeveloperTypeId);

                entity.ToTable("developer_types");

                entity.Property(e => e.DeveloperTypeId).HasColumnName("developer_type_id");

                entity.Property(e => e.DeveloperTypeName)
                    .HasColumnName("developer_type_name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Skills>(entity =>
            {
                entity.HasKey(e => e.SkillId);

                entity.ToTable("skills");

                entity.Property(e => e.SkillId).HasColumnName("skill_id");

                entity.Property(e => e.Skill)
                    .HasColumnName("skill")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Bug>(entity =>
            {
                entity.HasKey(e => e.BugId);

                entity.ToTable("bugs");

              

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(255);
                entity.Property(e => e.Component)
                   .HasColumnName("component");
                entity.Property(e => e.Product)
                  .HasColumnName("product");
                entity.Property(e => e.Severity)
                  .HasColumnName("severity");
                entity.Property(e => e.Priority)
                  .HasColumnName("priority");
                entity.Property(e => e.Assignee)
                 .HasColumnName("assignee");
                entity.Property(e => e.Summary)
                .HasColumnName("summary");
                entity.Property(e => e.Changed)
              .HasColumnName("changed");
                entity.Property(e => e.Description)
                .HasColumnName("description");
                entity.Property(e => e.KeyWords)
               .HasColumnName("keywords");
                entity.Property(e => e.Hardware)
              .HasColumnName("hardware");
                entity.Property(e => e.Resolution)
                .HasColumnName("resolution").HasMaxLength(100);
                entity.Property(e => e.CreatedDate)
                .HasColumnName("createddate").HasMaxLength(100);
                entity.Property(e => e.CreatedTime)
                .HasColumnName("createdtime").HasMaxLength(100);
                entity.Property(e => e.Source)
                .HasColumnName("source").HasMaxLength(100);


            });
        }







    }
}
