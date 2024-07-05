using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public bool IsFull { get; set; } = true;
    }
}
