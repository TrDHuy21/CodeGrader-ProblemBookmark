using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    internal class ProblemBookmarkConfig : IEntityTypeConfiguration<ProblemBookmark>
    {
        public void Configure(EntityTypeBuilder<ProblemBookmark> builder)
        {
            builder.ToTable(nameof(ProblemBookmark));
            builder.HasKey(o => new { o.UserId, o.ProblemId });

        }
    }
}
