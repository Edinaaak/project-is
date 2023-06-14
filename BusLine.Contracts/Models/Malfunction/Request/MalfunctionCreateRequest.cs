using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Malfunction.Request
{
    public class MalfunctionCreateRequest
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int BusId { get; set; }
    }
}
