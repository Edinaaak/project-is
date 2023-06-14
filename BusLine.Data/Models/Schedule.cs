using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Data.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public TimeOnly ArrivalTime { get; set; }
        [NotMapped]
        public TimeOnly DepartureTime { get; set; }
        public int Platform { get; set; }
        public string Day { get; set; }
        public bool Direction { get; set; }
        [ForeignKey(nameof(BusLine))]
        public int BusLineId { get; set; }
        public BusLine BusLine { get; set; }

        public List<ScheduleUser> scheduleUsers { get; set; }
        public List<Travel> travels { get; set; }
        
    }
}
