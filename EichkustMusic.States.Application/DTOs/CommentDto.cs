using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.Statistics.Application.DTOs
{
    public class CommentDto : SimpleStatisticsEntityDto
    {
        public string Value { get; set; } = null!;
        public bool isFull { get; set; } = true;
    }
}
