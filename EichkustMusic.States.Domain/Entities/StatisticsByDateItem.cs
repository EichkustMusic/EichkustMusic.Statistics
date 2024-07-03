using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.States.Domain.Entities
{
    /// <summary>
    /// Statistics by days or months item. Full statistics by days or months is a list of <c>DateStatisticsItem</c>
    /// </summary>
    public class StatisticsByDateItem
    {
        /// <summary>
        /// Can be used as month if day of each date set to 1
        /// </summary>
        public DateOnly Date { get; set; }

        public int Value { get; set; }
    }
}
