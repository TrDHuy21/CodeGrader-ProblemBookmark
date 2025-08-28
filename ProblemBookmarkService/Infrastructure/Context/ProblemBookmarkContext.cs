using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infrastructure.Context
{
    public class ProblemBookmarkContext : DbContext
    {
        public ProblemBookmarkContext()
        {
        }

        public ProblemBookmarkContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProblemBookmarkConfig());

            //seed data
            modelBuilder.Entity<ProblemBookmark>().HasData(
                new ProblemBookmark() { UserId = "2", ProblemId = "1"},
                new ProblemBookmark() { UserId = "2", ProblemId = "2"},
                new ProblemBookmark() { UserId = "2", ProblemId = "3"}
            );
        }

        public DbSet<ProblemBookmark> ProblemBookmarks { get; set; }
    }
}
