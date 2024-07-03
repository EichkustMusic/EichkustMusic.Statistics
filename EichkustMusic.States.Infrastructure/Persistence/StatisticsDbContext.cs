using EichkustMusic.States.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.Statistics.Infrastructure.Persistence
{
    public class StatisticsDbContext : DbContext
    {
        public StatisticsDbContext(DbContextOptions<StatisticsDbContext> options)
            : base(options) { }

        public DbSet<Sharing> Sharings { get; set; }

        public DbSet<Listening> Listenings { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
