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
    public class Travel
    {
        [Key]
        public int Id { get; set; }
        public DateTime TravelDate { get; set; }
        [ForeignKey(nameof(Schedule))]
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        [ForeignKey(nameof(Bus))]
        [AllowNull]
        public int? BusId { get; set; }
        [AllowNull]
        public Bus? Bus { get; set; }

    }
}
