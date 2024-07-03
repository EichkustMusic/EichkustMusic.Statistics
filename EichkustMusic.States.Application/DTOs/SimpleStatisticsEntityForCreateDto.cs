using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.Statistics.Application.DTOs
{
    public class SimpleStatisticsEntityForCreateDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int TrackId { get; set; }
    }
}
