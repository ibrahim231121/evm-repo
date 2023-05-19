using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Models.CapturedPlate
{
    public class CapturePlatesSummaryItem
    {
        public long CapturePlateId { get; set; }

        public int StationId { get; set; }

        public int ClientId { get; set; }

        public string UnitId { get; set; } = null!;

        public long UserId { get; set; }

        public string LoginId { get; set; } = null!;

        public DateTime CaptureDate { get; set; }

        public bool HasAlert { get; set; }

        public bool HasTicket { get; set; }
    }
}
