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
    public class SimpleStatisticsEntityRepository<T> 
        : ISimpleStatisticsEntityRepository<T>
        where T : SimpleStatisticsEntity
    {
        private readonly StatisticsDbContext _statisticsDbContext;
        private readonly DbSet<T> _dbSet;

        public SimpleStatisticsEntityRepository(StatisticsDbContext statisticsDbContext)
        {
            _statisticsDbContext = statisticsDbContext
                ?? throw new ArgumentNullException(nameof(statisticsDbContext));

            _dbSet = _statisticsDbContext.Set<T>();
        }

        public void AddToStatistics(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<List<StatisticsByDateItem>> GetStatisticsByDaysForMonthAsync(int year, int month, int userId)
        {
            var actionsByMonth = _dbSet
                .Where(l =>
                    l.DateTime.Year == year
                    && l.DateTime.Month == month
                    && l.UserId == userId)
                .Select(l => l.DateTime);

            var actionsGroupedByDay = actionsByMonth.GroupBy(ad => ad.Day);

            var statistics = new List<StatisticsByDateItem>();

            await actionsGroupedByDay.ForEachAsync(group =>
                statistics.Add(
                    new StatisticsByDateItem()
                    {
                        Date = new DateOnly(year, month, group.Key),
                        Value = group.Count()
                    }));

            return statistics;
        }

        public async Task<List<StatisticsByDateItem>> GetStatisticsByMonthsForYearAsync(int year, int userId)
        {
            var actionsForYear = _dbSet
                .Where(l =>
                    l.DateTime.Year == year
                    && l.UserId == userId)
                .Select(l => l.DateTime);

            var actionsGroupedByMonth = actionsForYear.GroupBy(ld => ld.Month);

            var statistics = new List<StatisticsByDateItem>();

            await actionsGroupedByMonth.ForEachAsync(group =>
                statistics.Add(
                    new StatisticsByDateItem()
                    {
                        Date = new DateOnly(year, group.Key, 1),
                        Value = group.Count()
                    }));

            return statistics;
        }

        public void RemoveFromStatistics(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
