using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Data.Models
{
    public class BusLine
    {
        [Key]
        public int Id { get; set; }
        public float Price { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime CreationDate { get; set; }
        public string Agency { get; set; }


    }
}
