﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Webcomic.Models.Entities;

namespace Webcomic.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region DbSet
        public DbSet<Comic> Comics { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ComicTag> ComicsTags { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Comic
            modelBuilder.Entity<Comic>()
                .ToTable("Comic")
                .HasKey(c => c.Id); 
            modelBuilder.Entity<Comic>() // 1-n Comic-Chapter
                .HasMany(c => c.Chapters)
                .WithOne(ch => ch.Comic)
                .HasForeignKey(ch => ch.ComicId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tag
            modelBuilder.Entity<Tag>()
                .ToTable("Tag")
                .HasKey(t => t.Id);

            // ComicTag n-n Comic-Tag
            modelBuilder.Entity<ComicTag>()
                .ToTable("ComicTag")
                .HasKey(ct => new { ct.ComicId, ct.TagId });
            modelBuilder.Entity<ComicTag>()
                .HasOne(ct => ct.Comic)
                .WithMany(c => c.ComicTags)
                .HasForeignKey(ct => ct.ComicId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ComicTag>()
                .HasOne(ct => ct.Tag)
                .WithMany(t => t.ComicTags)
                .HasForeignKey(ct => ct.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            // Chapter
            modelBuilder.Entity<Chapter>()
                .ToTable("Chapter")
                .HasKey(c => c.Id);
        }
    }
}
