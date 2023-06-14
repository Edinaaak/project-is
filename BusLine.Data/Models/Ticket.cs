using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int SeatNumber { get; set; }
        [ForeignKey(nameof(Travel))]
        public int TravelId{get; set;}
        public Travel Travel { get; set; }
    }
}
