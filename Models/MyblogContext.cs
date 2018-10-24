using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace MyblogApp.Models
{
    public partial class MyblogContext : DbContext
    {
        public MyblogContext()
        {
        }

        public MyblogContext(DbContextOptions<MyblogContext> options)
            : base(options)
        {
        }
        // TODO : When in memory and when not in memory?
        // A Microsoft.EntityFrameworkCore.DbSet1 can be used to query and save instances of TEntity . 
        // LINQ queries against a Microsoft.EntityFrameworkCore.DbSet1 will be translated into queries against the database.
        // The results of a LINQ query against a Microsoft.EntityFrameworkCore.DbSet`1 will contain the results 
        // returned from the database and may not reflect changes made in the context that have not been persisted to the database. 
        // For example, the results will not contain newly added entities and may still contain entities that are marked for deletion.
        // Depending on the database being used, 
        // some parts of a LINQ query against a Microsoft.EntityFrameworkCore.DbSet`1 may be evaluated in memory 
        // rather than being translated into a database query.
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Efmigrationshistory> Efmigrationshistory { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Recipies> Recipies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql(AppSettingsClass.MyConnection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasColumnType("varchar(95)");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)");
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.ToTable("posts");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("body")
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<Recipies>(entity =>
            {
                entity.ToTable("recipies");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Ingredients)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("varchar(250)");
            });
        }
    }
}
