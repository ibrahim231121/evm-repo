using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Models.CapturedPlate
{
    public class CapturePlatesSummaryStatusItem
    {
        public long SyncId { get; set; }

        public DateTime? LastExecutionDate { get; set; }

        public DateTime? LastExecutionEndDate { get; set; }

        public int? StatusId { get; set; }

        public string? StatusDesc { get; set; }
    }
}
