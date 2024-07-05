using EichkustMusic.States.Domain.Entities;
using EichkustMusic.Statistics.Application.UnitOfWork.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.Statistics.Infrastructure.Persistence.UnitOfWork.Repositories
{
    public class CommentRepository : SimpleStatisticsEntityRepository<Comment>, ICommentRepository
    {
        private readonly StatisticsDbContext _statisticsDbContext;

        public CommentRepository(StatisticsDbContext statisticsDbContext)
            : base(statisticsDbContext)
        { 
            _statisticsDbContext = statisticsDbContext;
        }

        public async Task<List<Comment>> ListTracksComments(int trackId)
        {
            return await _statisticsDbContext.Comments
                .Where(c => c.TrackId == trackId)
                .Select(c => new Comment
                {
                    Id = c.Id,
                    TrackId = c.TrackId,
                    UserId = c.UserId,
                    DateTime = c.DateTime,
                    Value = 
                        c.Value.Length > 128 
                        ? c.Value.Substring(0, 128)
                        : c.Value,
                    IsFull = !(c.Value.Length > 128)
                })
                .ToListAsync();
        }
    }
}
