using EichkustMusic.States.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.Statistics.Application.UnitOfWork.Repositories
{
    public interface ICommentRepository : ISimpleStatisticsEntityRepository<Comment>
    {
        public Task<List<Comment>> ListTracksComments(int trackId);
    }
}
