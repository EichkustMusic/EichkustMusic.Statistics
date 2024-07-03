using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.States.Domain.Entities
{
    public class Comment : SimpleStatisticsEntity
    {
        [Required]
        [MaxLength(2048)]
        public string Value { get; set; } = null!;
    }
}
