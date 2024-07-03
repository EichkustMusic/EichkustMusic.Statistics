using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EichkustMusic.States.Domain.Entities
{
    /// <summary>
    /// Simple statistics entity. Contains reference to user, track and action complete time
    /// </summary>
    public class SimpleStatisticsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int TrackId { get; set; }

        public DateTime DateTime { get; set; } = DateTime.UtcNow;
    }
}
