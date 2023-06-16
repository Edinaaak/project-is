using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Contracts.Models.Schedule
{
    public class ScheduleUserCreateRequest
    {
        public string? UserId { get; set; }
        public int ScheduleId { get; set; }
    }
}
