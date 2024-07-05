using EichkustMusic.States.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.Statistics.Application.UnitOfWork.Repositories
{
    public interface ISimpleStatisticsEntityRepository<T> where T : SimpleStatisticsEntity
    {
        public void AddToStatistics(T entity);

        public Task<T?> GetFromStatisticsById(int id);

        public void RemoveFromStatistics(T entity);

        public Task<List<StatisticsByDateItem>> GetStatisticsByMonthsForYearAsync(
            int year, int trackId);

        public Task<List<StatisticsByDateItem>> GetStatisticsByDaysForMonthAsync(
            int year, int month, int trackId);
    }
}
