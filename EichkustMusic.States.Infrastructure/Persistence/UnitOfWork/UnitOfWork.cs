using EichkustMusic.States.Application.UnitOfWork;
using EichkustMusic.States.Domain.Entities;
using EichkustMusic.Statistics.Application.UnitOfWork.Repositories;
using EichkustMusic.Statistics.Infrastructure.Persistence.UnitOfWork.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.Statistics.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StatisticsDbContext _statisticsDbContext;

        public UnitOfWork(StatisticsDbContext statisticsDbContext)
        {
            _statisticsDbContext = statisticsDbContext
                ?? throw new ArgumentNullException(nameof(statisticsDbContext));

            LikeRepository = new SimpleStatisticsEntityRepository<Like>(_statisticsDbContext);

            ListeningRepository = new SimpleStatisticsEntityRepository<Listening>(_statisticsDbContext);

            SharingRepository = new SimpleStatisticsEntityRepository<Sharing>(_statisticsDbContext);

            CommentRepository = new SimpleStatisticsEntityRepository<Comment>(_statisticsDbContext);
        }

        public ISimpleStatisticsEntityRepository<Like> LikeRepository { get; }

        public ISimpleStatisticsEntityRepository<Listening> ListeningRepository { get; }

        public ISimpleStatisticsEntityRepository<Sharing> SharingRepository {  get; }

        public ISimpleStatisticsEntityRepository<Comment> CommentRepository { get; }

        public async Task SaveChangesAsync()
        {
            await _statisticsDbContext.SaveChangesAsync();
        }
    }
}
