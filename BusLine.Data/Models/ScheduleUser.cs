using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Data.Models
{
    public class ScheduleUser
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        [AllowNull]
        public User? user { get; set; }
        [ForeignKey(nameof(Schedule))]
        public int ScheduleId { get; set; }
        public Schedule schedule { get; set; }
    }
}
