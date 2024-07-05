using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.Statistics.Application.DTOs
{
    public class SimpleStatisticsEntityDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TrackId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
