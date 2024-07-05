using EichkustMusic.Statistics.Application.UnitOfWork.Repositories;
using EichkustMusic.States.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.States.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        ISimpleStatisticsEntityRepository<Like> LikeRepository { get; }

        ISimpleStatisticsEntityRepository<Listening> ListeningRepository { get; }

        ISimpleStatisticsEntityRepository<Sharing> SharingRepository { get; }

        ICommentRepository CommentRepository { get; }

        Task SaveChangesAsync();
    }
}
